Feature: Plan a Journey
Verify user can plan a journey using Journey Planner widget on TFL homepage

@JourneyPlanner @ValidJourney
Scenario: 1. Verify that a valid journey can be planned using the widget
	Given user open tfl journey planner
	When user enter plan journey as follows
		| From                           | To                            |
		| Green Park Underground Station | Stratford Underground Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	And Fastest by public transport result should contain the following route
		| Route                  |
		| Transfer to Green Park |
		| Stratford Station      |

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
	| ScenarioDesc               | From                              | To          | WarningMessage                                                              |
	| Invalid to place           | Hounslow East Underground Station | dfsdfsdfsdf | Sorry, we can't find a journey matching your criteria                       |
	| Invalid from and to places | 1231232                           | 13423234    | Journey planner could not find any results to your search. Please try again |

@NoLocation
Scenario: 3.Verify that the widget is unable to plan a journey if no locations are entered into the widget
	Given user open tfl journey planner
	When Click on Plan my journey
	Then validation message 'The From field is required.' displayed for the input field 'From'
	And validation message 'The To field is required.' displayed for the input field 'To'

@EditJourney
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

@RecentJourney
Scenario: 5.Verify that the "Recents" tab on the widget displays a list of recently planned journeys
	Given user open tfl journey planner
	When user enter plan journey as follows
		| From                          | To                           |
		| Stratford Underground Station | Stanmore Underground Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	When user clicks on Home link
	And user enter plan journey as follows
		| From                          | To                           |
		| Stratford Underground Station | Stanmore Underground Station |
	And Click on Plan my journey
	Then Journey results page is displayed
	When user clicks on Home link
	Then homepage is displayed
	When user clicks on Recents tab
	Then the recent journey 'Stratford Underground Station to Stanmore Underground Station' is displayed
