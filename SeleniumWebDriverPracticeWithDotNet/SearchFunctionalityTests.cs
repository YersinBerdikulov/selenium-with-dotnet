using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumWebDriverPracticeWithDotNet;

public sealed class SearchFunctionalityTests : IDisposable
{
    private readonly IWebDriver _webDriver;

    public SearchFunctionalityTests()
    {
        _webDriver = new FirefoxDriver();
    }

    [Fact]
    public void SearchTest_ByUserImitation_UsingCssSelector()
    {
        //Arrange
        _webDriver
            .Navigate()
            .GoToUrl("https://www.google.com");

        var searchBar = _webDriver.FindElement(By.CssSelector("textarea#APjFqb"));

        //Act
        searchBar.SendKeys("Quality Assurance");
        searchBar.SendKeys(Keys.Enter);

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3));
        wait.Until(driver => driver.Title.Contains("Quality Assurance"));

        //Assert
        Assert.Contains("Quality Assurance", _webDriver.Title);
    }

    [Fact]
    public void SearchTest_ByUserImitation_UsingXPath()
    {
        //Arrange
        _webDriver
            .Navigate()
            .GoToUrl("https://www.google.com");

        var searchBar = _webDriver.FindElement(By.XPath("//textarea[@id='APjFqb']"));

        //Act
        searchBar.SendKeys("Quality Assurance");
        searchBar.SendKeys(Keys.Enter);

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3));
        wait.Until(driver => driver.Title.Contains("Quality Assurance"));

        //Assert
        Assert.Contains("Quality Assurance", _webDriver.Title);
    }

    [Fact]
    public void SearchTest_ByChangingUrlContents()
    {
        //Arrange
        _webDriver
            .Navigate()
            .GoToUrl("https://www.google.com");

        const string topicToSearch = "Quality+Assurance";

        //Act
        _webDriver.Url = _webDriver.Url + "/search?q=" + topicToSearch;

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3));
        wait.Until(driver => driver.Title.Contains("Quality Assurance"));

        //Assert
        Assert.Contains("Quality Assurance", _webDriver.Title);
        Assert.Contains("Quality+Assurance", _webDriver.Url);
    }

    public void Dispose()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}