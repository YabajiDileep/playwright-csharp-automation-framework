namespace PlaywrightCSharpDemo.Helpers;

public static class ScreenshotHelper
{
    private static readonly string ScreenshotDir = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "Reports", "Screenshots");

    public static async Task<string> CaptureAsync(IPage page, string testName)
    {
        Directory.CreateDirectory(ScreenshotDir);
        var fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
        var filePath = Path.Combine(ScreenshotDir, fileName);
        await page.ScreenshotAsync(new() { Path = filePath, FullPage = true });
        return filePath;
    }
}
