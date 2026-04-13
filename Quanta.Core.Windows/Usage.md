# Quanta Usage

## Prerequisites

- Windows 10 or Windows 11
- .NET 9 Desktop Runtime

## Run

Start Quanta.Core.Windows.exe. The app runs in the system tray.

- Left-click tray icon: opens quick Log Text form
- Right-click tray icon: opens full menu (Hotkey, Log, Add Alert, Edit Log, etc.)

Suggest adding to Windows startup.

### Required Files

Quanta requires `appsettings.json` in the working directory.

Required setting:

```json
{
  "logFilename": "c:/quanta/worklog.txt",
  "alertsfilename": "c:/quanta/alerts.json"
}
```

Important notes:

- logFilename is required for logging and Edit Log
- Missing files like worklog.txt or alerts.json are created automatically when needed
- Missing appsettings.json is a startup blocker

## Log Text Usage

Use this for fast event logging.

### Open

- Press the log hotkey (default: Control+Scroll Lock)
- Or left-click tray icon
- Or tray menu -> Log

### Enter a Log Item

1. Type Description (main text area)
2. Optional: enter User Story in User Story box
3. Optional: enter Length in Length box (numeric)
4. Press Enter to save, or Esc to close

Saved line format:

```text
MM/dd/yy HH:mm: Description text (Length, US: StoryId)
```

Examples:

```text
04/13/26 09:15: Refined alert save validation (0.5, US: 1234)
04/13/26 10:05: Team sync and backlog grooming
```

## Add Event Usage (Alerts)

Use this for calendar reminders.

### Open

- Press alert hotkey (default: Pause/Break)
- Or tray menu -> Add Alert

### Create Alert

1. Select date
2. Select hour, minute, and AM/PM
3. Enter title
4. Optional: check repeat checkbox to repeat on that weekday
5. Press Enter while title is focused, or click Add Event
6. Press Esc to close without saving

Manage alerts from tray menu:

- Edit Alerts
- Todays Alerts

## Set Hotkeys

1. Right-click tray icon -> Hotkey
2. Click inside:
   - Hotkey for open log
   - Hotkey for add alert
3. Press desired key combination
4. Click Ok

Defaults:

- Log: Control+Scroll Lock
- Add Alert: Pause/Break

Hotkeys are saved under HKCU\\SOFTWARE\\WinquantaCore.

---

## TLDR: Other Options

### Optional Config Settings

Use these in appsettings.json only if you want the related feature:

```json
{
  "accomplishmentsFilename": "c:/quanta/accomplishments.txt",
  "userstoriesfilename": "c:/quanta/userstories.json",
  "sprintsfilename": "c:/quanta/sprints.json",
  "aiTimeReportGuide": "c:/quanta/ai-time-report-guide.md"
}
```

- alertsfilename: custom path for alerts storage (default is c:/quanta/alerts.json)
- accomplishmentsFilename: enables Add Accomplishment and sets output file path
- userstoriesfilename: enables User Stories view and Log Text user story autocomplete
- sprintsfilename: enables Sprint Schedule view
- aiTimeReportGuide: required for Generate Markdown in Edit Log

### 1) Markdown Log Generation (Markdown for AI Time Reports)

From tray menu -> Edit Log:

1. Click Generate Markdown
2. Enter number of days
3. Optional: check Include Events
4. Save generated .md file

Required config key for this feature:

```json
{
  "aiTimeReportGuide": "c:/quanta/ai-time-report-guide.md"
}
```

### 2) User Stories (Optional)

Feature appears only when userstoriesfilename is configured.

Expected file format:

```json
[
  { "Id": 1231, "Name": "Set up project scaffolding", "SprintId": "1" },
  { "Id": 2232, "Name": "Create login screen", "SprintId": "1" },
  { "Id": 3334, "Name": "Implement user story grid view", "SprintId": "2" }
]
```

Tips:

- Must be valid JSON (no trailing/invalid tokens)
- Log Text User Story field supports autocomplete using this file

### 3) Accomplishments (Optional)

Feature appears only when accomplishmentsFilename is configured.

Saved format is plain text, one line per entry:

```text
04/13/26 16:10: Completed alert regression checks
04/13/26 16:45: Updated usage documentation
```

### 4) Sprint Schedule (Optional)

Feature appears only when sprintsfilename is configured.

Expected file format:

```json
[
  {
    "Id": 1,
    "Name": "Sprint 66",
    "StartDate": "2025-04-07",
    "EndDate": "2025-04-25",
    "ProjectName": "Project name"
  },
  {
    "Id": 2,
    "Name": "Sprint 67",
    "StartDate": "2025-04-28",
    "EndDate": "2025-05-16",
    "ProjectName": "Project name"
  }
]
```

Tips:

- Use valid date values (YYYY-MM-DD recommended)
- Active sprint rows are highlighted in the schedule view
