# Quanta.Core.Windows

Quanta is a lightweight system tray app for fast note capture and calendar alerts.

## Deployment

### Prerequisites

- Windows 10 or Windows 11
- [.NET 9+ Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/9.0)

### Run After Install

1. Start `Quanta.Core.Windows.exe`.
2. Confirm the tray icon appears.
3. Press `Scroll Lock` to open quick logging.
4. Use `Shift + Scroll Lock` to create an alert.
5. Right-click the tray icon to access settings and view logs.

## Configuration

Quanta stores its default data under `C:\quanta\`.

Typical files:

- `c:\quanta\worklog.txt`
- `c:\quanta\alerts.json`
- `c:\quanta\sprints.json`
- `[app location]\appsettings.json`

Update `appsettings.json` if you need custom file locations.

## Default Shortcuts

- `Scroll Lock`: quick log entry
- `Shift + Scroll Lock`: add alert

## Notes

- This project is intended for Windows deployment.
- Verify the target machine has permission to write to `C:\quanta\`.
- Make certain the execution path is in the environment variables.
- You will need to manually add the app to startup if you want it to run on boot.
- If hotkeys do not register, try running the app once directly before adding it to startup.
