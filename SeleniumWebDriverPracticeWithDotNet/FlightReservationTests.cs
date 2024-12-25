using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using static System.Threading.Thread;

namespace SeleniumWebDriverPracticeWithDotNet;

public sealed class FlightReservationTests : IDisposable
{
    private readonly IWebDriver _webDriver;

    public FlightReservationTests()
    {
        var options = new FirefoxOptions();
        _webDriver = new FirefoxDriver(options);
    }

    [Fact]
    public async Task TryToMakeFlightReservation_UsingCssSelectors()
    {
        // Arrange
        await _webDriver
            .Navigate()
            .GoToUrlAsync("https://aviata.kz");

        var fromWhere = _webDriver.FindElement(By.CssSelector("input[placeholder='Откуда']"));
        var whereTo = _webDriver.FindElement(By.CssSelector("input[placeholder='Куда']"));
        var searchButton = _webDriver.FindElement(By.CssSelector("button.search-form__btn"));

        // Act
        fromWhere.Click();
        fromWhere.SendKeys("Астана");

        await Task.Delay(3000);
        var suggestion1 = _webDriver.FindElement(By.CssSelector(".border-b"));
        suggestion1.Click();
        whereTo.Click();
        whereTo.SendKeys("Алматы");
        await Task.Delay(3000);
        var suggestion2 = _webDriver.FindElement(By.CssSelector(".border-b"));
        suggestion2.Click();

        searchButton.Click();
        await Task.Delay(3000);
        var chooseButtons = _webDriver.FindElements(By.CssSelector("button.search-form__btn"));
        
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => chooseButtons[0].Click());
    }

    [Fact]
    public async Task TryToMakeFlightReservation_UsingXPath()
    {
        // Arrange
        await _webDriver
            .Navigate()
            .GoToUrlAsync("https://aviata.kz");

        var fromWhere = _webDriver.FindElement(By.XPath("//input[@placeholder='Откуда']"));
        var whereTo = _webDriver.FindElement(By.XPath("//input[@placeholder='Куда']"));
        var searchButton = _webDriver.FindElement(By.XPath("//button[contains(@class, 'search-form__btn')]"));

        // Act
        fromWhere.Click();
        fromWhere.SendKeys("Астана");

        await Task.Delay(2000);
        var suggestion1 = _webDriver.FindElement(By.XPath("//div[contains(@class, 'border-b')]"));
        suggestion1.Click();
        whereTo.Click();
        whereTo.SendKeys("Алматы");
        await Task.Delay(2000);
        var suggestion2 = _webDriver.FindElement(By.XPath("//div[contains(@class, 'border-b')]"));
        suggestion2.Click();

        searchButton.Click();
        await Task.Delay(3000);
        var chooseButtons = _webDriver.FindElements(By.XPath("//button[contains(@class, 'search-form__btn')]"));

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => chooseButtons[0].Click());
    }
    
    public void Dispose()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}