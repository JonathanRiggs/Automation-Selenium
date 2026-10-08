using System.Text.Json;

namespace AutomationEx.Selenium.Tests.Fixtures;

// Sets up and cleans up accounts through the site's API so UI tests don't depend on the signup form
public static class AccountApi
{
    private static readonly HttpClient Http = new() { BaseAddress = new Uri(TestConfig.BaseUrl) };

    public static async Task CreateAsync(TestUser user)
    {
        var code = await SendAsync(HttpMethod.Post, "/api/createAccount", new Dictionary<string, string>
        {
            ["name"] = user.Name,
            ["email"] = user.Email,
            ["password"] = user.Password,
            ["title"] = "Mr",
            ["birth_date"] = "1",
            ["birth_month"] = "January",
            ["birth_year"] = "1990",
            ["firstname"] = user.FirstName,
            ["lastname"] = user.LastName,
            ["company"] = "Test Co",
            ["address1"] = "1 Test Street",
            ["address2"] = "",
            ["country"] = "United States",
            ["zipcode"] = "10001",
            ["state"] = "New York",
            ["city"] = "New York",
            ["mobile_number"] = "5550100"
        });

        if (code != 201)
            throw new InvalidOperationException($"Could not create account {user.Email}: responseCode {code}");
    }

    // Best effort: the account may already be gone if the test deleted it through the UI
    public static Task DeleteAsync(TestUser user) => SendAsync(HttpMethod.Delete, "/api/deleteAccount", new Dictionary<string, string>
    {
        ["email"] = user.Email,
        ["password"] = user.Password
    });

    // The API always answers HTTP 200; the real status is the responseCode in the body
    private static async Task<int> SendAsync(HttpMethod method, string path, Dictionary<string, string> form)
    {
        using var request = new HttpRequestMessage(method, path) { Content = new FormUrlEncodedContent(form) };
        using var response = await Http.SendAsync(request);
        response.EnsureSuccessStatusCode();

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
        return body.RootElement.GetProperty("responseCode").GetInt32();
    }
}
