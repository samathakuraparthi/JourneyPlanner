# JourneyPlanner

## Tools used for Automation
- Selenium
- Specflow
- C#
- nunit

## Prerequisite
- Visual Studio 2022 
- .net 6.0 version
- Git for Windows
- Specflow Extension
- Expected to have chrome version 105 for smooth test runs (if the current version of your chrome is greater than 105 you will be needing to upgrade the ChromeDriver nugget package via nuggetmanager)

## Test execution
- Configure run settings with the following steps [if not settings provided test will be using default settings]
  - Visual Studio -> Test Menu -> Configure run settings -> Select solution wide settings file -> Select "JourneyPlanner\Tests.runsettings"

- Build the solution
- run all tests from test explorer
<img width="427" alt="image" src="https://user-images.githubusercontent.com/87817875/193454071-365c91df-e944-4c2c-97ca-82d65316787e.png">


## Known issues
 - Test may fail if the journey planner returns different results for the given search criteria. That is expected behavior since we are testing a live system 
 - Scenario 5 Looks like a bug or an expected behavior if we search one time the recent tab is not showing any results. So in the test, we are searching two times to see the Recent tab data
