# Plan: SharePoint Alert Sync via OneDrive Local Folder

## Overview

Sync `alerts.json` through the OneDrive for Business local sync folder, which already mirrors the SharePoint Documents library at `https://groupecgi-my.sharepoint.com/personal/david_williams1_cgi_com/Documents`. No HTTP or OAuth code is required because file movement is handled by the OneDrive client.

This design is now explicitly **one-way sharing**:

- **Source** instance: writes alerts to the shared remote file
- **Target** instance: reads alerts from the shared remote file
- **Target never writes back to remote**

This removes bidirectional conflict handling from scope and makes the sync rules much easier to reason about.

**Key decisions:**
- Files to sync: `alerts.json` only
- Auth method: OneDrive for Business local sync folder
- Sync model: one-way sharing
- Sync roles: `Source` or `Target`
- Source behavior: local save + push to remote
- Target behavior: pull from remote only
- Remote subfolder: `Quanta\` inside the configured sync path
- Alert identity: add a short GUID-like id to each alert for stable matching across machines

## Recommended Configuration

Add or revise these keys in `Quanta.Core.Windows/appsettings.json`:

```json
{
  "syncFolderPath": "C:\\Users\\david.williams1\\CGI Group Inc\\david.williams1_cgi_com - Documents",
  "syncEnabled": "true",
  "syncRole": "Source"
}
```

- `syncFolderPath` - the local OneDrive for Business sync path on this machine
- `syncEnabled` - master toggle for sync
- `syncRole` - must be either `Source` or `Target`

Role behavior:

| Setting | Reads remote | Writes remote |
|---|---|---|
| `Source` | Optional/manual | Yes |
| `Target` | Yes | No |

If preferred, `Target` can also be treated as read-only in the UI so users understand they are consuming shared alerts rather than publishing them.

---

## Why Add a GUID?

Yes, sync will be helped by adding a GUID to each alert.

Without a stable id, alerts have to be matched using fields like title and date/time, which is fragile when:

- two alerts have the same title
- a title is edited
- the alert time is changed
- repeat settings are adjusted

Adding an id gives each alert a stable identity across edits and across machines. Even in a one-way model, this helps with:

- safer merge or overwrite logic later if the design expands
- reliable tracking of which alert changed
- future support for delete/update comparisons instead of replacing the entire file blindly

### Recommended id shape

Add a `Guid` property to each alert record and store a **10-character generated id** when missing.

Examples:

- `A1F84C2D9B`
- `7C91E2AB4F`

This is not a full RFC 4122 GUID string; it is a short unique identifier. If the codebase already uses the term `Guid`, the plan can keep that property name for familiarity, but the implementation should document that it stores a short generated identifier rather than a 36-character GUID.

---

## Updated Sync Rules

### Source

- Loads local `alerts.json`
- Ensures every alert has a `Guid`
- Saves local changes
- Pushes `alerts.json` to `<syncFolderPath>\Quanta\alerts.json`

### Target

- Reads `<syncFolderPath>\Quanta\alerts.json`
- Ensures every alert has a `Guid`
- If any alert is missing a `Guid`, generates one and writes the updated file back to the file it was read from
- Uses the remote-backed content locally
- Does not push local edits to remote

Because this is one-way sharing, the authoritative copy is effectively the Source-authored remote file, with one exception: if a file being read contains alerts with missing ids, the reader is allowed to normalize the file by assigning ids.

---

## Phase 1 - Configuration

Revise configuration handling to support role-based sync.

### App settings

```json
{
  "syncFolderPath": "C:\\Users\\david.williams1\\CGI Group Inc\\david.williams1_cgi_com - Documents",
  "syncEnabled": "true",
  "syncRole": "Source"
}
```

### Validation rules

- If `syncEnabled` is false, sync is skipped
- If `syncRole` is blank or invalid, default to `Target` for safety or fail with a clear config error
- If `syncFolderPath` is missing, report sync as unavailable

---

## Phase 2 - Alert Model and Read Normalization

Update the alert model and alert file load path to support ids.

### Alert schema addition

Each alert should support:

```json
{
  "Guid": "A1F84C2D9B"
}
```

### Read behavior

When reading alerts from a file:

1. Deserialize alerts
2. For each alert, inspect `Guid`
3. If `Guid` is null, empty, or missing:
   - generate a new 10-character id
   - assign it to the alert
4. If any ids were created:
   - write the updated alert list back to the same file

This should happen for both local and remote-backed reads so the system gradually normalizes older files.

### Generation rule

Use a random uppercase alphanumeric 10-character string, or generate from a standard GUID and trim/sanitize consistently. The important part is:

- exactly 10 characters
- stable once assigned
- low collision risk for the alert volumes involved here

---

## Phase 3 - SyncService

Update `Quanta.Core.Service/SyncService.cs` so behavior depends on `syncRole`.

### SyncResult enum

```csharp
public enum SyncResult
{
    Success,
    Offline,
    Skipped,
    Error
}
```

### Methods

| Method | Behaviour |
|---|---|
| `IsRemoteAvailable()` | Returns `true` if `Directory.Exists(syncFolderPath)` |
| `PullFromRemote(string localFilePath)` | Copies `<syncFolderPath>\Quanta\alerts.json` to `localFilePath`, then ensures all alerts have `Guid` values |
| `PushToRemote(string localFilePath)` | Only allowed for `Source`; copies `localFilePath` to `<syncFolderPath>\Quanta\alerts.json` |
| `CanPush()` | Returns `true` only when `syncRole == Source` |
| `CanPull()` | Returns `true` when sync is enabled and the remote is available |

### Role logic

- `Source`
  - may push after save
  - may optionally pull manually if desired, but this is not required for one-way sharing
- `Target`
  - may pull
  - must never push
  - save operations should remain local-only unless the UI is made fully read-only

If `PushToRemote()` is called while role is `Target`, it should return `SyncResult.Skipped` and do nothing.

---

## Phase 4 - UI Integration in ViewAlerts

Adjust the UI so the role is visible and behavior is clear.

### Source UI

- Keep Save button behavior
- After save, attempt remote push
- Sync status can show:
  - `Last published: ...`
  - `Saved locally only`
  - `Offline`

### Target UI

- Provide Sync/Refresh button for pull
- Status can show:
  - `Last refreshed: ...`
  - `Offline - sync unavailable`
- Consider disabling or hiding publishing-related language

If editing remains enabled on Target, clearly label that those edits are local-only and will not be shared.

---

## Phase 5 - Persist Last Sync Timestamp

Continue writing sync state to `c:\quanta\sync-state.json`, but track the action type.

Example:

```json
{
  "lastSyncUtc": "2026-03-26T14:15:00Z",
  "lastSyncAction": "Push"
}
```

Possible values for `lastSyncAction`:

- `Push`
- `Pull`
- `Normalize`

`Normalize` is useful when ids were added during a read/write-back operation.

---

## Files Affected

| File | Change |
|---|---|
| `Quanta.Core.Windows/appsettings.json` | Add or revise `syncFolderPath`, `syncEnabled`, `syncRole` |
| `Quanta.Core.Service/SyncService.cs` | Role-aware pull/push logic |
| `Quanta.Core.Service/AlertService.cs` | Ensure `Guid` exists when reading alerts; write back if ids were added |
| `Quanta.Core.Models` alert model file | Add `Guid` property |
| `Quanta.Core.Windows/ViewAlerts.cs` | Role-aware sync and status messaging |
| `Quanta.Core.Windows/ViewAlerts.Designer.cs` | Optional label/button text changes for Source vs Target |

---

## Verification Checklist

1. `Source` save writes local file and pushes to `<syncFolderPath>\Quanta\alerts.json`
2. `Target` sync reads remote file and updates local view/file
3. `Target` never pushes, even if Save is clicked
4. Loading a legacy `alerts.json` with missing `Guid` values assigns 10-character ids
5. After id generation, the file is rewritten with the new ids
6. Existing alerts with populated `Guid` values keep the same id
7. Offline target pull reports `Offline` without crashing
8. Offline source save still succeeds locally and reports that remote publish was skipped

---

## Further Considerations

1. Use `appsettings.json` for the sync file location and role. Do not add a file-browser button.
2. Keep hourly background sync in addition to the existing sync buttons and menu options.
3. Do not include `sprints.json` or `userstories.json` in this sync scope.
4. Keep `Quanta` as the remote subfolder name for now.
5. Keep the property name `Guid` for the 10-character alert identifier.

