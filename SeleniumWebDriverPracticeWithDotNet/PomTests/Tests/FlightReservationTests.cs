using System.Reflection;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using SeleniumWebDriverPracticeWithDotNet.PomTests.Pages;
using Xunit.Abstractions;

namespace SeleniumWebDriverPracticeWithDotNet.PomTests.Tests;

public sealed class FlightReservationTests : IClassFixture<ExtentReportsFixture>, IDisposable
{
    private readonly ExtentReports _extent;
    private readonly ExtentTest _test;
    private readonly IWebDriver _webDriver;
    private readonly FlightReservationPage _page;

    public FlightReservationTests(ExtentReportsFixture extentFixture, ITestOutputHelper output)
    {
        var type = output.GetType();
        var testMember = type.GetField("test", BindingFlags.Instance | BindingFlags.NonPublic);
        var test = (ITest)testMember?.GetValue(output)!;
        var className = test.TestCase.TestMethod.TestClass.Class.Name;
        var methodName = test.TestCase.TestMethod.Method.Name;

        _extent = extentFixture.Extent;
        _test = _extent.CreateTest($"POM implemented tests - Flight Reservation");
        _test.Log(Status.Info, $"Test class {className}");
        _test.Log(Status.Info, $"Test method {methodName}");
        _test.Log(Status.Info, $"Test method full path {className}.{methodName}");
        
        _webDriver = new FirefoxDriver();
        _page = new FlightReservationPage(_webDriver);
    }

    [Fact]
    public void TryToMakeFlightReservation_UsingPom()
    {
        try
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
            _test.Log(Status.Pass, "Test Successful. Website detected us as a bot as expected.");
        }
        catch (Exception e)
        {
            _test.Log(Status.Fail, "Test failed, see below for more details");
            _test.Log(Status.Error, e.Message);
        }
    }

    public void Dispose()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}