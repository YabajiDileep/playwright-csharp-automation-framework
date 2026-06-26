namespace PlaywrightCSharpDemo.Models;

public class TestConfig
{
    public string BaseUrl { get; set; } = "https://the-internet.herokuapp.com";
    public string ApiBaseUrl { get; set; } = "https://reqres.in/api";
    public string Username { get; set; } = "tomsmith";
    public string Password { get; set; } = "SuperSecretPassword!";
    public string Browser { get; set; } = "chromium";
    public bool Headless { get; set; } = true;
    public int TimeoutMs { get; set; } = 30000;
}
