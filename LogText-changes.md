# LogText.cs - Proposed Changes

## 1. Add Using Directives

Add the following `using` statements at the top of `LogText.cs`:

```csharp
using Microsoft.Extensions.Configuration;
using Quanta.Core.Service;
using System.Collections.Generic;
using System.IO;
```

---

## 2. Add Field

Add a private field after `private bool isValid;`:

```csharp
private UserStoryService _userStoryService = new UserStoryService();
```

---

## 3. Add Helper Method

Add the following private method to the class:

```csharp
private void LoadUserStoryAutoComplete()
{
    if (!File.Exists("appsettings.json")) return;

    var config = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    var filePath = config.GetValue<string>("userstoriesfilename");

    if (string.IsNullOrWhiteSpace(filePath)) return;
    if (!File.Exists(filePath)) return;

    List<UserStory> stories;
    try
    {
        stories = _userStoryService.GetUserStories(filePath);
    }
    catch (Exception ex)
    {
        var dateNow = DateTime.Now.ToString("MM/dd/yy HH:mm");
        Console.WriteLine($"{dateNow}, LoadUserStoryAutoComplete: {ex.Message}");
        return;
    }

    if (stories == null || stories.Count == 0) return;

    var source = new AutoCompleteStringCollection();
    foreach (var story in stories)
    {
        source.Add(story.Id.ToString());
    }

    txtUsId.AutoCompleteMode = AutoCompleteMode.Suggest;
    txtUsId.AutoCompleteSource = AutoCompleteSource.CustomSource;
    txtUsId.AutoCompleteCustomSource = source;
}
```

> **Note:** Check the correct property name on `UserStory` for the ID — this assumes `story.Id`.  
> Confirm the type returned by `GetUserStories` matches `List<UserStory>`.

---

## 4. Update Constructor

Update the constructor to call the new helper method:

```csharp
public LogText()
{
    InitializeComponent();
    LoadUserStoryAutoComplete();
}
```

---

## Summary of Failure Cases Handled

| Scenario | How Handled |
|---|---|
| `userstoriesfilename` key missing or blank | `string.IsNullOrWhiteSpace` guard — early return |
| User stories file missing | `File.Exists` guard — early return; also prevents `CreateIfDoesNotExist` side-effect |
| Stories list empty or null | Explicit null/count check — early return |
| Corrupt JSON or file permissions | `try/catch` around `GetUserStories` only — logs to console, early return |

---

## No Changes Required

- `LogText.Designer.cs` — autocomplete set programmatically
- `ValidateNumeric` — still works; autocomplete values are numeric IDs only
- `txtUsId` required/optional state — unchanged (remains optional)
