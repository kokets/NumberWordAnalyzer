# NumberWordAnalyzer

A .NET 8 Web API that analyzes an input string and counts occurrences of number words (`zero` to `ten`).  

---

## ?? Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)  
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) (recommended)

---

## ?? Setup & Run (Visual Studio 2022)

### 1. Clone the repository
```bash
git clone https://github.com/kokets/NumberWordAnalyzer.git
cd NumberWordAnalyzer
```

### 2. Open the solution

* Open Visual Studio 2022  
* File ? Open ? Project/Solution  
* Select `NumberWordAnalyzer.sln`

### 3. Restore NuGet packages

* Visual Studio should restore automatically  
* If not: right-click solution ? **Restore NuGet Packages**

### 4. Build the solution

* Press `Ctrl + Shift + B` or go to **Build ? Build Solution**

### 5. Run the API

* Set `NumberWordAnalyzer` as startup project  
* Press `F5` (debug) or `Ctrl + F5` (run without debugging)  
* Swagger UI will open at:  
  ```
  https://localhost:{port}/swagger
  ```

---

## ?? Running Tests

* Run from Visual Studio Test Explorer  
* Or from CLI:

```bash
dotnet test NumberWordAnalyzer.Tests/NumberWordAnalyzer.Tests.csproj --configuration Release
```

---

## ?? API Usage

### POST `/api/NumberWordAnalyzer/analyze`

Send a JSON with `"input"` to get counts of number words.

**Example request:**
```json
{
  "input": "onetwothreefourfivesixseveneightnineten"
}
```

**Example response:**
```json
{
  "zero": 0,
  "one": 1,
  "two": 1,
  "three": 1,
  "four": 1,
  "five": 1,
  "six": 1,
  "seven": 1,
  "eight": 1,
  "nine": 1,
  "ten": 1
}
```
