using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using University.Selenium.Framework.Browser;
using University.Selenium.Framework.Utilities;

namespace University.Selenium.Framework.Pages
{
    public class ViessmannLoginPage
    {
        [FindsBy(How = How.Id, Using = "login")]
        private IWebElement loginInput;
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordInput;
        [FindsBy(How = How.ClassName, Using = "button")]
        private IWebElement confirmButton;
        public bool CheckIfLoginInputDisplayed()
        {
            return loginInput.Displayed;
        }
        public bool CheckIfPasswordInputDisplayed()
        {
            return passwordInput.Displayed;
        }
        public bool CheckIfConfirmButtonDisplayed()
        {
            return confirmButton.Displayed;
        }

        public void GoToLoginPage()
        {
            Driver.WebDriver.Url = Settings.ViessmannLoginPage;
        }

        public void LogIn(string login, string password)
        {
            loginInput.SendKeys(login);
            passwordInput.SendKeys(password);
            confirmButton.Submit();
        }
        public bool CheckIfInputForPasswordHasTypePassword()
        {
            loginInput.SendKeys(Settings.CorrectPassword);
            return passwordInput.GetAttribute("type") == "password";


        }
    }
}
