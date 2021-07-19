using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_Basics.PageObjects
{
    public class ProfileMenuPage : BasePage
    {
        private static readonly WebElement logoutButton = new WebElement(By.XPath("//div[text()= 'Выйти']"));

        public ProfileMenuPage() : base(logoutButton)
        {

        }

        public void ClickOnLogoutButton()
        {
            logoutButton.Click();
        }
    }
}
