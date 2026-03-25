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
        Error
    }

    public class SyncService : BaseService
    {
        private const string RemoteFolderName = "Quanta";
        private const string AlertsFileName = "alerts.json";
        private const string SyncStateFilePath = "c:/quanta/sync-state.json";

        private readonly string syncFolderPath;
        private readonly bool syncEnabled;

        public string LastErrorMessage { get; private set; } = string.Empty;

        public SyncService()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            syncFolderPath = _config.GetValue<string>("syncFolderPath", string.Empty) ?? string.Empty;
            syncEnabled = bool.TryParse(_config.GetValue<string>("syncEnabled", "false"), out var enabled) && enabled;
        }

        public bool IsRemoteAvailable()
        {
            if (!syncEnabled || string.IsNullOrWhiteSpace(syncFolderPath))
            {
                return false;
            }

            return Directory.Exists(syncFolderPath);
        }

        public SyncResult PullFromRemote(string localFilePath)
        {
            LastErrorMessage = string.Empty;

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

                CreateIfDoesNotExist(localFilePath);
                File.Copy(remoteFilePath, localFilePath, true);
                WriteSyncState(DateTime.UtcNow);
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

                var remoteDirectoryPath = GetRemoteDirectoryPath();
                Directory.CreateDirectory(remoteDirectoryPath);

                var remoteFilePath = GetRemoteAlertsFilePath();
                File.Copy(localFilePath, remoteFilePath, true);
                WriteSyncState(DateTime.UtcNow);
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

        public DateTime? GetLastSyncedUtc()
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

                var syncState = JsonConvert.DeserializeObject<SyncState>(syncStateText);
                return syncState?.LastSyncedUtc;
            }
            catch (Exception ex)
            {
                LastErrorMessage = ex.Message;
                return null;
            }
        }

        public string GetLastSyncStatusText()
        {
            var lastSyncedUtc = GetLastSyncedUtc();
            if (!lastSyncedUtc.HasValue)
            {
                return "Never synced";
            }

            return $"Last synced: {lastSyncedUtc.Value.ToLocalTime():M/d/yyyy h:mm tt}";
        }

        private void WriteSyncState(DateTime lastSyncedUtc)
        {
            var syncState = new SyncState
            {
                LastSyncedUtc = lastSyncedUtc
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
            public DateTime LastSyncedUtc { get; set; }
        }
    }
}
