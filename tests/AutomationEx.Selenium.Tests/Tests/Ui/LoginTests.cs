using AutomationEx.Selenium.Tests.Pages;
using AutomationEx.Selenium.Tests.Fixtures;

namespace AutomationEx.Selenium.Tests.Tests.Ui;


[Category("UI")]
public class LoginTests : BaseUiTest
{
    private LoginPage _login = null!;
    private TestUser? _user;

    [SetUp]
    public void OpenLoginPage()
    {
        _login = new LoginPage(Driver);
        _login.GoTo();
        _login.WaitUntilLoaded();
    }

    [TearDown]
    public async Task DeleteTestUser()
    {
        if (_user is not null)
            await AccountApi.DeleteAsync(_user);
    }

    [Test, Description("TC-02 Login with correct email and password")]
    public async Task Login_WithValidCredentials_ThenDelete()
    {
        var user = await CreateUserAsync();

        _login.Login(user.Email, user.Password);
        Assert.That(_login.Header.WaitForLoggedInAs(), Does.Contain(user.Name));

        _login.Header.DeleteAccount();
        var deleted = new AccountDeletedPage(Driver);
        deleted.WaitUntilLoaded();
        Assert.That(deleted.HeadingText(), Is.EqualTo("ACCOUNT DELETED!").IgnoreCase);
        deleted.Continue();
    }

    [Test, Description("TC-03 Login with incorrect email and password")]
    public void Login_WithInvalidCreds_ShowsError()
    {
        _login.Login("nobody@example.com", "wrong-password");

        Assert.That(_login.WaitForError().Displayed, Is.True);
    }

    [Test, Description("TC-04 Logout user")]
    public async Task Logout_ReturnsToLoginPage()
    {
        var user = await CreateUserAsync();

        _login.Login(user.Email, user.Password);
        Assert.That(_login.Header.WaitForLoggedInAs(), Does.Contain(user.Name));

        _login.Header.Logout();
        Assert.DoesNotThrow(_login.WaitUntilLoaded);
    }

    [Test, Description("Login with empty fields is blocked by required-field validation")]
    public void Login_WithEmptyFields_ShowsRequiredValidation()
    {
        _login.Login("", "");

        Assert.That(_login.EmailValidationMessage(), Is.Not.Empty);
        Assert.DoesNotThrow(_login.WaitUntilLoaded);
    }

    private async Task<TestUser> CreateUserAsync()
    {
        var user = TestUser.Random();
        await AccountApi.CreateAsync(user);
        _user = user;
        return user;
    }
}
