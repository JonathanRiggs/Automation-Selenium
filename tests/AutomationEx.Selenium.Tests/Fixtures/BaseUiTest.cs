using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace AutomationEx.Selenium.Tests.Fixtures;

[Parallelizable(ParallelScope.Self)]
public abstract class BaseUiTest
{
    protected const string BaseUrl = "https://automationexercise.com";
    private static readonly string[] BlockedUrls = [
        "*googlesyndication.com*",
        "*doubleclick.net*",
        "*googleadservices.com*",
        "*adservice.google.com*"
    ];
    protected IWebDriver Driver = null!;
    protected WebDriverWait Wait = null!;

    [SetUp]
    protected void StartBrowser()
    {
        var options = new ChromeOptions();
        if (Environment.GetEnvironmentVariable("HEADED") != "1")
            options.AddArgument("--headless=new");
        options.AddArgument("--window-size=1280,800");

        var driver = new ChromeDriver(options);

        // Block ads
        driver.ExecuteCdpCommand("Network.enable", new Dictionary<string, object?>());
        driver.ExecuteCdpCommand("Network.setBlockedURLs", new Dictionary<string, object?>
        {
            ["urls"] = BlockedUrls
        });

        Driver = driver;
        Wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
    }

    [TearDown]
    public void QuitBrowser()
    {
        Driver?.Quit();
        Driver?.Dispose();
    }

    protected static By TestId(string id) => By.CssSelector($"[data-qa='{id}']");
}