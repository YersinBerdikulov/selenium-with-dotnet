using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace SeleniumWebDriverPracticeWithDotNet.PomTests.Pages;

public class FlightReservationPage
{
    private readonly IWebDriver _webDriver;

    [FindsBy(How = How.CssSelector, Using = "input[placeholder='Откуда']")]
    private IWebElement? _fromWhereField = null!;

    [FindsBy(How = How.CssSelector, Using = "input[placeholder='Куда']")]
    private IWebElement? _toWhereField = null!;

    [FindsBy(How = How.CssSelector, Using = "button.search-form__btn")]
    private IWebElement? _searchButton = null!;

    public FlightReservationPage(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        PageFactory.InitElements(_webDriver, this);
    }

    public void NavigateTo(string url) => _webDriver.Navigate().GoToUrl(url);

    public void EnterFromWhereAndSelectSuggestion(string location)
    {
        _fromWhereField?.Click();
        _fromWhereField?.SendKeys(location);
        Task.Delay(2000).Wait();
        WaitForSuggestionsAndSelect();
    }

    public void EnterToWhereAndSelectSuggestion(string location)
    {
        _toWhereField?.Click();
        _toWhereField?.SendKeys(location);
        Task.Delay(2000).Wait();
        WaitForSuggestionsAndSelect();
    }

    private void WaitForSuggestionsAndSelect()
    {
        var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(3));
        var suggestion = wait.Until(d => d.FindElement(By.CssSelector(".border-b")));
        suggestion.Click();
    }

    public void ClickSearchButton() => _searchButton?.Click();

    public IReadOnlyCollection<IWebElement> GetChooseButtons()
    {
        return _webDriver.FindElements(By.CssSelector("button.search-form__btn"));
    }

}