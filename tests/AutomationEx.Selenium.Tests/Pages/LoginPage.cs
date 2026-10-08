using OpenQA.Selenium;

namespace AutomationEx.Selenium.Tests.Pages;

public class LoginPage(IWebDriver driver) : BasePage(driver)
{
    // By is just the query description
    private static readonly By LoginHeading = By.XPath("//h2[normalize-space()='Login to your account']");
    private static readonly By Email = TestId("login-email");
    private static readonly By Password = TestId("login-password");
    private static readonly By Submit = TestId("login-button");
    private static readonly By Error = By.XPath("//p[normalize-space()='Your email or password is incorrect!']");
    public void GoTo() => Driver.Navigate().GoToUrl($"{BaseUrl}/login");
    public void WaitUntilLoaded()
    {
        WaitForUrl("/login");
        WaitForVisible(LoginHeading);
    }

    public void Login(string email, string password)
    {
        Type(Email, email);
        Type(Password, password);
        Click(Submit);
    }

    public IWebElement WaitForError() => WaitForVisible(Error);

    // Browser-native "required" message; empty when the field is valid
    public string EmailValidationMessage() => WaitForVisible(Email).GetDomProperty("validationMessage") ?? "";
}
