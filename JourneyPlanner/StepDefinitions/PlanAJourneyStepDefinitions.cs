using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using TechTalk.SpecFlow;
using JourneyPlanner.PageObjects;
using OpenQA.Selenium.Support.UI;
using JourneyPlanner.Model;
using TechTalk.SpecFlow.Assist;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;

namespace JourneyPlanner.StepDefinitions
{
    [Binding]
    public class PlanAJourneyStepDefinitions
    {
        WebDriver driver;
        IWait<IWebDriver> webDriverWait;
        HomePage homePage;
        JourneyResultsPage resultPage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var browserType = TestContext.Parameters["BrowserType"];
            var headLess = Convert.ToBoolean(TestContext.Parameters["Headless"]);

            if (browserType.ToLower() == "chrome")
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments("start-maximized");
                options.AddArguments("--incognito");
                if (headLess)
                    options.AddArgument("--headless");

                driver = new ChromeDriver(options);
            }
            else
            {
                driver = new FirefoxDriver();
            }

            webDriverWait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
            webDriverWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Close();
        }

        [Given(@"user open tfl journey planner")]
        public void GivenUserOpenTflJourneyPlanner()
        {
            driver.Url = "https://tfl.gov.uk/";
            var acceptCookiesElementBy = By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
            webDriverWait.Until(_ => driver.FindElement(acceptCookiesElementBy).Displayed);
            driver.FindElement(acceptCookiesElementBy).Click();

            var doneElementBy = By.XPath("//div[@id='cb-confirmedSettings']/div/button");
            webDriverWait.Until(_ => driver.FindElement(doneElementBy).Displayed);
            driver.FindElement(doneElementBy).Click();
        }

        [When(@"user enter plan journey as follows")]
        public void WhenUserEnterPlanJourneyAsFollows(Table Table)
        {
            var searchTable = Table.CreateInstance<SearchCriteriaTable>();
            homePage = new HomePage(driver, webDriverWait);
            homePage.WaitForPageLoad();
            homePage.EnterFromAndToPlaces(searchTable.From, searchTable.To);
        }

        [When(@"user enter plan journey as follows")]
        [Scope(Tag = "InvalidLocation")]
        public void WhenUserEnterPlanJourneyAsFollowsWithInvalidLocations(Table Table)
        {
            var searchTable = Table.CreateInstance<SearchCriteriaTable>();
            homePage = new HomePage(driver, webDriverWait);
            homePage.WaitForPageLoad();
            homePage.EnterFromAndToPlaces(searchTable.From, searchTable.To, false);
        }

        [When(@"Click on Plan my journey")]
        public void WhenClickOnPlanMyJourney()
        {
            homePage.ClickPlanMyJourneyButton();
        }

        [Then(@"Journey results page is displayed")]
        public void ThenJourneyResultsPageIsDisplayed()
        {
            resultPage = new JourneyResultsPage(driver, webDriverWait);
            resultPage.WaitForPageLoad();
            resultPage.IsJourneyResultsHeaderDisplayed().Should().BeTrue();
        }

        [Then(@"Fastest by public transport result should contain the following route")]
        public void ThenFastestByPublicTransportResultShouldContainTheFollowingRoute(Table table)
        {
            var topResultsBy = By.Id("option-1-content");
            webDriverWait.Until(d => d.FindElement(topResultsBy).Displayed);
            var topResult = driver.FindElement(topResultsBy).Text;

            foreach (var row in table.Rows)
            {
                topResult.Contains(row["Route"]).Should().BeTrue($"{row["Route"]} is not found in the results");
            }
        }

        [Then(@"verify that error message '(.*)' is displayed")]
        public void ThenVerifyThatErrorMessageIsDisplayed(string expectedErrorMessage)
        {
            var actulErrorMessage = resultPage.GetErrorMessage();
            actulErrorMessage.Should().Be(expectedErrorMessage);
        }

        [Then(@"the following warning message is displayed")]
        public void ThenTheFollowingWarningMessageIsDisplayed(Table table)
        {
            throw new PendingStepException();
        }

        [When(@"user click the '([^']*)' link")]
        public void WhenUserClickTheLink(string p0)
        {
            throw new PendingStepException();
        }

        [When(@"Click on Update journey")]
        public void WhenClickOnUpdateJourney()
        {
            throw new PendingStepException();
        }

        [When(@"user clicks on '([^']*)' breadcrumb")]
        public void WhenUserClicksOnBreadcrumb(string p0)
        {
            throw new PendingStepException();
        }

        [Then(@"homepage is displayed")]
        public void ThenHomepageIsDisplayed()
        {
            throw new PendingStepException();
        }

        [When(@"user clicks on '([^']*)' tab")]
        public void WhenUserClicksOnTab(string recents)
        {
            throw new PendingStepException();
        }

        [Then(@"the recent journey '([^']*)' is displayed")]
        public void ThenTheRecentJourneyIsDisplayed(string p0)
        {
            throw new PendingStepException();
        }


    }
}
