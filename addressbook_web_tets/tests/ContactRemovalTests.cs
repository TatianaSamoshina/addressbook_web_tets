using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactRemovalTests : ContactTestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            // Проверяем наличие контакта перед его удалением
            if (!app.Contacts.IsContactPresent(1))
            {
                ContactDatas contact = new ContactDatas("n1", "l1");
                app.Contacts.Create(contact);
            }
            List<ContactDatas> oldContacts = ContactDatas.GetAll(); 
            ContactDatas tobeRemoved = oldContacts[0];
            app.Contacts.Removed(tobeRemoved); 
            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactCount());

            List<ContactDatas> newContacts = ContactDatas.GetAll(); 
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);
            foreach (ContactDatas contact in newContacts)
            {
                Assert.AreNotEqual(contact.IdContact, tobeRemoved.IdContact);
            }
        }
    }
}
