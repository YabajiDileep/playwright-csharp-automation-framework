# 🎭 Playwright C# Automation Framework

A clean, structured test automation project built with **Playwright + NUnit + C#**, covering UI login automation, form validation, API testing, Page Object Model design, and HTML reporting.

> Built as a portfolio project by **Dileep** to demonstrate QA Automation skills for SDET/Test Engineer roles.

---

## 📁 Project Structure

```
PlaywrightCSharpDemo/
├── Pages/                    # Page Object Model classes
│   ├── LoginPage.cs          # Login page interactions
│   └── FormPage.cs           # Form / input page interactions
├── Tests/                    # NUnit test classes
│   ├── BaseTest.cs           # Shared setup, teardown, ExtentReports init
│   ├── LoginTests.cs         # Login automation tests (4 tests)
│   ├── FormValidationTests.cs# Form validation tests (5 tests)
│   └── ApiTests.cs           # API tests using Playwright APIRequestContext (6 tests)
├── Models/                   # Data models
│   ├── TestConfig.cs         # Test configuration (URL, credentials, browser)
│   └── ApiModels.cs          # Serialization models for API responses
├── Helpers/                  # Utility classes
│   ├── ReportHelper.cs       # ExtentReports HTML report generator
│   └── ScreenshotHelper.cs   # On-failure screenshot capture
└── PlaywrightCSharpDemo.csproj
```

---

## ✅ Features Covered

| Feature | Implementation |
|---|---|
| **Login Automation** | Valid/invalid login, logout via POM |
| **Form Validation** | Checkboxes, dynamic controls, number inputs |
| **API Testing** | GET, POST, PUT, DELETE, Auth using Playwright's `APIRequestContext` |
| **Page Object Model** | `LoginPage`, `FormPage` — locators encapsulated, no test-side selectors |
| **HTML Reports** | ExtentReports with dark theme, pass/fail badges, system info |
| **Screenshots on Failure** | Auto-captured and embedded in the HTML report |

---

## 🚀 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Run once after clone:

```bash
dotnet build
pwsh bin/Debug/net8.0/playwright.ps1 install
```

---

## ▶️ Running Tests

```bash
# Run all tests
dotnet test

# Run by category
dotnet test --filter Category=Login
dotnet test --filter Category=FormValidation
dotnet test --filter Category=API

# Run a specific test
dotnet test --filter Name~Login_WithValidCredentials
```

---

## 📊 HTML Report

After test run, the report is generated at:

```
bin/Debug/net8.0/Reports/TestReport.html
```

Open it in any browser to see pass/fail status, logs, and failure screenshots.

---

## 🧪 Test Sites Used

| Tests | Site |
|---|---|
| Login, Form | `https://the-internet.herokuapp.com` (free Selenium practice site) |
| API | `https://reqres.in` (free REST API sandbox) |

---

## 🛠️ Tech Stack

- **Language**: C# (.NET 8)
- **Test Framework**: NUnit 3
- **Browser Automation**: Microsoft Playwright
- **API Testing**: Playwright `APIRequestContext`
- **Reporting**: ExtentReports (ExtentSparkReporter)
- **HTTP Models**: Newtonsoft.Json, RestSharp
- **CI Ready**: GitHub Actions compatible

---

## 📌 Author

**Yabaji Dileep** — B.Tech CSE, JNTU Hyderabad  
QA Automation Engineer | Playwright • Selenium • Java • C# • Postman  
[LinkedIn](https://linkedin.com) | [GitHub](https://github.com)
