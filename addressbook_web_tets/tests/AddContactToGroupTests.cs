//using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace addressbook_web_tets 
{
    public class AddContactToGroupTests : AuthTestBase
    {
        [Test]
        public void AddContactToGroupTest() 
        {           
            // Проверка наличия групп, если нет - создаем
            if (GroupDatacs.GetAll().Count == 0)
            {
                app.Groups.Create(new GroupDatacs("TestGroup1", "TestGroup2", "TestGroup3"));
            }

            GroupDatacs group = GroupDatacs.GetAll()[0];
            List<ContactDatas> oldList = group.GetContacts();

            // Проверка наличия контактов, если нет - создаем
            if (ContactDatas.GetAll().Count == 0)
            {
                app.Contacts.Create(new ContactDatas("Test", "Test2"));
            }

            // Поиск контакта, который еще не в группе
            ContactDatas contact = ContactDatas.GetAll().Except(oldList).FirstOrDefault();

            // Если все контакты добавлены во все группы - создать новую группу и выбрать любой контакт
            if (contact == null)
            {
                app.Groups.Create(new GroupDatacs("TestGroup1", "TestGroup2", "TestGroup3"));
                group = GroupDatacs.GetAll().Last(); 
                contact = ContactDatas.GetAll().First(); 
            }

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
            // Проверка наличия групп, если нет - создаем
            if (GroupDatacs.GetAll().Count == 0)
            {
                app.Groups.Create(new GroupDatacs("TestGroup1", "TestGroup2", "TestGroup3"));
            }

            GroupDatacs group = GroupDatacs.GetAll()[0];
            List<ContactDatas> oldList = group.GetContacts();

            // Если в группе нет контактов - добавляем
            if (oldList.Count == 0)
            {
                // Проверка наличия контактов, если нет - создаем
                if (ContactDatas.GetAll().Count == 0)
                {
                    app.Contacts.Create(new ContactDatas("Test", "Test2"));
                }

                // Добавляем первый доступный контакт в группу
                ContactDatas contact = ContactDatas.GetAll().First();
                app.Contacts.AddContactToGroup(contact, group);
                oldList.Add(contact); 
            }

            ContactDatas contactToDelete = oldList.First();
            app.Contacts.DeleteContactToGroup(contactToDelete, group);

            List<ContactDatas> newList = group.GetContacts();
            oldList.Remove(contactToDelete); 
            oldList.Sort();
            newList.Sort();
            Assert.AreEqual(oldList, newList);
        }
    }
}
