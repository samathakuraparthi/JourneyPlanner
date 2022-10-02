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
    }
}