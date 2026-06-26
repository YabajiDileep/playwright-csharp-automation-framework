using Newtonsoft.Json;

namespace PlaywrightCSharpDemo.Models;

public class UserResponse
{
    [JsonProperty("data")]
    public UserData? Data { get; set; }

    [JsonProperty("support")]
    public Support? Support { get; set; }
}

public class UsersListResponse
{
    [JsonProperty("page")]
    public int Page { get; set; }

    [JsonProperty("per_page")]
    public int PerPage { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("data")]
    public List<UserData> Data { get; set; } = new();
}

public class UserData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("first_name")]
    public string FirstName { get; set; } = string.Empty;

    [JsonProperty("last_name")]
    public string LastName { get; set; } = string.Empty;

    [JsonProperty("avatar")]
    public string Avatar { get; set; } = string.Empty;
}

public class Support
{
    [JsonProperty("url")]
    public string Url { get; set; } = string.Empty;

    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;
}

public class CreateUserRequest
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("job")]
    public string Job { get; set; } = string.Empty;
}

public class CreateUserResponse
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("job")]
    public string Job { get; set; } = string.Empty;

    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;

    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; } = string.Empty;
}

public class LoginRequest
{
    [JsonProperty("email")]
    public string Email { get; set; } = string.Empty;

    [JsonProperty("password")]
    public string Password { get; set; } = string.Empty;
}

public class LoginResponse
{
    [JsonProperty("token")]
    public string Token { get; set; } = string.Empty;
}
