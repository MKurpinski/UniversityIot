using System;

namespace University.Selenium.Framework.Utilities
{
    public static class Settings
    {
        public static String[] Arguments = Environment.GetCommandLineArgs();
        public static string Driver { get {return Arguments[0] ;}   }

        public static string BaseUrl { get { return "http://google.com"; } }

        public static string Login { get { return ""; } }
        public static string Password { get { return ""; } }
        
        public static  string LoginPageLink { get { return "/Account/LogOn"; } }

        public static TimeSpan ImplicitWaitTimeout { get { return TimeSpan.FromSeconds(30); } }
        public static int WaitTime { get { return 1000; } }

        public static string ViessmannLoginPage { get { return "http://localhost:3000/#!/login"; } }
        public static string ViessmannTemperaturePage { get { return "http://localhost:3000/#!/temperature"; } }
        public static string CorrectLogin { get { return "john.doe@viessmann.com"; } }
        public static string CorrectPassword { get { return "ViessmannJD"; } }
        public static string IncorrectLogin { get { return "jan.doe@viessmann.com"; } }
        public static string IncorrectPassword { get { return "Viesmann"; } }
        public static string AlertText { get { return "dupa"; } } //That wasn't our idea, we got it during classes.
        

    }
}
