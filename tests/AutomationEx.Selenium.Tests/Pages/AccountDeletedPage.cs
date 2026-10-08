using OpenQA.Selenium;

namespace AutomationEx.Selenium.Tests.Pages;

public class AccountDeletedPage(IWebDriver driver) : BasePage(driver)
{
    private static readonly By Heading = TestId("account-deleted");
    private static readonly By ContinueButton = TestId("continue-button");

    public void WaitUntilLoaded()
    {
        WaitForUrl("/delete_account");
        WaitForVisible(Heading);
    }

    public string HeadingText() => WaitForVisible(Heading).Text.Trim();

    public void Continue() => Click(ContinueButton);
}
