using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Quanta.Core.Service
{
    public enum SyncResult
    {
        Success,
        Offline,
        Skipped,
        Error
    }

    public enum SyncRole
    {
        Target,
        Source
    }

    public class SyncService : BaseService
    {
        private const string RemoteFolderName = "Quanta";
        private const string AlertsFileName = "alerts.json";
        private const string SyncStateFilePath = "c:/quanta/sync-state.json";
        private const string PushAction = "Push";
        private const string PullAction = "Pull";

        private readonly AlertService alertService = new AlertService();
        private readonly string syncFolderPath;
        private readonly bool syncEnabled;
        private readonly SyncRole syncRole;

        public string LastErrorMessage { get; private set; } = string.Empty;
        public bool IsSyncEnabled => syncEnabled;
        public bool IsSource => syncRole == SyncRole.Source;
        public bool IsTarget => syncRole == SyncRole.Target;
        public SyncRole Role => syncRole;

        public SyncService()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            syncFolderPath = _config.GetValue<string>("syncFolderPath", string.Empty) ?? string.Empty;
            syncEnabled = bool.TryParse(_config.GetValue<string>("syncEnabled", "false"), out var enabled) && enabled;

            var configuredRole = _config.GetValue<string>("syncRole", nameof(SyncRole.Target));
            syncRole = Enum.TryParse(configuredRole, true, out SyncRole parsedRole)
                ? parsedRole
                : SyncRole.Target;
        }

        public bool CanPush()
        {
            return syncEnabled && IsSource;
        }

        public bool CanPull()
        {
            return syncEnabled;
        }

        public string GetManualSyncText()
        {
            return IsSource ? "Publish" : "Refresh";
        }

        public bool IsRemoteAvailable()
        {
            if (!syncEnabled || string.IsNullOrWhiteSpace(syncFolderPath))
            {
                return false;
            }

            return Directory.Exists(syncFolderPath);
        }

        public SyncResult PerformConfiguredSync(string localFilePath)
        {
            return IsSource ? PushToRemote(localFilePath) : PullFromRemote(localFilePath);
        }

        public SyncResult PullFromRemote(string localFilePath)
        {
            LastErrorMessage = string.Empty;

            if (!CanPull())
            {
                return SyncResult.Skipped;
            }

            if (!IsRemoteAvailable())
            {
                return SyncResult.Offline;
            }

            try
            {
                var remoteFilePath = GetRemoteAlertsFilePath();
                if (!File.Exists(remoteFilePath))
                {
                    LastErrorMessage = "Remote alerts file was not found.";
                    return SyncResult.Error;
                }

                alertService.NormalizeAlertsFile(remoteFilePath);
                CreateIfDoesNotExist(localFilePath);
                File.Copy(remoteFilePath, localFilePath, true);
                alertService.NormalizeAlertsFile(localFilePath);
                WriteSyncState(DateTime.UtcNow, PullAction);
                return SyncResult.Success;
            }
            catch (DirectoryNotFoundException ex)
            {
                LastErrorMessage = ex.Message;
                return SyncResult.Offline;
            }
            catch (IOException ex) when (!IsRemoteAvailable())
            {
                LastErrorMessage = ex.Message;
                return SyncResult.Offline;
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                return SyncResult.Error;
            }
        }

        public SyncResult PushToRemote(string localFilePath)
        {
            LastErrorMessage = string.Empty;

            if (!syncEnabled)
            {
                return SyncResult.Skipped;
            }

            if (!CanPush())
            {
                return SyncResult.Skipped;
            }

            if (!IsRemoteAvailable())
            {
                return SyncResult.Offline;
            }

            try
            {
                if (!File.Exists(localFilePath))
                {
                    LastErrorMessage = "Local alerts file was not found.";
                    return SyncResult.Error;
                }

                alertService.NormalizeAlertsFile(localFilePath);

                var remoteDirectoryPath = GetRemoteDirectoryPath();
                Directory.CreateDirectory(remoteDirectoryPath);

                var remoteFilePath = GetRemoteAlertsFilePath();
                File.Copy(localFilePath, remoteFilePath, true);
                WriteSyncState(DateTime.UtcNow, PushAction);
                return SyncResult.Success;
            }
            catch (DirectoryNotFoundException ex)
            {
                LastErrorMessage = ex.Message;
                return SyncResult.Offline;
            }
            catch (IOException ex) when (!IsRemoteAvailable())
            {
                LastErrorMessage = ex.Message;
                return SyncResult.Offline;
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                return SyncResult.Error;
            }
        }

        public string GetLastSyncStatusText()
        {
            var syncState = GetSyncState();
            if (syncState == null || syncState.LastSyncUtc == default)
            {
                return "Never synced";
            }

            var actionLabel = syncState.LastSyncAction switch
            {
                PushAction => "Last published",
                PullAction => "Last refreshed",
                _ => "Last synced"
            };

            return $"{actionLabel}: {syncState.LastSyncUtc.ToLocalTime():M/d/yyyy h:mm tt}";
        }

        private SyncState GetSyncState()
        {
            try
            {
                if (!File.Exists(SyncStateFilePath))
                {
                    return null;
                }

                var syncStateText = File.ReadAllText(SyncStateFilePath);
                if (string.IsNullOrWhiteSpace(syncStateText))
                {
                    return null;
                }

                return JsonConvert.DeserializeObject<SyncState>(syncStateText);
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                return null;
            }
        }

        private void WriteSyncState(DateTime lastSyncedUtc, string lastSyncAction)
        {
            var syncState = new SyncState
            {
                LastSyncUtc = lastSyncedUtc,
                LastSyncAction = lastSyncAction
            };

            CreateIfDoesNotExist(SyncStateFilePath);
            File.WriteAllText(SyncStateFilePath, JsonConvert.SerializeObject(syncState, Formatting.Indented));
        }

        private string GetRemoteDirectoryPath()
        {
            return Path.Combine(syncFolderPath, RemoteFolderName);
        }

        private string GetRemoteAlertsFilePath()
        {
            return Path.Combine(GetRemoteDirectoryPath(), AlertsFileName);
        }

        private class SyncState
        {
            public DateTime LastSyncUtc { get; set; }
            public string LastSyncAction { get; set; } = string.Empty;
        }
    }
}
