﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_Basics.PageObjects
{
    public class HeaderPage : BasePage
    {
        private static readonly WebElement header = new WebElement(By.Id("ph-whiteline"));
        private readonly WebElement emailAddress = new WebElement(By.XPath("//span[@class = 'ph-project__user-name svelte-a0l97g']"));

        public HeaderPage() : base(header)
        {

        }

        public bool VerifySuccessfullLogin(string login)
        {
            return emailAddress.GetText().Contains(login);
        }

        public void OpenProfileMenu()
        {
            emailAddress.Click();
        }
    }
}
