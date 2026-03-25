# Plan: SharePoint Alert Sync via OneDrive Local Folder

## Overview

Sync `alerts.json` to the OneDrive for Business local sync folder, which already mirrors the SharePoint Documents library at `https://groupecgi-my.sharepoint.com/personal/david_williams1_cgi_com/Documents`. No HTTP or OAuth code is required — offline support is provided by the OneDrive client itself.

**Key decisions:**
- Files to sync: `alerts.json` only
- Auth method: OneDrive for Business local sync folder (no HTTP/OAuth)
- Sync trigger: Manual "Sync" button in the UI (future: hourly interval)
- Conflict resolution: Remote always wins on pull
- Push: Automatic after save, if the remote folder is reachable
- Remote subfolder: `Quanta\` inside the configured sync path

> Deferred change: the newer-update-wins conflict model discussed on 2026-03-25 is intentionally postponed until after the initial Phase 1-4 path is in place.

## Progress

- [x] Phase 1 — Configuration
- [x] Phase 2 — SyncService
- [x] Phase 3 — UI Integration in ViewAlerts
- [x] Phase 4 — Persist Last-Sync Timestamp

## Change Log

### 2026-03-25

- Completed Phase 1 configuration in `Quanta.Core.Windows/appsettings.json`
- Added `syncFolderPath` for the local OneDrive for Business mount point
- Added `syncEnabled` toggle to allow sync to be disabled without removing the path
- Completed Phase 2 by adding `Quanta.Core.Service/SyncService.cs`
- Added `SyncResult`, `IsRemoteAvailable()`, `PullFromRemote()`, and `PushToRemote()`
- Completed Phase 3 by wiring `ViewAlerts` to manual pull and post-save push actions
- Added a Sync button and sync status label to `ViewAlerts`
- Completed Phase 4 by persisting last successful sync time to `c:\quanta\sync-state.json`
- Added load-time sync status display using the persisted timestamp
- Deferred the newer-update-wins conflict model until after Phase 4 to avoid widening the initial implementation scope

---

## Phase 1 — Configuration

Status: Completed on 2026-03-25

Add two new keys to `Quanta.Core.Windows/appsettings.json`:

```json
{
  "syncFolderPath": "C:\\Users\\david.williams1\\CGI Group Inc\\david.williams1_cgi_com - Documents",
  "syncEnabled": "true"
}
```

- `syncFolderPath` — the local OneDrive for Business sync path on this machine
- `syncEnabled` — toggle sync on/off without removing configuration

> **Note:** The local sync path varies per machine and organisation. Consider making this configurable via a file-browser button in a settings dialog (see Further Considerations).

---

## Phase 2 — SyncService

Status: Completed on 2026-03-25

Create `Quanta.Core.Service/SyncService.cs`, extending `BaseService` to reuse the existing config loading pattern.

### SyncResult enum

```csharp
public enum SyncResult
{
    Success,
    Offline,
    Error
}
```

### Methods

| Method | Behaviour |
|---|---|
| `IsRemoteAvailable()` | Returns `true` if `Directory.Exists(syncFolderPath)` |
| `PullFromRemote(string localFilePath)` | Copies `<syncFolderPath>\Quanta\alerts.json` → `localFilePath`, overwriting local (remote wins). Returns `SyncResult`. |
| `PushToRemote(string localFilePath)` | Copies `localFilePath` → `<syncFolderPath>\Quanta\alerts.json`, creating the `Quanta\` subfolder if needed. Returns `SyncResult`. |

Both methods catch exceptions and return `SyncResult.Offline` when the folder is unreachable, `SyncResult.Error` for unexpected failures.

---

## Phase 3 — UI Integration in ViewAlerts

Status: Completed on 2026-03-25

### 3a. Add controls to ViewAlerts.Designer.cs

- **"Sync" button** — placed alongside the existing Save/Add buttons
- **Status label** — displayed beside the button (e.g. `"Last synced: 3/24/2026 2:15 PM"` or `"Offline"`)

### 3b. Sync button click handler (ViewAlerts.cs)

```
On click:
  result = SyncService.PullFromRemote(localAlertsPath)
  switch result:
    Success  → reload alerts from local file, refresh DataGridView, update label
    Offline  → update label to "Offline - sync unavailable"
    Error    → show MessageBox with error detail
```

### 3c. After existing Save logic (ViewAlerts.cs)

After `AlertService.WriteAlertsToFile(alerts)` completes:

```
result = SyncService.PushToRemote(localAlertsPath)
switch result:
  Success  → update "Last synced: <timestamp>" label
  Offline  → update label to "Saved locally only"
  Error    → show a brief non-blocking local warning
```

---

## Phase 4 — Persist Last-Sync Timestamp

Status: Completed on 2026-03-25

Write the last-sync timestamp to `c:\quanta\sync-state.json` after every successful push or pull:

```json
{
  "lastSyncedUtc": "2026-03-24T14:15:00Z"
}
```

Display it on `ViewAlerts` form load as `"Last synced: 3/24/2026 2:15 PM"`. If the file does not exist, show `"Never synced"`.

---

## Files Affected

| File | Change |
|---|---|
| `Quanta.Core.Windows/appsettings.json` | Add `syncFolderPath`, `syncEnabled` |
| `Quanta.Core.Service/SyncService.cs` | New sync logic and sync-state persistence |
| `Quanta.Core.Windows/ViewAlerts.cs` | Sync button handler, push-after-save, label updates |
| `Quanta.Core.Windows/ViewAlerts.Designer.cs` | Sync button + status label controls |

### Reference files (read-only context)

- `Quanta.Core.Service/BaseService.cs` — config loading pattern to reuse
- `Quanta.Core.Service/AlertService.cs` — `WriteAlertsToFile` is the push trigger point
- `Quanta.Core.Windows/ViewAlerts.cs` — existing Save button pattern to mirror

---

## Verification Checklist

1. **Online — pull**: Click Sync → local `alerts.json` updates from `<syncFolderPath>\Quanta\alerts.json`
2. **Offline — pull**: Remove sync folder or disconnect → click Sync → label shows "Offline", no crash
3. **Conflict (remote wins)**: Edit alerts on a second machine via OneDrive, then click Sync on this machine → local file is overwritten
4. **Online — push after save**: Save alerts → push happens silently, label shows `"Last synced: ..."`
5. **Offline — push after save**: Save alerts with no network → local save succeeds, label shows `"Saved locally only"`
6. **Last-sync persistence**: Close and reopen `ViewAlerts` after a successful sync → previous sync time is displayed from `sync-state.json`

---

## Further Considerations

1. **OneDrive path auto-discovery** — The local sync path varies per machine. The app could read it automatically from the Windows registry key `HKCU\Software\Microsoft\OneDrive\Accounts\Business1\ScopeIdToMountPointPathCache`, falling back to the configured value in `appsettings.json`. A file-browser button in a settings dialog would also suffice.

2. **Hourly background sync** — A `System.Windows.Forms.Timer` in `MainForm.cs` (similar to the existing 10-second alert polling timer) could call `PullFromRemote` + `PushToRemote` on an hourly interval. Straightforward addition once `SyncService` exists.

3. **Expanding scope** — `sprints.json` and `userstories.json` excluded for now but can be added to `SyncService` with the same pattern.

4. **Sync subfolder name** — `Quanta\` is assumed inside Documents. Confirm or rename as needed.
