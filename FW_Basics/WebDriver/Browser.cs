using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_Basics.WebDriver
{
    public class Browser
    {
        private static Browser currentInstance;
        private static IWebDriver driver;
        private static string browser;

        private Browser()
        {
            browser = Configuration.Browser;
            driver = BrowserFactory.GetDriver(browser);
        }

        public static Browser Instance => currentInstance ?? (currentInstance = new Browser());

        public static void Navigate(string url)
        {
            driver.Url = url;
        }

        public static void Refresh()
        {
            driver.Navigate().Refresh();
        }

        public static IWebDriver GetDriver()
        {
            return driver;
        }

        public static void Quit()
        {
            driver.Dispose();
            driver = null;
            currentInstance = null;
        }
    }
}
