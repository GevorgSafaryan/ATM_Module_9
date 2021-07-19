using FW_Basics.Entity;
using FW_Basics.PageObjects;
using FW_Basics.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW_Basics.Tests
{
    public class TestScenarios : BaseTest
    {
        private string errorMessageForIncorrectLogin = "Неверное имя ящика";
        private string errorMessageForIncorrectPassword = "Неверное имя или пароль";

        public List<Users> Users { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        [OneTimeSetUp]
        public void SetupCredetials()
        {
            Users = JsonConvertor.GetUser();
            Recipient = JsonConvertor.GetTestData().Recipient;
            Subject = JsonConvertor.GetTestData().Subject;
            Body = JsonConvertor.GetTestData().Body;
        }

        [Test]
        public void SuccessfullyLoginTest()
        {
            HomePage homePage = new HomePage();
            homePage.Login(Users[0].Login, Users[0].Password);
            HeaderPage header = new HeaderPage();
            Assert.IsTrue(header.VerifySuccessfullLogin(Users[0].Login));
        }

        [Test]
        public void TestTheCreationOfDraftEmail()
        {
            HomePage homePage = new HomePage();
            homePage.Login(Users[0].Login, Users[0].Password);
            LeftMenuPage leftMenu = new LeftMenuPage();
            leftMenu.ClickOnComposeButton();
            CreateEmailPage createEmail = new CreateEmailPage();
            createEmail.FillRecipient(Recipient);
            createEmail.FillSubject(Subject);
            createEmail.FillBody(Body);
            createEmail.SaveEmailByActions();
            createEmail.CloseNewEmailForm();
            leftMenu.OpenDraftsFolder();
            EmailContentPage emailContent = new EmailContentPage();
            emailContent.OpenEnEmailFromTheListById(0);
            Assert.IsTrue(createEmail.VerifyDraftEmailsContent(Recipient, Subject, Body));
        }

        [Test]
        public void TestDraftsFolderAfterSendingTheMail()
        {
            HomePage homePage = new HomePage();
            homePage.Login(Users[0].Login, Users[0].Password);
            LeftMenuPage leftMenu = new LeftMenuPage();
            leftMenu.ClickOnComposeButton();
            CreateEmailPage createEmail = new CreateEmailPage();
            createEmail.FillRecipient(Recipient);
            createEmail.FillSubject(Subject);
            createEmail.FillBody(Body);
            createEmail.SaveEmailByActions();
            createEmail.CloseNewEmailForm();
            leftMenu.OpenDraftsFolder();
            EmailContentPage emailContent = new EmailContentPage();
            emailContent.OpenEnEmailFromTheListById(0);
            createEmail.SendEmailByActions();
            createEmail.ClickOnCloseButtonAfterSendingEmail();
            Assert.IsTrue(emailContent.VerifyThatEmailDisappearsFromDraftsFolder());
        }

        [Test]
        public void TestThatEmailIsInSentFolderAfterSending()
        {
            HomePage homePage = new HomePage();
            homePage.Login(Users[0].Login, Users[0].Password);
            LeftMenuPage leftMenu = new LeftMenuPage();
            leftMenu.ClickOnComposeButton();
            CreateEmailPage createEmail = new CreateEmailPage();
            createEmail.FillRecipient(Recipient);
            createEmail.FillSubject(Subject);
            createEmail.FillBody(Body);
            createEmail.ClickOnSaveButton();
            createEmail.CloseNewEmailForm();
            leftMenu.OpenDraftsFolder();
            EmailContentPage emailContent = new EmailContentPage();
            emailContent.OpenEnEmailFromTheListById(0);
            createEmail.ClickOnSendButton();
            createEmail.ClickOnCloseButtonAfterSendingEmail();
            leftMenu.OpenSentFolder();
            emailContent.OpenEnEmailFromTheListById(0);
            Assert.IsTrue(emailContent.VerifySentEmailsContent(Recipient, Subject, Body));
        }

        [Test]
        public void TestDeletionOfTheEmailsByDragAndDrop()
        {
            HomePage homePage = new HomePage();
            homePage.Login(Users[0].Login, Users[0].Password);
            LeftMenuPage leftMenu = new LeftMenuPage();
            leftMenu.ClickOnComposeButton();
            CreateEmailPage createEmail = new CreateEmailPage();
            createEmail.FillRecipient(Recipient);
            createEmail.FillSubject(Subject);
            createEmail.FillBody(Body);
            createEmail.ClickOnSaveButton();
            createEmail.CloseNewEmailForm();
            leftMenu.OpenDraftsFolder();
            EmailContentPage emailContent = new EmailContentPage();
            emailContent.DeleteEmailsByDragAndDrop();
            Assert.IsTrue(emailContent.VerifyThatEmailDisappearsFromDraftsFolder());
        }

        [Test]
        public void TestSuccessfullyLogout()
        {
            HomePage homePage = new HomePage();
            homePage.Login(Users[0].Login, Users[0].Password);
            HeaderPage header = new HeaderPage();
            header.OpenProfileMenu();
            ProfileMenuPage profileMenu = new ProfileMenuPage();
            profileMenu.ClickOnLogoutButton();
            Assert.IsTrue(homePage.VerifySuccessfullLogout());
        }

        [Test]
        public void TestLoginWithIncorrectEmail()
        {
            HomePage homePage = new HomePage();
            homePage.EnterLogin(Users[1].Login);
            homePage.ClickOnEnterPasswordButton();
            Assert.IsTrue(homePage.VerifyErrorMessage(errorMessageForIncorrectLogin));
        }

        [Test]
        public void TestLoginWithIncorrectPassword()
        {
            HomePage homePage = new HomePage();
            homePage.EnterLogin(Users[2].Login);
            homePage.ClickOnEnterPasswordButton();
            homePage.EnterPassword(Users[2].Password);
            homePage.ClickOnEnterButton();
            Assert.IsTrue(homePage.VerifyErrorMessage(errorMessageForIncorrectPassword));
        }

        [TestCaseSource(typeof(JsonConvertor), "ReturnUsers")]
        public void TestLoginWhenSelectingIncorrectDomain(Users user)
        {
            HomePage homePage = new HomePage();
            homePage.EnterLogin(user.Login);
            homePage.SelectDomain(user.Domain);
            homePage.ClickOnEnterPasswordButton();
            Assert.IsTrue(homePage.VerifyErrorMessage(errorMessageForIncorrectLogin));
        }
    }
}
