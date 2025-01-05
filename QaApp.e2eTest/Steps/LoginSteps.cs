using Microsoft.Playwright;
using QaApp.e2eTest.Pages;

namespace QaApp.e2eTest.Steps;

[Binding]
public class LoginSteps
{
    private readonly IPage _user;
    private readonly QaAppHomePage _qaAppHomePage;

    public LoginSteps(Hooks.Hooks hooks, QaAppHomePage qaAppHomePage)
    {
        _user = hooks.User;
        _qaAppHomePage = qaAppHomePage;
    }

    [Given(@"the user is on the QaApp homepage")]
    public async Task GivenTheUserIsOnTheQaAppHomepage()
    {
        //Go to the QaApp homepage
        await _user.GotoAsync("http://localhost:5249/Identity/Account/Login");

        //Assert the page
        await _qaAppHomePage.AssertPageContent();
    }

    [When(@"I enter valid credentials")]
    public async Task WhenIEnterValidCredentials()
    {
        await _qaAppHomePage.EnterValidCredentials();
    }

    [When(@"I click the login button")]
    public async Task WhenIClickTheLoginButton()
    {
        await _qaAppHomePage.ClickLogin();
    }

    [Then(@"the user should be redirected to the QaApp home page")]
    public async Task ThenTheUserShouldBeRedirectedToTheQaAppHomePage()
    {
        await _qaAppHomePage.AssertUserIsLoggedIn();
    }

    // negative test steps
    [When(@"I enter invalid credentials")]
    public async Task WhenIEnterInvalidCredentials()
    {
        await _qaAppHomePage.EnterInvalidCredentials();
    }

    [Then(@"I should see an error message")]
    public async Task ThenIShouldSeeAnErrorMessage()
    {
        await _qaAppHomePage.AssertErrorMessage();
    }
}