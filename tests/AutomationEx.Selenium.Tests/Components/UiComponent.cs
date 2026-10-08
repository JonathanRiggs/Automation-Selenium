using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationEx.Selenium.Tests.Components;

public abstract class UiComponent(IWebDriver driver)
{
    protected readonly IWebDriver Driver = driver;
    protected readonly WebDriverWait Wait = new(driver, TimeSpan.FromSeconds(10));

    protected static By TestId(string id) => By.CssSelector($"[data-qa='{id}']");

    protected IWebElement WaitForVisible(By by) => Wait.Until(d => { var el = d.FindElement(by); return el.Displayed ? el : null; });

    protected void Type(By by, string text)
    {
        var el = WaitForVisible(by);
        el.Clear();
        el.SendKeys(text);
    }

    protected void Click(By by) => Wait.Until(d =>
    {
        var el = d.FindElement(by);
        if (!el.Displayed || !el.Enabled) return false;
        el.Click();
        return true;
    });
}
