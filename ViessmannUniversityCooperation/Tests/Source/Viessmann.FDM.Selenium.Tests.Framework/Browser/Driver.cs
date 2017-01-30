using System.Threading;
using University.Selenium.Framework.Utilities;
using OpenQA.Selenium;

namespace University.Selenium.Framework.Browser
{
    public static class Driver
    {
        public static IWebDriver WebDriver = DriverMethods.GetDriverType();


        public static ISearchContext Browser
        {
            get { return WebDriver; }
        }

        public static string GetPageTitle
        {
            get
            {
                return WebDriver.Title;
            }
        }

        public static void GoToExamplePage()
        {
            WebDriver.Url = Settings.BaseUrl;
        }
        public static void GoToViessmannLoginPage()
        {
            WebDriver.Url = Settings.ViessmannLoginPage;
        }

        public static string GetUrl()
        {
            return WebDriver.Url;
        }
        public static void WaitForBrowser()
        {
            Thread.Sleep(Settings.WaitTime);
        }

        public static bool  CheckAlert()
        {
            try
            {
                var alert = WebDriver.SwitchTo().Alert();
                return alert.Text == Settings.AlertText;
            }
            catch(NoAlertPresentException)
            {
                return false;
            }
        }
        public static void Exit()
        {
            WebDriver.Close();
            WebDriver.Quit();
            WebDriver.Dispose();
        } 
        
        public static void ImplicitWait()
        {
            WebDriver.Manage().Timeouts().ImplicitlyWait(Settings.ImplicitWaitTimeout);
        }
    }
}
