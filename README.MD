# Quanta

**Quanta - Because your thoughts move faster than your mouse.**

A lightning-fast Windows productivity tool for quick notes and calendar alerts.

![.NET](https://img.shields.io/badge/.NET-9.0-blue)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey)
![License](https://img.shields.io/badge/license-MIT-green)

## What is Quanta?

Quanta is a minimalist system tray application designed for **speed and simplicity**. No fancy interfaces, no complex features�just fast note-taking and reliable calendar alerts. Think of it as "BlinkNote" meets "BlinkCal"�capture your thoughts and schedule reminders in seconds, not minutes.

**Perfect for:**
- Quick work logs and meeting notes
- Simple daily journaling
- Time-based reminders and alerts
- Task tracking without the overhead
- Anyone who values speed over features

## Core Features

### ⚡ Lightning-Fast Note Logging
- **One-Click Entry**: Global hotkey opens log window instantly
- **Auto-Timestamps**: Every entry automatically stamped with date/time
- **Smart Highlighting**: 
  - Each day gets a unique color for easy scanning
  - Keywords like `Work:`, `Task:`, `Meeting:` are highlighted
  - Hashtags (`#important`, `#followup`) stand out visually
- **Instant Search**: Real-time highlighting as you type
- **No Formatting Overhead**: Plain text with smart visual aids

### ⏰ Simple Calendar Alerts
- **Quick Scheduling**: Set alerts in seconds
- **Reliable Notifications**: Toast-style popups at scheduled times
- **Repeating Support**: Daily, weekly, or custom recurring alerts
- **Today's View**: See what's coming up without clutter

### 🎯 System Tray Integration
- Lives quietly in your system tray
- One click to add notes or alerts
- Customizable global hotkeys
- Minimal memory footprint

## Why Quanta?

**Traditional note apps are too slow.** Opening Notepad, finding the file, scrolling to the bottom, typing the date...by then you've forgotten what you wanted to write.

**Traditional calendar apps are too complex.** Multiple clicks, forms, categories, colors, notifications settings...you just wanted a simple reminder.

**Quanta is different:** It's always one hotkey away. Open, type, done. That's it.

## Installation

### Quick Start (5 minutes)

1. **Install .NET 9 Runtime** (if not already installed)
   - [Download .NET 9 Runtime ⚡](https://dotnet.microsoft.com/download/dotnet/9.0)
   - Choose: ".NET Desktop Runtime 9.0.x - Windows x64"

2. **Download Quanta**
   - [Download latest release ⚡](https://github.com/dwilliams459/Quanta/releases)
   - Extract the ZIP file

3. **Run `Quanta.Core.Windows.exe`**
   - The app will appear in your system tray
   - First launch creates default config files in `C:\quanta\`

4. **That's it!** Press `Scroll Lock` to start logging.

### Building from Source

```bash
# Clone the repository
git clone https://github.com/dwilliams459/Quanta.git
cd Quanta

# Build
dotnet build --configuration Release

# Run
cd Quanta.Core.Windows\bin\Release\net9.0-windows7.0
Quanta.Core.Windows.exe
```

## Usage

### Quick Note Entry (The Fast Way)

1. **Press `Scroll Lock`** (or your configured hotkey)
2. Type your note - textbox automatically selected
3. Press Enter or click Save
4. Done!

**Example entries:**
```
Work: Fixed the login bug
Meeting: Discussed Q1 roadmap with Sarah
Task: Call vendor about shipment #urgent
Finished the Peterson report #completed
```

### Viewing & Editing Your Log

1. **Right-click tray icon → "Edit Log"**
2. See all entries with:
   - Color-coded dates (each day gets a unique color)
   - Highlighted keywords (`Work:`, `Task:`, etc.)
   - Highlighted hashtags (`#urgent`, `#followup`)
3. **Search**: Type in search box to highlight matching text
4. Edit directly in the window
5. Click Save

**Pro Tip:** Any word ending with `:` becomes a highlighted keyword. Create your own categories like `Client:`, `Personal:`, `Bug:`, etc.

### Creating Alerts

**The Quick Way:**
1. **Press `Shift + Scroll Lock`** (alert hotkey)
2. Fill in:
   - Title (e.g., "Team standup")
   - Date & Time
   - Optional: End time, repeat settings
3. Click Save

**Managing Alerts:**
- **Edit All Alerts**: Right-click tray → "Edit Alerts"
- **View Today**: Right-click tray → "Today's Alerts"
- **Notifications**: Popup appears automatically at scheduled time

### Customizing Hotkeys (not working consitantly)

1. Right-click tray icon → "Hotkey"
2. Click in the text box
3. Press your desired key combination
4. Click OK

**Recommended keys:**
- Function keys (F9, F10, F11, F12)
- Scroll Lock (rarely used otherwise)
- Pause/Break
- Modified keys (Ctrl+F12, Alt+Space, etc.)

## How It Works

### The Log Format

Quanta uses a simple text format:

```
MM/DD/YY HH:MM  [optional-keyword:] Your note text here #hashtags
```

**Real examples:**
```
01/15/24 09:30  Work: Started code review for PR #453
01/15/24 10:15  Meeting: Discussed budget with finance team
01/15/24 11:00  Lunch with Mike #networking
01/15/24 14:30  Task: Submitted expense report #done
```

**When displayed:**
- `01/15/24 09:30` - Highlighted in a unique color per date
- `Work:` - Highlighted keyword
- `#done` - Highlighted hashtag

### File Locations

By default, Quanta stores files in `C:\quanta\`:

| File | Purpose |
|------|---------|
| `worklog.txt` | Your log entries |
| `alerts.json` | Scheduled alerts |
| `sprints.json` | Sprint schedule data (optional) |
| `appsettings.json` | Configuration |

**Change locations:** Edit `appsettings.json` in the app folder.

## Configuration

Edit `appsettings.json` to customize paths:

```json
{
  "logFilename": "c:/quanta/worklog.txt",
  "alertsfilename": "c:/quanta/alerts.json",
  "sprintsfilename": "c:/quanta/sprints.json"
}
```

**Note:** Use forward slashes (`/`) in paths, even on Windows.

## Keyboard Shortcuts

| Action | Default Hotkey | Customizable |
|--------|---------------|--------------|
| Quick log entry | `Scroll Lock` | ✅ Yes |
| Add alert | `Shift + Scroll Lock` | ✅ Yes |
| Left-click tray icon | Quick log entry | No |
| Right-click tray icon | Full menu | No |

## Tips for Maximum Speed

### Logging Best Practices

1. **Create consistent keywords**
   - Use 4-8 character codes: `WORK:`, `MTG:`, `CALL:`
   - Stick to the same keywords daily

2. **Use hashtags liberally**
   - `#urgent`, `#waiting`, `#done`, `#idea`
   - Searchable later

3. **Keep entries short**
   - One line per thought
   - Add detail only if needed

4. **Use the hotkey**
   - Don't click the tray icon
   - Muscle memory = faster logging

### Alert Best Practices

1. **Set alerts slightly early**
   - 5-10 minutes before events
   - Time to wrap up current task

2. **Use repeating alerts**
   - Daily standups
   - Weekly reviews

3. **Review regularly**
   - Check "Today's Alerts" each morning
   - Clean up old/completed alerts weekly

## Troubleshooting

### App won't start
- ✅ Install [.NET 9 Runtime](https://dotnet.microsoft.com/download/dotnet/9.0)
- ✅ Check Windows Event Viewer for errors
- ✅ Ensure `C:\quanta\` folder is writable

### Hotkey not working (Not consistently working)
- ❌ Another app may be using the same key
- ❌ Try a different key combination
- ❌ Check if Quanta is running (look in system tray)

### Alerts not firing
- ✅ Verify Quanta is running in system tray
- ✅ Check system time is correct
- ✅ View "Today's Alerts" to verify alert exists

### Can't save log
- ✅ Check file permissions on `C:\quanta\`
- ✅ Ensure disk has free space
- ✅ File isn't open in another program

### Duplicate entries appearing
- There's a bug where forms call `.Show()` twice
- Close and restart Quanta if this happens
- This will be fixed in the next release

## Project Structure

```
Quanta/
├── Quanta.Core.Common/      # Shared utilities
├── Quanta.Core.Domain/      # Data models
│   ├── Alert.cs             # Alert definition
│   ├── LogTask.cs           # Task tracking
│   └── SprintSchedule.cs    # Sprint data
├── Quanta.Core.Service/     # Business logic
│   ├── AlertService.cs      # Alert management
│   ├── LogService.cs        # Log file operations
│   ├── FileLogService.cs    # File logging
│   └── ScheduleService.cs   # Sprint schedules
└── Quanta.Core.Windows/     # WinForms UI
    ├── MainForm.cs          # System tray & main app
    ├── LogText.cs           # ⚡ Quick log entry
    ├── ViewLog.cs           # Full log editor
    ├── AddAlert.cs          # Alert creation
    ├── ViewAlerts.cs        # Alert management
    ├── ViewOnlyAlerts.cs    # Today's alerts view
    ├── ViewSchedule.cs      # Sprint viewer
    └── ToastAlert.cs        # Notification popup
```

## System Requirements

- **OS**: Windows 7 or later
- **Runtime**: .NET 9.0 or later ([Download ⚡](https://dotnet.microsoft.com/download/dotnet/9.0))
- **Disk**: ~50MB
- **RAM**: Minimal (runs in system tray)

## Technologies

- **.NET 9.0**: Core framework
- **Windows Forms**: Native UI
- **NHotkey**: Global hotkey support
- **Newtonsoft.Json**: Data storage
- **Microsoft.Extensions.Configuration**: Settings management

## Philosophy

Quanta follows three principles:

1. **Speed First**: Every feature is designed for minimal clicks/keystrokes
2. **Simplicity Always**: No feature bloat, no complex UI
3. **Reliability Matters**: Alerts fire when scheduled, logs save correctly

We deliberately **don't** include:
- ❌ Rich text formatting (slows you down)
- ❌ Cloud sync (adds complexity)
- ❌ Mobile app (not the use case)
- ❌ Categories/tags (use keywords and hashtags instead)
- ❌ Statistics/reports (keeps it simple)

If you need those features, traditional apps like OneNote or Outlook might be better fits.

## Known Issues

- Icon may not update on all forms (legacy embedded icons)

## Contributing

Contributions welcome! Please:
1. Keep the "speed & simplicity" philosophy
2. Test on Windows 10 and 11
3. Follow existing code patterns
4. Update README for new features

## Roadmap

**Not planned:** We intentionally avoid feature creep.

**Considering:**
- [ ] UI improvements.
- [ ] Export log to CSV/Markdown
- [ ] Optional sound for alert notifications
- [ ] Portable mode (store files in app folder)
- [ ] Dark mode for forms

**Never adding:**
- ❌ Cloud features
- ❌ Mobile apps
- ❌ Rich text editing
- ❌ Complex categorization

## License

MIT License - See LICENSE file for details

## Support

- 📧 [Open an issue](https://github.com/dwilliams459/Quanta/issues)
- 💬 Discussions welcome in Issues tab

---


