using Newtonsoft.Json;
using PlaywrightCSharpDemo.Models;

namespace PlaywrightCSharpDemo.Tests;

/// <summary>
/// API Tests using Playwright's built-in APIRequestContext against reqres.in (free public API).
/// </summary>
[TestFixture]
[Category("API")]
public class ApiTests : BaseTest
{
    private IAPIRequestContext _apiContext = null!;

    [SetUp]
    public new async Task SetUp()
    {
        await base.SetUp();
        _apiContext = await Playwright.APIRequest.NewContextAsync(new()
        {
            BaseURL = Config.ApiBaseUrl,
            ExtraHTTPHeaders = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" },
                { "Accept", "application/json" }
            }
        });
        ExtentTest.Info("API context created — base URL: " + Config.ApiBaseUrl);
    }

    [TearDown]
    public new async Task TearDown()
    {
        await _apiContext.DisposeAsync();
        await base.TearDown();
    }

    // ── GET Tests ──────────────────────────────────────────────────

    [Test]
    [Description("GET /users/2 should return status 200 and correct user data")]
    public async Task GetUser_WithValidId_ShouldReturn200()
    {
        var response = await _apiContext.GetAsync("/users/2");
        var body = await response.TextAsync();
        var user = JsonConvert.DeserializeObject<UserResponse>(body);

        ExtentTest.Info($"Status: {response.Status}, Body: {body}");

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(user?.Data?.Id, Is.EqualTo(2));
        Assert.That(user?.Data?.Email, Is.Not.Null.And.Not.Empty);
    }

    [Test]
    [Description("GET /users?page=2 should return a list of users")]
    public async Task GetUsers_Page2_ShouldReturnUserList()
    {
        var response = await _apiContext.GetAsync("/users?page=2");
        var body = await response.TextAsync();
        var users = JsonConvert.DeserializeObject<UsersListResponse>(body);

        ExtentTest.Info($"Status: {response.Status}, Users count: {users?.Data?.Count}");

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(users?.Data, Is.Not.Null);
        Assert.That(users!.Data.Count, Is.GreaterThan(0));
        Assert.That(users.Page, Is.EqualTo(2));
    }

    [Test]
    [Description("GET /users/999 should return 404 for non-existent user")]
    public async Task GetUser_WithInvalidId_ShouldReturn404()
    {
        var response = await _apiContext.GetAsync("/users/999");

        ExtentTest.Info($"Status: {response.Status}");
        Assert.That(response.Status, Is.EqualTo(404));
    }

    // ── POST Tests ─────────────────────────────────────────────────

    [Test]
    [Description("POST /users should create a new user and return 201")]
    public async Task CreateUser_WithValidPayload_ShouldReturn201()
    {
        var payload = new CreateUserRequest { Name = "Dileep", Job = "QA Automation Engineer" };
        var response = await _apiContext.PostAsync("/users", new()
        {
            DataObject = payload
        });

        var body = await response.TextAsync();
        var created = JsonConvert.DeserializeObject<CreateUserResponse>(body);

        ExtentTest.Info($"Status: {response.Status}, Created ID: {created?.Id}");

        Assert.That(response.Status, Is.EqualTo(201));
        Assert.That(created?.Name, Is.EqualTo("Dileep"));
        Assert.That(created?.Job, Is.EqualTo("QA Automation Engineer"));
        Assert.That(created?.Id, Is.Not.Null.And.Not.Empty);
    }

    // ── PUT Test ───────────────────────────────────────────────────

    [Test]
    [Description("PUT /users/2 should update user and return 200")]
    public async Task UpdateUser_WithValidPayload_ShouldReturn200()
    {
        var payload = new CreateUserRequest { Name = "Dileep Updated", Job = "SDET" };
        var response = await _apiContext.PutAsync("/users/2", new()
        {
            DataObject = payload
        });

        var body = await response.TextAsync();
        var updated = JsonConvert.DeserializeObject<CreateUserResponse>(body);

        ExtentTest.Info($"Status: {response.Status}, Updated name: {updated?.Name}");

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(updated?.Name, Is.EqualTo("Dileep Updated"));
    }

    // ── DELETE Test ────────────────────────────────────────────────

    [Test]
    [Description("DELETE /users/2 should return 204 No Content")]
    public async Task DeleteUser_WithValidId_ShouldReturn204()
    {
        var response = await _apiContext.DeleteAsync("/users/2");

        ExtentTest.Info($"Status: {response.Status}");
        Assert.That(response.Status, Is.EqualTo(204));
    }

    // ── Auth / Login Test ──────────────────────────────────────────

    [Test]
    [Description("POST /login with valid credentials should return token")]
    public async Task Login_WithValidCredentials_ShouldReturnToken()
    {
        var payload = new LoginRequest { Email = "eve.holt@reqres.in", Password = "cityslicka" };
        var response = await _apiContext.PostAsync("/login", new()
        {
            DataObject = payload
        });

        var body = await response.TextAsync();
        var login = JsonConvert.DeserializeObject<LoginResponse>(body);

        ExtentTest.Info($"Status: {response.Status}, Token: {login?.Token}");

        Assert.That(response.Status, Is.EqualTo(200));
        Assert.That(login?.Token, Is.Not.Null.And.Not.Empty);
    }
}
