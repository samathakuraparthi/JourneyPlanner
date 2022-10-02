using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Xml.Serialization;

namespace JourneyPlanner.PageObjects
{
    public class HomePage
    {
        public IWebDriver Driver { get; }
        public IWait<IWebDriver> Wait { get; }
        private IWebElement AcceptAllCookiesElement => Driver.FindElement(By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll"));
        private IWebElement DoneElement => Driver.FindElement(By.XPath("//div[@id='cb-confirmedSettings']/div/button"));
        private IWebElement FromElement => Driver.FindElement(By.Id("InputFrom"));
        private IWebElement ToElement => Driver.FindElement(By.Id("InputTo"));

        private IWebElement FromSuggestionDropDownList => Driver.FindElement(By.XPath("//*[input[@id='InputFrom']]/*[contains(@class, 'tt-dropdown-menu')]"));
        private IWebElement ToSuggestionDropDownList => Driver.FindElement(By.XPath("//*[input[@id='InputTo']]/*[contains(@class, 'tt-dropdown-menu')]"));

        private IWebElement PlanJourneyButton => Driver.FindElement(By.Id("plan-journey-button"));
        private IWebElement RecentTab => Driver.FindElement(By.LinkText("Recents"));

        public HomePage(IWebDriver driver, IWait<IWebDriver> wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void WaitForPageLoad()
        {
            Wait.Until(driver => FromElement.Displayed);
            Wait.Until(driver => RecentTab.Displayed);
        }

        public void AcceptCookies()
        {
            Wait.Until(_ => AcceptAllCookiesElement.Displayed);
            AcceptAllCookiesElement.Click();

            Wait.Until(_ => DoneElement.Displayed);
            DoneElement.Click();
        }

        public void EnterFromAndToPlaces(string from, string to, bool selectFromSuggestion = true)
        {
            EnterAndSelectFromSuggestionList(FromElement, from, FromSuggestionDropDownList, selectFromSuggestion);
            EnterAndSelectFromSuggestionList(ToElement, to, ToSuggestionDropDownList, selectFromSuggestion);
        }

        public void EnterAndSelectFromSuggestionList(IWebElement element, string input, IWebElement suggestionElement, bool selectFromSuggestion)
        {
            Wait.Until(d => element.Displayed);
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Backspace);
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

        public string GetErrorMessage(string fieldName)
        {
            string locatorId = "InputFrom-error";

            if (fieldName.ToLower() == "to")
            {
                locatorId = "InputTo-error";
            }

            return Driver.FindElement(By.Id(locatorId)).Text;
        }

        public void ClickRecentsTab()
        {
            RecentTab.Click();
        }

        public string GetRecentSearch()
        {
            var recentTabBy = By.XPath("//*[contains(@class,'plain-button journey-item')]");
            Wait.Until(d => d.FindElement(recentTabBy).Displayed);
            return Driver.FindElement(recentTabBy).Text;
        }
    }
}