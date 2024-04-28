using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactCreationTests : AuthTestBase
    {
        [Test]
        public void ContactTest()
        {
            ContactDatas contact = new ContactDatas("n1", "l1");
            app.Contacts.Create(contact);
        } 
    }
}