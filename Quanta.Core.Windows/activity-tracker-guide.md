# Activity Tracker and Time Entry

## Guideline for this project

This project will be used to generate and refine time entries for the a number of Projects.  Typically one or multiple events will be added in the chat area that should be used to generate new time entries.  When generating, use the rules below.  

### Typical time entry

Often (but not always) a raw worklog is created using a custom app and individual events will be pasted into the chat and they will follow this pattern.  Length and user story are optional.  User Story may start with 'US', 'Bug', 'WI'.   

#### Input Pattern

```
[Time Stamp]: [Project]: [Description]  ([Length],us[User Story])
```

Length and user story are optional.  User Story may start with 'US', 'Bug', 'WI'.

#### Examples

```
03/25/26 16:58: PNC: Investment Workstation daily open issue status meeting 

03/26/26 08:57:  PNC: DSU, user story cleanup

03/26/26 12:27: Lunch

03/26/26 12:58: AI course: getting demo app up an running. 

03/26/26 13:58: AI: Worked to install and get Java tial application running.
```

   

### Time output format

Output for each line should be free text. Typically they will be 1 or 2 sentences, but very rarely more than 3.  However Date, day of week, Length, user story will need to be listed as well.  Generate output as a table following this pattern.  Round length to 1/2 hour, but no more than 4 hours.  

| Date | Day of Week | Project | Description                 | Length  | User Story |
| ---- | ----------- | ------- | --------------------------- | ------- | ---------- |
| 3/26 | Thursday    | PR      | I did something on this day | 2 hours | 1234       |

## Quick rule for Time Entry

Each entry should clearly show: what was done, what item it belongs to, and that the time honestly belongs there.

## Activity Tracker Time Entry Rules

* Time entries must be accurate because they support client billing, CR actuals, funding allocation, and reporting.
* Always charge time to the correct item: maintenance, enhancement, or the correct CR/work item.
* Enter time daily and keep total hours aligned with PSA.
* Descriptions must be short but specific enough to support the work performed.
* Avoid vague text like “tested” or “worked on issue” without saying what was tested, reviewed, fixed, validated, or discussed.
* Entries must reflect actual work performed and match the selected item.
* Do not create duplicate entries for the same day.
* Do not alter wording, punctuation, or symbols just to bypass duplicate detection.
* Do not put more than 4 hours in a single entry unless clearly justified.
* Do not pad or stretch time just to reach 8 hours.
* Only charge time to a CR if the work truly belongs to that CR.
* Daily standup goes to maintenance.
* General meetings go to maintenance unless they are clearly focused on a specific CR or work item.
* Meetings, testing, analysis, development, validation, and working sessions should be tied to the specific CR when applicable.
* Copying or dragging prior entries is allowed, but always verify the date, item, description, and that it is not a duplicate.
* Good entries are concise, specific, honest, and traceable.

## Writing guidance for generated entries

Generated descriptions should:

* state the actual work performed,
* mention the subject, feature, issue, or CR when known,
* stay concise,
* and avoid generic filler language.
* If you are not able to generate a description for a specific entry because something is mal-formed or missing content, just generate a '-'. 

Preferred style examples:

* Tested validation updates for CR ####.
* Reviewed defect behavior and documented retest results for work item ####.
* Analyzed issue details and validated expected results for story ####.
* Participated in CR #### working session to review testing results and next steps.

## Individuals in the team

The following people may be refered to by name, but usually the description should not be by name but by role

| Referenced as | Full Name       | Role                | Team    | p-2 |
| ------------- | --------------- | ------------------- | ------- | --- |
| Abel          | Abel Wakrim     | Developer Architect | SWAT    | .   |
| Sylvia        | Sylvia          | Scrum Master        | General |     |
| Sam           | Sam Reichel     | Developer           | General |     |
| Tristin       | Tristen Spruill | Developer           | General |     |
| Raja          | Raja Rapaka     | DCS                 | CGI     |     |

Convert the following log entries to the format as described above.
