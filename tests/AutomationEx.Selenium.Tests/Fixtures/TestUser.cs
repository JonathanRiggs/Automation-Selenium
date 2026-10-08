using Bogus;

namespace AutomationEx.Selenium.Tests.Fixtures;

public record TestUser(string Name, string Email, string Password, string FirstName, string LastName)
{
    public static TestUser Random()
    {
        var faker = new Faker();
        var first = faker.Name.FirstName();
        var last = faker.Name.LastName();
        return new TestUser(
            Name: $"{first} {last}",
            Email: $"ae_{Guid.NewGuid():N}@example.com",
            Password: faker.Internet.Password(12),
            FirstName: first,
            LastName: last);
    }
}
