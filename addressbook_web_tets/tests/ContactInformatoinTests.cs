using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using System.Linq;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactInformatoinTests : AuthTestBase
    {
        [Test]
        public void TestContactInformation()
        {
            ContactDatas fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactDatas fromForm = app.Contacts.GetContactInformationFromEditForm(0);
            //verification
            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
        }
        [Test]
        public void TestContactInformation2()
        {
            ContactDatas fromProperties = app.Contacts.GetContactInformationFromProperties(1);
            ContactDatas fromForm = app.Contacts.GetContactInformationFromEditForm(1);
            //verification
            Assert.AreEqual(fromProperties, fromForm);
            Assert.AreEqual(fromProperties.Address, fromForm.Address);
            Assert.AreEqual(fromProperties.AllPhones, fromForm.AllPhones);
            Assert.AreEqual(fromProperties.AllEmails, fromForm.AllEmails);
        }
    }
}
