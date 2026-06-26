using PlaywrightCSharpDemo.Pages;

namespace PlaywrightCSharpDemo.Tests;

[TestFixture]
[Category("FormValidation")]
public class FormValidationTests : BaseTest
{
    private FormPage _formPage = null!;

    [SetUp]
    public new async Task SetUp()
    {
        await base.SetUp();
        _formPage = new FormPage(Page);
    }

    // ── Checkbox Tests ─────────────────────────────────────────────

    [Test]
    [Description("First checkbox should be unchecked by default")]
    public async Task Checkbox_DefaultState_ShouldBeUnchecked()
    {
        await _formPage.NavigateToCheckboxesAsync(Config.BaseUrl);
        var isChecked = await _formPage.IsCheckboxCheckedAsync();

        ExtentTest.Info($"Checkbox default state checked: {isChecked}");
        Assert.That(isChecked, Is.False, "First checkbox should be unchecked by default");
    }

    [Test]
    [Description("Clicking checkbox should toggle its state")]
    public async Task Checkbox_OnClick_ShouldToggleState()
    {
        await _formPage.NavigateToCheckboxesAsync(Config.BaseUrl);
        var initialState = await _formPage.IsCheckboxCheckedAsync();

        await _formPage.ToggleCheckboxAsync();
        var afterToggle = await _formPage.IsCheckboxCheckedAsync();

        ExtentTest.Info($"Initial: {initialState}, After toggle: {afterToggle}");
        Assert.That(afterToggle, Is.Not.EqualTo(initialState), "Checkbox state should change after click");
    }

    // ── Dynamic Controls Tests ─────────────────────────────────────

    [Test]
    [Description("Input should be enabled after clicking Enable button")]
    public async Task DynamicInput_AfterEnable_ShouldBeEditable()
    {
        await _formPage.NavigateToDynamicControlsAsync(Config.BaseUrl);
        await _formPage.EnableInputAsync();

        var isEnabled = await _formPage.IsInputEnabledAsync();
        var message = await _formPage.GetMessageAsync();

        ExtentTest.Info($"Input enabled: {isEnabled}, Message: {message}");
        Assert.That(isEnabled, Is.True, "Input should be enabled");
        Assert.That(message, Does.Contain("It's enabled!"));
    }

    [Test]
    [Description("Enabled input should accept typed text")]
    public async Task DynamicInput_WhenEnabled_ShouldAcceptText()
    {
        await _formPage.NavigateToDynamicControlsAsync(Config.BaseUrl);
        await _formPage.EnableInputAsync();

        const string testText = "Playwright C# Test";
        await _formPage.TypeInDynamicInputAsync(testText);
        var typedValue = await Page.Locator("#input-example input").InputValueAsync();

        ExtentTest.Info($"Typed value: {typedValue}");
        Assert.That(typedValue, Is.EqualTo(testText));
    }

    // ── Inputs Page Tests ──────────────────────────────────────────

    [Test]
    [Description("Number input should accept numeric values")]
    public async Task NumberInput_WithValidNumber_ShouldSetValue()
    {
        await _formPage.NavigateToInputsAsync(Config.BaseUrl);
        await _formPage.EnterNumberAsync("42");

        var value = await _formPage.GetNumberInputValueAsync();
        ExtentTest.Info($"Number input value: {value}");
        Assert.That(value, Is.EqualTo("42"));
    }
}
