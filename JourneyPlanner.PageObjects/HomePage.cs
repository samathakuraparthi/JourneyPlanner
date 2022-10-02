using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JourneyPlanner.PageObjects
{
    public class HomePage
    {
        public IWebDriver Driver { get; }
        public IWait<IWebDriver> Wait { get; }

        private IWebElement FromElement => Driver.FindElement(By.Id("InputFrom"));
        private IWebElement ToElement => Driver.FindElement(By.Id("InputTo"));

        private IWebElement FromSuggestionDropDownList => Driver.FindElement(By.XPath("//*[input[@id='InputFrom']]/*[contains(@class, 'tt-dropdown-menu')]"));
        private IWebElement ToSuggestionDropDownList => Driver.FindElement(By.XPath("//*[input[@id='InputTo']]/*[contains(@class, 'tt-dropdown-menu')]"));

        private IWebElement PlanJourneyButton => Driver.FindElement(By.Id("plan-journey-button"));

        public HomePage(IWebDriver driver, IWait<IWebDriver> wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void WaitForPageLoad()
        {
            Wait.Until(driver => FromElement.Displayed);
        }

        public void EnterFromAndToPlaces(string from, string to, bool selectFromSuggestion = true)
        {
            EnterAndSelectFromSuggestionList(FromElement, from, FromSuggestionDropDownList, selectFromSuggestion);
            EnterAndSelectFromSuggestionList(ToElement, to, ToSuggestionDropDownList, selectFromSuggestion);
        }

        public void EnterAndSelectFromSuggestionList(IWebElement element, string input, IWebElement suggestionElement, bool selectFromSuggestion)
        {
            element.SendKeys(input);
            if (selectFromSuggestion)
            {
                Wait.Until(driver => suggestionElement.Displayed);
                element.SendKeys(Keys.ArrowDown);
                element.SendKeys(Keys.Enter);
            }
        }

        public void ClickPlanMyJourneyButton()
        {
            PlanJourneyButton.Click();
        }
    }
}