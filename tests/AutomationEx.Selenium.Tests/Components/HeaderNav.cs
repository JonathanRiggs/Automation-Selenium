using OpenQA.Selenium;

namespace AutomationEx.Selenium.Tests.Components;

public class HeaderNav(IWebDriver driver) : UiComponent(driver)
{
    private static readonly By HomeLink = By.LinkText("Home");
    private static readonly By ProductsLink = By.LinkText("Products");
    private static readonly By CartLink = By.LinkText("Cart");
    private static readonly By SignupLoginLink = By.LinkText("Signup / Login");
    private static readonly By TestCasesLink = By.LinkText("Test Cases");
    private static readonly By ContactUsLink = By.LinkText("Contact us");
    private static readonly By LogoutLink = By.LinkText("Logout");
    private static readonly By DeleteAccountLink = By.LinkText("Delete Account");
    private static readonly By LoggedInAs = By.XPath("//a[contains(normalize-space(), 'Logged in as')]");

    public void GoToHome() => Click(HomeLink);
    public void GoToProducts() => Click(ProductsLink);
    public void GoToCart() => Click(CartLink);
    public void GoToSignupLogin() => Click(SignupLoginLink);
    public void GoToTestCases() => Click(TestCasesLink);
    public void GoToContactUs() => Click(ContactUsLink);
    public void Logout() => Click(LogoutLink);
    public void DeleteAccount() => Click(DeleteAccountLink);

    public string WaitForLoggedInAs() => WaitForVisible(LoggedInAs).Text.Trim();
}
