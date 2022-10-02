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
using FluentAssertions;

namespace JourneyPlanner.StepDefinitions
{
    [Binding]
    public class JourneyPlannerHomeSteps
    {
        HomePage homePage;
        JourneyResultsPage resultPage;

        public Context Context { get; }

        public JourneyPlannerHomeSteps(Context context)
        {
            Context = context;
        }

        [Given(@"user open tfl journey planner")]
        public void GivenUserOpenTflJourneyPlanner()
        {
            Context.Driver.Url = "https://tfl.gov.uk/";
            var acceptCookiesElementBy = By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
            Context.WebDriverWait.Until(_ => Context.Driver.FindElement(acceptCookiesElementBy).Displayed);
            Context.Driver.FindElement(acceptCookiesElementBy).Click();

            var doneElementBy = By.XPath("//div[@id='cb-confirmedSettings']/div/button");
            Context.WebDriverWait.Until(_ => Context.Driver.FindElement(doneElementBy).Displayed);
            Context.Driver.FindElement(doneElementBy).Click();

            homePage = new HomePage(Context.Driver, Context.WebDriverWait);
            homePage.WaitForPageLoad();
        }

        [When(@"user enter plan journey as follows")]
        public void WhenUserEnterPlanJourneyAsFollows(Table Table)
        {
            var searchTable = Table.CreateInstance<SearchCriteriaTable>();
            homePage.EnterFromAndToPlaces(searchTable.From, searchTable.To);
        }

        [When(@"user enter plan journey as follows")]
        [Scope(Tag = "InvalidLocation")]
        public void WhenUserEnterPlanJourneyAsFollowsWithInvalidLocations(Table Table)
        {
            var searchTable = Table.CreateInstance<SearchCriteriaTable>();
            homePage = new HomePage(Context.Driver, Context.WebDriverWait);
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
            resultPage = new JourneyResultsPage(Context.Driver, Context.WebDriverWait);
            resultPage.WaitForPageLoad();
            resultPage.IsJourneyResultsHeaderDisplayed().Should().BeTrue();
        }

        [Then(@"Fastest by public transport result should contain the following route")]
        public void ThenFastestByPublicTransportResultShouldContainTheFollowingRoute(Table table)
        {
            var topResultsBy = By.Id("option-1-content");
            Context.WebDriverWait.Until(d => d.FindElement(topResultsBy).Displayed);
            var topResult = Context.Driver.FindElement(topResultsBy).Text;

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

        [Then(@"validation message '([^']*)' displayed for the input field '([^']*)'")]
        public void ThenValidationMessageDisplayedForTheInputField(string expectedWarningMessage, string fieldName)
        {
            var actualErrorMessage = homePage.GetErrorMessage(fieldName);
            actualErrorMessage.Should().Be(expectedWarningMessage);
        }

        [When(@"user click the Edit journey link")]
        public void WhenUserClickTheLink()
        {
            resultPage.ClickEditJourneyLink();
        }

        [When(@"Click on Update journey")]
        public void WhenClickOnUpdateJourney()
        {
            resultPage.UpdateJourneyResults();
        }

        [When(@"user clicks on Home link")]
        public void WhenUserClicksOnHomeLink()
        {
            resultPage.NavigateToHome();
        }

        [Then(@"homepage is displayed")]
        public void ThenHomepageIsDisplayed()
        {
            homePage.WaitForPageLoad();
        }

        [When(@"user clicks on Recents tab")]
        public void WhenUserClicksOnTab()
        {
            homePage.ClickRecentsTab();
        }

        [Then(@"the recent journey '([^']*)' is displayed")]
        public void ThenTheRecentJourneyIsDisplayed(string expectedRecentSearch)
        {
            var actualRecentJourneyText = homePage.GetRecentSearch();
            actualRecentJourneyText.Should().Be(expectedRecentSearch);
        }


    }
}
