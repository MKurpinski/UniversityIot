using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Support.Events;
using University.Selenium.Framework.Browser;
using University.Selenium.Framework.Utilities;

namespace University.Selenium.Tests
{
    [TestClass]
    public class ViessmannLoginPageTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var screensotDriver = new EventFiringWebDriver(DriverMethods.GetDriverType());
            screensotDriver.ExceptionThrown += DriverMethods.TakeScreenshotOnException;
            Driver.WebDriver = screensotDriver;
        }
        [TestMethod]
        public void CheckIfInputsAndButtonDisplayedOnCorrectlySite()
        {
            //arrange
            Driver.GoToExamplePage();
            Driver.ImplicitWait();

            //act
            Page.ViessmannLoginPage.GoToLoginPage();

            //assert
            Assert.IsTrue(Page.ViessmannLoginPage.CheckIfConfirmButtonDisplayed());
            Assert.IsTrue(Page.ViessmannLoginPage.CheckIfLoginInputDisplayed());
            Assert.IsTrue(Page.ViessmannLoginPage.CheckIfPasswordInputDisplayed());
        }
        [TestMethod]
        public void CorrectCredentialsShouldLetToSignInAndNotTriggerAlert()
        {
            //arrange
            Driver.GoToViessmannLoginPage();
            Driver.ImplicitWait();

            //act
            Page.ViessmannLoginPage.LogIn(Settings.CorrectLogin, Settings.CorrectPassword);
            Driver.WaitForBrowser();
            var alert = Driver.CheckAlert();
            
           
            //assert
            Assert.IsFalse(alert);
            Assert.AreEqual(Settings.ViessmannTemperaturePage, Driver.GetUrl());
        }
        [TestMethod]
        public void InorrectCredentialsShouldNotLetToSignInAndTriggerAlert()
        {
            //arrange
            Driver.GoToViessmannLoginPage();
            Driver.ImplicitWait();

            //act
            Page.ViessmannLoginPage.LogIn(Settings.IncorrectLogin,Settings.IncorrectPassword);
            Driver.WaitForBrowser();
            var alert = Driver.CheckAlert();
        

            //assert
            Assert.IsTrue(alert);
            Assert.AreNotEqual(Settings.ViessmannTemperaturePage, Driver.GetUrl());
        }
        [TestMethod]
        public void InorrectPasswordWithCorrectLoginShouldNotLetToSignInAndTriggerAlert()
        {
            //arrange
            Driver.GoToViessmannLoginPage();
            Driver.ImplicitWait();

            //act
            Page.ViessmannLoginPage.LogIn(Settings.CorrectLogin, Settings.IncorrectPassword);
            Driver.WaitForBrowser();
            var alert = Driver.CheckAlert();
       
            
            //assert
            Assert.IsTrue(alert);
            Assert.AreNotEqual(Settings.ViessmannTemperaturePage, Driver.GetUrl());
        }
        [TestMethod]
        public void CorrectPasswordWithIncorrectLoginShouldNotLetToSignInAndTriggerAlert()
        {
            //arrange
            Driver.GoToViessmannLoginPage();
            Driver.ImplicitWait();

            //act
            Page.ViessmannLoginPage.LogIn(Settings.IncorrectLogin, Settings.CorrectPassword);
            Driver.WaitForBrowser();
            var alert = Driver.CheckAlert();
            //assert

            Assert.IsTrue(alert);
            Assert.AreNotEqual(Settings.ViessmannTemperaturePage, Driver.GetUrl());
        }
        [TestMethod]
        public void EmptyLoginAndPasswordShouldNotLetToSignInAndTriggerAlert()
        {
            //arrange
            Driver.GoToViessmannLoginPage();
            Driver.ImplicitWait();

            //act
            Page.ViessmannLoginPage.LogIn(string.Empty,string.Empty);
            Driver.WaitForBrowser();
            var alert = Driver.CheckAlert();

            //assert
            Assert.IsTrue(alert);
            Assert.AreNotEqual(Settings.ViessmannTemperaturePage, Driver.GetUrl());
        }
        [TestMethod]
        public void PasswordInputShouldHasTypePassword()
        {
            //arrange
            Driver.GoToViessmannLoginPage();
            Driver.ImplicitWait();

            //act
            var passwordType = Page.ViessmannLoginPage.CheckIfInputForPasswordHasTypePassword();
            
            //assert
            Assert.IsTrue(passwordType);
        }
        [TestCleanup]
        public void Cleanup()
        {
            Driver.Exit();
        }
    }
}
