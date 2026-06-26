using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace PlaywrightCSharpDemo.Helpers;

public static class ReportHelper
{
    private static ExtentReports? _extent;
    private static readonly string ReportPath = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "Reports", "TestReport.html");

    public static ExtentReports GetInstance()
    {
        if (_extent == null)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(ReportPath)!);
            var htmlReporter = new ExtentSparkReporter(ReportPath);
            htmlReporter.Config.DocumentTitle = "Playwright C# Test Report";
            htmlReporter.Config.ReportName = "Automation Test Results";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;

            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);
            _extent.AddSystemInfo("Tester", "Dileep");
            _extent.AddSystemInfo("Framework", "Playwright + NUnit + C#");
            _extent.AddSystemInfo("Environment", "QA");
        }
        return _extent;
    }

    public static void Flush() => _extent?.Flush();
}
