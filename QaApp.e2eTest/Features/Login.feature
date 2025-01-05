@Login
Feature: Login


  @SuccessfulLogin
  Scenario: Successful login
    Given the user is on the QaApp homepage
    When I enter valid credentials
    And I click the login button
    Then I should be redirected to the homepage

  @UnsuccessfulLogin
  Scenario: Unsuccessful login with invalid credentials
    Given the user is on the QaApp homepage
    When I enter invalid credentials
    And I click the login button
    Then I should see an error message