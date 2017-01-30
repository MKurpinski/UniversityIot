using Microsoft.VisualStudio.TestTools.UnitTesting;
using University.Selenium.Framework.Browser;
using University.Selenium.Framework.Utilities;
using OpenQA.Selenium.Support.Events;

namespace University.Selenium.Tests
{
    [TestClass]
    public class ExampleTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var screensotDriver = new EventFiringWebDriver(DriverMethods.GetDriverType());
            screensotDriver.ExceptionThrown += DriverMethods.TakeScreenshotOnException;
            Driver.WebDriver = screensotDriver;

        }

        [TestMethod]
        public void ExampleShouldSuccess()
        {
            //arrange
            Driver.GoToExamplePage();
            Driver.ImplicitWait();

            //act
            Page.ExamplePage.GoToLink1().GoToLink2();
            
            //assert
            Assert.IsTrue(Page.ExamplePage.CheckIfGotToExample());         
        }

        [TestCleanup]
        public void Cleanup()
        {
            Driver.Exit();
        }
    }
}
