using System;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using System.Drawing.Imaging;
using University.Selenium.Framework.Browser;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using OpenQA.Selenium.Interactions;

namespace University.Selenium.Framework.Utilities
{
    using OpenQA.Selenium.Firefox;

    public static class DriverMethods
    {
        public static void TakeScreenshotOnException(object sender, WebDriverExceptionEventArgs e)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hhmm-ss");
            Driver.WebDriver.TakeScreenshot().SaveAsFile("Exception-" + timestamp + ".png", ImageFormat.Png);
        }

        public static IWebDriver GetDriverType()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.BrowserExecutableLocation = "C:\\Program Files (x86)\\Mozilla Firefox\\firefox.exe";
            return new FirefoxDriver(options);
        }

        public static bool CheckElementExists(By by)
        {
            try
            {
                return Driver.WebDriver.FindElement(by).Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public static IJavaScriptExecutor JavaScripts(this IWebDriver driver)
        {
            return (IJavaScriptExecutor)driver;
        }

        public static bool IsAlertPresent()
        {
            try
            {
                Driver.WebDriver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException Ex)
            {
                return false;
            }
        }

        public static bool WaitForWebElementToLoad(By element, int timeToWait)
        {
            //wait until data shows up after upload
            var counter = 0;
            var elementAppeared = false;
            while (!elementAppeared)
            {
                counter++;

                try
                {
                    Driver.WebDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromMilliseconds(500));
                    IWebElement searchedElement = Driver.WebDriver.FindElement(element);
                    Driver.WebDriver.Manage().Timeouts().ImplicitlyWait(Settings.ImplicitWaitTimeout);
                    elementAppeared = searchedElement.Displayed;
                    if (elementAppeared)
                    {
                        return true;
                    }
                }
                catch (NoSuchElementException)
                {
                    elementAppeared = false;
                }

                if (counter > timeToWait * 2)
                {
                    return false;
                }
            }

            return elementAppeared;
        }

        public static void MoveToElement(IWebElement element)
        {
            Thread.Sleep(500);
            Actions moveToInstallation = new Actions(Driver.WebDriver);
            Driver.ImplicitWait();
            moveToInstallation.MoveToElement(element).Build().Perform();
            Thread.Sleep(900);
        }
    }
}
