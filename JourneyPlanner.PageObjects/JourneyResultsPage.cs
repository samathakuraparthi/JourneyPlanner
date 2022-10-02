using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JourneyPlanner.PageObjects
{
    public class JourneyResultsPage
    {
        public IWebDriver Driver { get; }
        public IWait<IWebDriver> Wait { get; }

        private IWebElement FromElement => Driver.FindElement(By.Id("InputFrom"));
        private By journeyResultsHeaderBy = By.XPath("//h1/span[text()=\"Journey results\"]");
        private IWebElement EditJourneyLink => Driver.FindElement(By.XPath("//*[contains(@class, 'edit-journey')]"));
        //private IWebElement UpdateJourneyButton => Driver.FindElement(By.XPath("//*[@id='plan-journey-button']"));
        private IWebElement UpdateJourneyButton => Driver.FindElement(By.Id("plan-journey-button"));

        public JourneyResultsPage(IWebDriver driver, IWait<IWebDriver> wait)
        {
            Driver = driver;
            Wait = wait;
        }

        public void WaitForPageLoad()
        {
            Wait.Until(d => d.FindElement(journeyResultsHeaderBy).Displayed);
        }

        public bool IsJourneyResultsHeaderDisplayed()
        {
            var elements = Driver.FindElements(journeyResultsHeaderBy);
            if (elements.Any() && elements[0].Displayed)
                return true;
            return false;
        }

        public string GetErrorMessage()
        {
            var errorMessageBy = By.XPath("//li[@class='field-validation-error']");
            Wait.Until(d => d.FindElement(errorMessageBy).Displayed);
            return Driver.FindElement(errorMessageBy).Text;
        }

        public void ClickEditJourneyLink()
        {
            EditJourneyLink.Click();
        }

        public void UpdateJourneyResults()
        {
            UpdateJourneyButton.Click();
        }

        public void ClickPlanAJourneyLink()
        {
            Driver.FindElement(By.LinkText("Plan a journey")).Click();
        }

        public void NavigateToHome()
        {
            Driver.FindElement(By.XPath("//span[contains(text(),'Home')]")).Click();
        }
    }
}