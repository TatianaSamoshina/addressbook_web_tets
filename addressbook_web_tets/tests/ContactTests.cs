using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactTests : TestBase
    {
        [Test]
        public void ContactTest()
        {
            app.Navigator.GoToContactsPage();
            app.Contacts
                .FillContactForm(new ContactDatas("n1", "l1"))
                .SubmitContactCreation();
            app.Navigator.ReturnToHomePage();
            app.Auth.Logout();
        } 
    }
}