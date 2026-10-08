using AutomationEx.Selenium.Tests.Components;
using AutomationEx.Selenium.Tests.Fixtures;
using OpenQA.Selenium;

namespace AutomationEx.Selenium.Tests.Pages;

public abstract class BasePage(IWebDriver driver) : UiComponent(driver)
{
    protected const string BaseUrl = TestConfig.BaseUrl;

    public HeaderNav Header { get; } = new(driver);

    protected void WaitForUrl(string pathEnding) => Wait.Until(d => new Uri(d.Url).AbsolutePath.TrimEnd('/').EndsWith(pathEnding));
}
