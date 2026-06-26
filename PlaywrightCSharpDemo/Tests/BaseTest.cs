using AventStack.ExtentReports;
using PlaywrightCSharpDemo.Helpers;
using PlaywrightCSharpDemo.Models;

namespace PlaywrightCSharpDemo.Tests;

[TestFixture]
public abstract class BaseTest
{
    protected IPlaywright Playwright = null!;
    protected IBrowser Browser = null!;
    protected IPage Page = null!;
    protected IBrowserContext Context = null!;
    protected ExtentTest ExtentTest = null!;
    protected TestConfig Config = new();

    private static readonly ExtentReports Extent = ReportHelper.GetInstance();

    [OneTimeSetUp]
    public virtual Task OneTimeSetup() => Task.CompletedTask;

    [SetUp]
    public async Task SetUp()
    {
        ExtentTest = Extent.CreateTest(TestContext.CurrentContext.Test.Name);

        Playwright = await Microsoft.Playwright.Playwright.CreateAsync();

        Browser = Config.Browser.ToLower() switch
        {
            "firefox" => await Playwright.Firefox.LaunchAsync(new() { Headless = Config.Headless }),
            "webkit"  => await Playwright.Webkit.LaunchAsync(new()  { Headless = Config.Headless }),
            _         => await Playwright.Chromium.LaunchAsync(new() { Headless = Config.Headless })
        };

        Context = await Browser.NewContextAsync(new()
        {
            ViewportSize = new() { Width = 1280, Height = 720 },
            RecordVideoDir = null
        });

        Page = await Context.NewPageAsync();
        Page.SetDefaultTimeout(Config.TimeoutMs);

        ExtentTest.Info("Browser launched: " + Config.Browser);
    }

    [TearDown]
    public async Task TearDown()
    {
        var outcome = TestContext.CurrentContext.Result.Outcome.Status;

        if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            var screenshotPath = await ScreenshotHelper.CaptureAsync(Page, TestContext.CurrentContext.Test.Name);
            ExtentTest.Fail("Test Failed")
                      .AddScreenCaptureFromPath(screenshotPath);
        }
        else
        {
            ExtentTest.Pass("Test Passed");
        }

        await Page.CloseAsync();
        await Context.CloseAsync();
        await Browser.CloseAsync();
        Playwright.Dispose();
    }

    [OneTimeTearDown]
    public void OneTimeTearDown() => ReportHelper.Flush();
}
