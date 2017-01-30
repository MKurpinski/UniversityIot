using University.Selenium.Framework.Pages;
using OpenQA.Selenium.Support.PageObjects;

namespace University.Selenium.Framework.Browser
{
    public static class Page
    {
        public static ExamplePage ExamplePage
        {
            get
            {
                var examplePage = new ExamplePage();
                PageFactory.InitElements(Driver.Browser, examplePage);
                return examplePage;
            }
        }
        public static ViessmannLoginPage ViessmannLoginPage
        {
            get
            {
                var viessmanPage = new ViessmannLoginPage();
                PageFactory.InitElements(Driver.Browser, viessmanPage);
                return viessmanPage;
            }
        }
    }
}
