using Microsoft.Playwright;

namespace QaApp.e2eTest.Pages;

public class QaAppHomePage
{
    private readonly IPage _user;

    public QaAppHomePage(Hooks.Hooks hooks)
    {
        _user = hooks.User;
    }

    private ILocator EmailInput => _user.Locator("input[id='Input_Email']");
    private ILocator PasswordInput => _user.Locator("input[id='Input_Password']");
    private ILocator LoginButton => _user.Locator("button[type='submit']");
    private ILocator ErrorList => _user.Locator("form > div[role='alert'] > ul > li");

    public async Task AssertPageContent()
    {
        await Assertions.Expect(_user).ToHaveURLAsync("http://localhost:5249/Identity/Account/Login");
        await Assertions.Expect(EmailInput).ToBeVisibleAsync();
        await Assertions.Expect(PasswordInput).ToBeVisibleAsync();
        await Assertions.Expect(LoginButton).ToBeVisibleAsync();
    }

    public async Task EnterValidCredentials()
    {
        await EmailInput.TypeAsync("qa@qa.nl");
        await PasswordInput.TypeAsync("qa");
    }
    public async Task ClickLogin()
    {
        await LoginButton.ClickAsync();
    }

    public async Task AssertUserIsLoggedIn()
    {
        await Assertions.Expect(_user).ToHaveURLAsync("http://localhost:5249/");
    }

    public async Task EnterInvalidCredentials()
    {
        await EmailInput.TypeAsync("qa@qas.com");
        await PasswordInput.TypeAsync("qas");
    }

    public async Task AssertErrorMessage()
    {
        await Assertions.Expect(ErrorList).ToBeVisibleAsync();
    }
}