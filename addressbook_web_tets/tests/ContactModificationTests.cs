using System;
using System.Collections.Generic;
using System.Linq;
//using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            // Проверяем наличие контакта перед его модификацией
            if (!app.Contacts.IsContactPresent(1))
            {
                ContactDatas newData = new ContactDatas("n1", "l1");
                app.Contacts.Create(newData);
            }
            // Модифицируем контакт
            ContactDatas newDataForModification = new ContactDatas("newName", "newLastName");
            List<ContactDatas> oldContacts = ContactDatas.GetAll();
            ContactDatas oldData = oldContacts[0];
            app.Contacts.Modify2(oldData, newDataForModification);
            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());
            List<ContactDatas> newContacts = ContactDatas.GetAll();
            oldContacts[0].FName = newDataForModification.FName;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.IsTrue(oldContacts.SequenceEqual(newContacts));
            foreach (ContactDatas contact in newContacts)
            {
                if (contact.IdContact == oldData.IdContact)
                {
                    Assert.AreEqual(newDataForModification.FName, contact.FName);
                }
            }
        }
    }
}
