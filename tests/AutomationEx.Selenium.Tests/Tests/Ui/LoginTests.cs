using OpenQA.Selenium;
using AutomationEx.Selenium.Tests.Fixtures;

namespace AutomationEx.Selenium.Tests.Ui;

[Category("UI")]
public class LoginTests : BaseUiTest
{
    [Test, Description("TC-03 Login with wrong email password")]
    public void Login_WithInvalidCreds_ShowsError()
    {
        Driver.Navigate().GoToUrl($"{BaseUrl}/login");

        Wait.Until(d => d.FindElement(TestId("login-email"))).SendKeys("nobody@example.com");
        Driver.FindElement(TestId("login-password")).SendKeys("wrong-password");
        Driver.FindElement(TestId("login-button")).Click();

        var error = Wait.Until(d => d.FindElement(
            By.XPath("//p[contains(., 'Your email or password is incorrect')]")));
        Assert.That(error.Displayed, Is.True);
    }
}