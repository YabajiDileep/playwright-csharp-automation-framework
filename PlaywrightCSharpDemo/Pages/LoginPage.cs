namespace PlaywrightCSharpDemo.Pages;

/// <summary>
/// Page Object Model for the Login page (https://the-internet.herokuapp.com/login)
/// </summary>
public class LoginPage
{
    private readonly IPage _page;

    // Locators
    private ILocator UsernameInput  => _page.Locator("#username");
    private ILocator PasswordInput  => _page.Locator("#password");
    private ILocator LoginButton    => _page.Locator("button[type='submit']");
    private ILocator FlashMessage   => _page.Locator("#flash");
    private ILocator LogoutButton   => _page.Locator("a.button.secondary.radius");

    public LoginPage(IPage page) => _page = page;

    public async Task NavigateAsync(string baseUrl) =>
        await _page.GotoAsync($"{baseUrl}/login");

    public async Task LoginAsync(string username, string password)
    {
        await UsernameInput.FillAsync(username);
        await PasswordInput.FillAsync(password);
        await LoginButton.ClickAsync();
    }

    public async Task<string> GetFlashMessageAsync() =>
        (await FlashMessage.InnerTextAsync()).Trim();

    public async Task<bool> IsLoggedInAsync() =>
        await LogoutButton.IsVisibleAsync();

    public async Task LogoutAsync() =>
        await LogoutButton.ClickAsync();
}
