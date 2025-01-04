using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SeleniumWebDriverPracticeWithDotNet;

public sealed class LoginAndLogoutFunctionalityTests : IDisposable
{
    private readonly IWebDriver _webDriver;
    private readonly UserData _userData;
    
    public LoginAndLogoutFunctionalityTests()
    {
        _webDriver = new FirefoxDriver();

        var currentDirectory = Directory.GetCurrentDirectory();
        var jsonFilePath = Path.Combine(currentDirectory, "UserData.json");
        var json = File.ReadAllText(jsonFilePath);

        _userData = JsonSerializer.Deserialize<UserData>(json)!;
    }

    [Fact]
    public void LoginToWebsiteAndLogout_UsingCssSelector()
    {
        // Arrange
        _webDriver
            .Navigate()
            .GoToUrl("https://saucedemo.com");

        var usernameField = _webDriver.FindElement(By.CssSelector("input#user-name"));
        var passwordField = _webDriver.FindElement(By.CssSelector("input#password"));
        var button = _webDriver.FindElement(By.CssSelector("input#login-button"));

        // Act
        usernameField.SendKeys(_userData.Username);
        passwordField.SendKeys(_userData.Password);
        button.Click();

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3));
        wait.Until(driver => driver.Url.Contains("inventory.html"));
        
        // Assert
        Assert.Contains("saucedemo.com/inventory.html", _webDriver.Url);
        
        var menuIcon = _webDriver.FindElement(By.CssSelector("button#react-burger-menu-btn"));
        menuIcon.Click();

        var logoutButton = _webDriver.FindElement(By.CssSelector("a#logout_sidebar_link"));
        logoutButton.Click();

        wait.Until(driver => driver.FindElement(By.CssSelector("input#login-button")));
        var loginButtonAfterLoggingOut = _webDriver.FindElement(By.CssSelector("input#login-button"));

        Assert.NotNull(loginButtonAfterLoggingOut);
    }

    [Fact]
    public void LoginToWebsiteAndLogout_UsingXPath()
    {
        // Arrange
        _webDriver.Navigate().GoToUrl("https://saucedemo.com");

        var usernameField = _webDriver.FindElement(By.XPath("//input[@id='user-name']"));
        var passwordField = _webDriver.FindElement(By.XPath("//input[@id='password']"));
        var button = _webDriver.FindElement(By.XPath("//input[@id='login-button']"));

        // Act
        usernameField.SendKeys(_userData.Username);
        passwordField.SendKeys(_userData.Password);
        button.Click();

        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3));
        wait.Until(driver => driver.Url.Contains("inventory.html"));
    
        // Assert
        Assert.Contains("saucedemo.com/inventory.html", _webDriver.Url);

        var menuIcon = _webDriver.FindElement(By.XPath("//button[@id='react-burger-menu-btn']"));
        menuIcon.Click();

        var logoutButton = _webDriver.FindElement(By.XPath("//a[@id='logout_sidebar_link']"));
        logoutButton.Click();

        wait.Until(driver => driver.FindElement(By.XPath("//input[@id='login-button']")));
        var loginButtonAfterLoggingOut = _webDriver.FindElement(By.XPath("//input[@id='login-button']"));

        Assert.NotNull(loginButtonAfterLoggingOut);
    }

    public void Dispose()
    {
        _webDriver.Quit();
        _webDriver.Dispose();
    }
}