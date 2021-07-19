﻿using FW_Basics.Utils;
using FW_Basics.WebDriver;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_Basics.PageObjects
{
    public class WebElement
    {
        private readonly IWebDriver driver;
        private readonly By locator;
        private readonly WebDriverWait wait;

        public WebElement(By locator)
        {
            this.locator = locator;
            driver = Browser.GetDriver();
            wait = Wait.GetWait();
        }

        public IWebElement GetElement()
        {
            return driver.FindElement(locator);
        }

        public ReadOnlyCollection<IWebElement> GetElements()
        {
            return driver.FindElements(locator);
        }

        public By GetLocator()
        {
            return locator;
        }

        public int GetCount()
        {
            return wait.Until(WaitConditions.VisibilityOfAllElementsLocatedBy(locator)).Count;
        }

        public void Click()
        {
            wait.Until(WaitConditions.ElementToBoClickable(locator)).Click();
        }

        public void JSClick()
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)driver;
            jsExecuter.ExecuteScript("arguments[0].click()", GetElement());
        }

        public void SendKeys(string input)
        {
            wait.Until(WaitConditions.ElementDisplayed(locator)).SendKeys(input);
        }

        public void JSSendkeysByInnerText(string input)
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)driver;
            jsExecuter.ExecuteScript("arguments[0].innerText = '"+ input +"'", GetElement());
        }

        public void JSSendkeysByValue(string input)
        {
            IJavaScriptExecutor jsExecuter = (IJavaScriptExecutor)driver;
            jsExecuter.ExecuteScript($"arguments[0].setAttribute(\"value\", {input})", GetElement());
        }

        public string GetText()
        {
            return wait.Until(WaitConditions.ElementDisplayed(locator)).Text;
        }

        public string GetAttribute(string attribute)
        {
            return wait.Until(WaitConditions.ElementDisplayed(locator)).GetAttribute(attribute);
        }

        public bool Displayed()
        {
            return driver.FindElement(locator).Displayed;
        }

    }
}
