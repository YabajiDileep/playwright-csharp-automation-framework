namespace PlaywrightCSharpDemo.Pages;

/// <summary>
/// Page Object for the Dynamic Controls and Inputs pages — demonstrates form validation POM.
/// </summary>
public class FormPage
{
    private readonly IPage _page;

    // Dropdown (Checkboxes page)
    private ILocator Checkbox        => _page.Locator("#checkbox");
    private ILocator CheckboxForm    => _page.Locator("#checkboxes");

    // Dynamic Controls
    private ILocator EnableButton    => _page.Locator("button:has-text('Enable')");
    private ILocator DisableButton   => _page.Locator("button:has-text('Disable')");
    private ILocator DynamicInput    => _page.Locator("#input-example input");
    private ILocator Message         => _page.Locator("#message");

    // Inputs
    private ILocator NumberInput     => _page.Locator("input[type='number']");

    public FormPage(IPage page) => _page = page;

    // ── Checkbox page ──────────────────────────────────────────────
    public async Task NavigateToCheckboxesAsync(string baseUrl) =>
        await _page.GotoAsync($"{baseUrl}/checkboxes");

    public async Task<bool> IsCheckboxCheckedAsync() =>
        await Checkbox.IsCheckedAsync();

    public async Task ToggleCheckboxAsync() =>
        await Checkbox.ClickAsync();

    // ── Dynamic Controls page ──────────────────────────────────────
    public async Task NavigateToDynamicControlsAsync(string baseUrl) =>
        await _page.GotoAsync($"{baseUrl}/dynamic_controls");

    public async Task EnableInputAsync()
    {
        await EnableButton.ClickAsync();
        await _page.WaitForSelectorAsync("#message:has-text(\"It's enabled!\")",
            new() { Timeout = 10000 });
    }

    public async Task<bool> IsInputEnabledAsync() =>
        await DynamicInput.IsEnabledAsync();

    public async Task<string> GetMessageAsync() =>
        (await Message.InnerTextAsync()).Trim();

    public async Task TypeInDynamicInputAsync(string text) =>
        await DynamicInput.FillAsync(text);

    // ── Inputs page ───────────────────────────────────────────────
    public async Task NavigateToInputsAsync(string baseUrl) =>
        await _page.GotoAsync($"{baseUrl}/inputs");

    public async Task EnterNumberAsync(string value) =>
        await NumberInput.FillAsync(value);

    public async Task<string> GetNumberInputValueAsync() =>
        await NumberInput.InputValueAsync();
}
