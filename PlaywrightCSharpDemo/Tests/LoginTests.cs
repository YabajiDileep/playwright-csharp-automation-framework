using PlaywrightCSharpDemo.Pages;

namespace PlaywrightCSharpDemo.Tests;

[TestFixture]
[Category("Login")]
public class LoginTests : BaseTest
{
    private LoginPage _loginPage = null!;

    [SetUp]
    public new async Task SetUp()
    {
        await base.SetUp();
        _loginPage = new LoginPage(Page);
        await _loginPage.NavigateAsync(Config.BaseUrl);
        ExtentTest.Info("Navigated to Login page");
    }

    [Test]
    [Description("Valid credentials should log in successfully")]
    public async Task Login_WithValidCredentials_ShouldSucceed()
    {
        await _loginPage.LoginAsync(Config.Username, Config.Password);

        var isLoggedIn = await _loginPage.IsLoggedInAsync();
        var flash = await _loginPage.GetFlashMessageAsync();

        ExtentTest.Info($"Flash message: {flash}");
        Assert.That(isLoggedIn, Is.True, "Logout button should be visible after login");
        Assert.That(flash, Does.Contain("You logged into a secure area!"));
    }

    [Test]
    [Description("Invalid password should show error message")]
    public async Task Login_WithInvalidPassword_ShouldShowError()
    {
        await _loginPage.LoginAsync(Config.Username, "wrongpassword");

        var flash = await _loginPage.GetFlashMessageAsync();

        ExtentTest.Info($"Flash message: {flash}");
        Assert.That(flash, Does.Contain("Your password is invalid!"));
    }

    [Test]
    [Description("Invalid username should show error message")]
    public async Task Login_WithInvalidUsername_ShouldShowError()
    {
        await _loginPage.LoginAsync("invaliduser", Config.Password);

        var flash = await _loginPage.GetFlashMessageAsync();

        ExtentTest.Info($"Flash message: {flash}");
        Assert.That(flash, Does.Contain("Your username is invalid!"));
    }

    [Test]
    [Description("After logout, user should be redirected to login page")]
    public async Task Logout_AfterSuccessfulLogin_ShouldRedirectToLogin()
    {
        await _loginPage.LoginAsync(Config.Username, Config.Password);
        Assert.That(await _loginPage.IsLoggedInAsync(), Is.True);

        await _loginPage.LogoutAsync();

        var flash = await _loginPage.GetFlashMessageAsync();
        ExtentTest.Info($"Flash after logout: {flash}");
        Assert.That(flash, Does.Contain("You logged out of the secure area!"));
    }
}
