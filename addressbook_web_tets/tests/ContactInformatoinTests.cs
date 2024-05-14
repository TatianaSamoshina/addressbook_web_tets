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
        }
    }
}
