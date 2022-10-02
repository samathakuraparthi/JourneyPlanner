Feature: Plan a Journey
Journey planner widget is used to plan a journey

@JourneyPlanner @Regression @ValidJourney
Scenario: 1. Verify that a valid journey can be planned using the widget
	Given user open tfl journey planner
	When user enter plan journey as follows
		| From                           | To                                 |
		| Green Park Underground Station | Canary Wharf, Canary Wharf Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	And Fastest by public transport result should contain the following route
		| Route                  |
		| Transfer to Green Park |
		| Canary Wharf           |

@InvalidLocation
Scenario Outline: 2.Verify that the widget is unable to provide results when an invalid journey is planned
	Given user open tfl journey planner
	When user enter plan journey as follows
		| From   | To   |
		| <From> | <To> |
	And Click on Plan my journey
	Then Journey results page is displayed
	And verify that error message '<WarningMessage>' is displayed

Examples:
	| ScenarioDesc               | From                              | To       | WarningMessage                                                              |
	| Invalid to place           | Hounslow East Underground Station | 456789   | Journey planner could not find any results to your search. Please try again |
	| Invalid from and to places | 1231232                           | 13423234 | Journey planner could not find any results to your search. Please try again |


Scenario: 3.Verify that the widget is unable to plan a journey if no locations are entered into the widget
	Given user open tfl journey planner
	When Click on Plan my journey
	Then validation message 'The From field is required.' displayed for the input field 'From'
	And validation message 'The To field is required.' displayed for the input field 'To'

Scenario: 4.Verify that a journey can be amended by using the "Edit Journey" button on the journey results page
	Given user open tfl journey planner
	When user enter plan journey as follows
		| From                           | To                                 |
		| Green Park Underground Station | Canary Wharf, Canary Wharf Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	When user click the Edit journey link
	And user enter plan journey as follows
		| From                          | To                           |
		| Stratford Underground Station | Stanmore Underground Station |
	And Click on Update journey
	Then Fastest by public transport result should contain the following route
		| Route                         |
		| Transfer to Stratford Station |
		| Stanmore Station              |

Scenario: 5.Verify that the "Recents" tab on the widget displays a list of recently planned journeys
	Given user open tfl journey planner
	When user enter plan journey as follows
		| From                          | To                           |
		| Stratford Underground Station | Stanmore Underground Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	When user clicks on Home link
	When user enter plan journey as follows
		| From                          | To                           |
		| Stratford Underground Station | Stanmore Underground Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	When user clicks on Home link
	Then homepage is displayed
	When user clicks on Recents tab
	Then the recent journey 'Stratford Underground Station to Stanmore Underground Station' is displayed
