using System;
using System.Collections.Generic;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets 
{
    public class AddContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddContactToGroupTest() 
        {
            GroupDatacs group = GroupDatacs.GetAll()[0];
            List<ContactDatas> oldList = group.GetContacts();
            ContactDatas contact = ContactDatas.GetAll().Except(oldList).First();
            app.Contacts.AddContactToGroup(contact, group);
            List<ContactDatas> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();
            Assert.AreEqual(oldList, newList);
        }
        
        [Test]
        public void DeleteContactToGroupTest()
        {
            GroupDatacs group = GroupDatacs.GetAll()[0];
            List<ContactDatas> oldList = group.GetContacts();
            ContactDatas contact = oldList.First();

            app.Contacts.DeleteContactToGroup(contact, group);

            List<ContactDatas> newList = group.GetContacts();
            oldList.Remove(contact);
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
