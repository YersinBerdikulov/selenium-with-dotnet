using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumWebDriverPracticeWithDotNet.PomTests.Pages;

namespace SeleniumWebDriverPracticeWithDotNet.PomTests.Tests;

public sealed class FlightReservationTests : IDisposable
{
    private readonly IWebDriver _webDriver;
    private readonly FlightReservationPage _page;

    public FlightReservationTests()
    {
        _webDriver = new FirefoxDriver();
        _page = new FlightReservationPage(_webDriver);
    }

    [Fact]
    public void TryToMakeFlightReservation_UsingPom()
    {
        // Arrange
        _page.NavigateTo("https://aviata.kz");

        // Act
        _page.EnterFromWhereAndSelectSuggestion("Астана");
        _page.EnterToWhereAndSelectSuggestion("Алматы");
        _page.ClickSearchButton();

        var chooseButtons = _page.GetChooseButtons();

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => chooseButtons.ElementAt(0).Click());
    }

    public void Dispose()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}