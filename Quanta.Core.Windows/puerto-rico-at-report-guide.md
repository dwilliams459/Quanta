
* ***This is a sample template file, do not use for final reports. Modify `ai-time-report-guide.md` instead according to your own report generation requirements***
* ***Modify `appsettings.json` the `aiTimeReportGuideFile` setting to specify the file location/name.***

---

# Time Entry Table Generation Guide

## Main Directive

Read the log entries from the section titled `# Log Entries - Last # Days`. Parse each entry according to the **Log Entry Format** below and convert the entries into the **Table Output Format**.

Generate both:

1. A Markdown table.
2. An Excel (`.xlsx`) file containing the same rows and columns.

Follow all parsing, writing, replacement, time-entry, and validation rules in this guide. Do not invent facts that are not supported by the log entries or other information explicitly provided with them.

This file is a guide to reading log entries and generating a table in the defined output format.  When generating, use the guidelines below.  

---

## Log Entry Format

Read the log entries in this file in the section starting with '# Log Entries - Last # Days' put directly from the chat.  This section specifies the log format.  Each week a log files is created by a custom app with time, project, description and possibly length and user story.  

Read the log entries that are placed directly in the source content under a section beginning with:    

`Log Entries - Last # Days`

#### Log Entry Pattern

```
[Time Stamp]: [Project]: [Description]  (Length) (US: ID)
```

A custom application normally generates these entries. An entry should contain:

* Timestamp
* Project
* Description
* Length
* User Story, Bug, or Work Item identifier  

Length and work-item information are optional.

### Parsing Rules

* Treat the timestamp as the date and time of the activity.
* Convert the output date to `MM/dd`.
* Determine the full day-of-week name from the date.
* Treat the text between the timestamp and description as the project only when a project value is clearly present.
* Length is expressed in hours unless the entry explicitly indicates otherwise.
* Length and work-item information may appear together in the same parentheses, for example `(1, US: 292540)`.
* Work-item prefixes may include `US`, `Bug`, or `WI`.
* Preserve the work-item identifier from the source. Do not invent one.
* If a field cannot be determined reliably, output `-` rather than guessing.
* Each generated row should make clear what work was performed, what project or item it belongs to when known, and the duration when known.

### Examples

```
04/02/26 16:58: PR: Investment Workstation daily open issue status meeting 
04/04/26 08:57: PR: DSU, user story cleanup
04/04/26 12:27: Lunch
04/04/26 12:58: PR: Participated in a refinement session with SWAT SM, PO, BA, QAs, and Developers to review and clarify requirements, ensuring alignment on scope and estimates. (1, US: 292540) 
04/04/26 13:58: AI: Worked to install and get Java tial application running. (2, US: 39485)
04/04/26 15:58: PR: Getting demo app up an running. 
```

## Table Output Format

Generate output as a table following this pattern.  Generate both the text output, but also generate an excel file with the same columns.

```
| Date | Day of Week | Project | Description | Length | User Story | User Story Name |
```

Example output format:

| Date  | Day of Week | Project | Description                                                                                                                                                   | Length    | User Story | User Story Name |
| ----- | ----------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------- | --------- | ---------- | --------------- |
| 04/02 | Thursday    | BMC     | Created the Azure search service configuration to support the genereration solution setup and retrieval workflow.                                             | 1.0 (est) | -          |                 |
| 04/02 | Thursday    | PNC     | Participated in a refinement session with SWAT SM, PO, BA, QAs, and Developers to review and clarify requirements, ensuring alignment on scope and estimates. | 1.0       | 292540     |                 |
| 04/02 | Thursday    | BMC     | Updated the genereration project plan and organized next steps for ongoing delivery activities.                                                               | 2.0       | -          |                 |

### ### Column Rules

#### Date

* Format as `MM/dd`.
* Use the date from the source timestamp.

#### Day of Week

* Use the full English day name, such as `Monday`, `Tuesday`, or `Wednesday`.
* Calculate it from the source date.

#### Project

* Use the project from the source when present.
* Apply any explicit replacement or mapping rule in this guide.
* If no project can be determined, use `-`.

#### Description

* State the actual work performed.
* Mention the subject, feature, issue, CR, User Story, Bug, or Work Item when known and relevant.
* Prefer one or two clear, specific sentences.
* A third sentence should be rare and used only when needed for clarity.
* Expand terse source text when the meaning is clear, but do not add unsupported facts.
* Use a related User Story Name to improve clarity when that name is explicitly available.
* If the source entry is malformed or does not contain enough information to create a reliable description, use `-`.
* Do not change wording solely to bypass duplicate detection.

#### Length

* Express length in hours.
* Round estimated lengths to the nearest 0.5 hour.
* Preserve an explicit source duration unless there is a clear source-supported reason to normalize its format.
* If the source does not provide a duration, estimate only when the entry contains enough context to support a reasonable estimate.
* Mark estimated durations with `(est)`, for example `1.0 (est)`.
* If there is not enough information to estimate a duration, use `-`.
* Do not invent a duration.
* A generated entry should not normally exceed 4 hours. If the source explicitly provides more than 4 hours, do not silently alter the source value; preserve it and include the issue in the validation warnings unless the longer duration is clearly justified.

#### User Story

* Copy the identifier from the source when present.
* A source work item may be labeled `US`, `Bug`, or `WI`.
* Do not invent, infer, or look up an identifier unless the required information is explicitly provided as part of the task.
* If no identifier is available, use `-`.

#### User Story Name

* Populate this column only when the work-item name is explicitly available in the supplied source material or task context.
* Do not invent a name or perform an external lookup unless specifically requested.
* If the name is unavailable, use `-`.

---

## Writing Guidance for Generated Entries

Generated descriptions should:

* Be specific enough to support the work performed.
* Describe what was created, reviewed, fixed, validated, discussed, configured, tested, analyzed, or otherwise completed.
* Avoid vague wording such as `tested` or `worked on issue` without explaining what was tested, reviewed, fixed, validated, or discussed.
* Be verbose enough to be useful, while remaining faithful to the source.
* Use related work-item names or descriptions when they are supplied and help clarify the activity.
* Prefer role names over individual names when the team-member mapping below makes the role clear.

---

### ## Activity Tracker Time Entry Rules

#### Corporate guidelines for all time generation entries.

* Descriptions must should be 1-2 sentences but specific enough to support the work performed.  Sentences should be verbose where possible but without making up content. Error on the side of longer sentences.
* Avoid vague text like “tested” or “worked on issue” without saying what was tested, reviewed, fixed, validated, or discussed.
* Do not create duplicate entries for the same day.
* Do not alter wording, punctuation, or symbols just to bypass duplicate detection.
* Do not put more than 4 hours in a single entry unless clearly justified.
* Daily standup goes to maintenance, no User Story number.
* Meetings, testing, analysis, development, validation, and working sessions should be tied to the specific CR when applicable.
* Cloning prior entries is allowed, but always verify the date, item, description, and that it is not a duplicate.
* Length should be rounded to 1/2 hour, but no more than 4 hours. If length is not provided, it can be estimated based on the description, but it should be clear that it is an estimate.  If there is no way to estimate, just leave it blank or put '-'.  Do not guess or make up a length if there is no information to support it.  

#### Preferred style examples (when CR or User story number is known):

* Reviewed defect behavior and documented retest results for work item ****.
* Analyzed issue details and validated expected results for story ****.
* Participated in CR **** working session to review testing results and next steps.

#### Regarding AI entries

When a source entry concerns AI-assisted work:

* Do not use the generic term `AI` in the generated description.
* When known, identify the actual tool instead, such as `GitHub Copilot`.
* Do not describe the activity as `research`, `evaluation`, `assessment`, `investigation`, or `training` of AI tools.
* Rewrite the description around the actual development, configuration, troubleshooting, coding, documentation, or other work performed.
* If the actual tool or activity cannot be determined from the source, do not invent it.

--- 

## Terms and Replacement Text

### Individuals in the team

People may be referenced by name in the source log. In generated descriptions, prefer their role when the role is known and doing so remains clear. 

| Referenced as | Full Name       | Role                | Team    |
| ------------- | --------------- | ------------------- | ------- |
| Abel          | Abel Wakrim     | Developer Architect | SWAT    |
| Sylvia        | Sylvia          | Scrum Master        | General |
| Sam           | Sam Reichel     | Developer           | General |
| Tristin       | Tristen Spruill | Developer           | General |
| Raja          | Raja Rapaka     | DCS                 | CGI     |

### Terms and Abbreviations replacements

Replace the following terms or phrases to the exact phrase

| Term            | Replace with exact phrase                                                                                                                                          |
| --------------- | ------------------------------------------------------------------------------------------------------------------------------------------------------------------ |
| DSU             | Daily Stand Up                                                                                                                                                     |
| PRDSU           | Attended daily standup with SWAT team                                                                                                                              |
| SWAT Refinement | Participated in a refinement session with POC, SWAT SM, PO, BA, QAs, and Developers to review and clarify requirements, ensuring alignment on scope and estimates. |

Terms:

* SWAT - The development team I am working with on the Puerto Rico (PR) project.  

---

Log Entries - Last # Days
=========================

Place log entries below this heading.
