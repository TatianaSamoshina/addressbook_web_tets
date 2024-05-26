using NUnit.Framework;
//using System;
using System.Collections.Generic;
//using System.Linq;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupRemovalTests : GroupTestBase
    {
        [Test]
        public void GroupRemovalTest() 
        {
            // Проверяем наличие группы перед ее удалением
            if (!app.Groups.IsGroupPresent(1))
            {
                GroupDatacs group = new GroupDatacs("qw", "as", "zx");
                app.Groups.Create(group);               
                app.Navigator.GoToGroupsPage(); // Возвращаемся на страницу групп после создания новой группы
            }
            
            List<GroupDatacs> oldGroups = GroupDatacs.GetAll(); //Список групп ДО
            GroupDatacs tobeRemoved = oldGroups[0];                                     
            app.Groups.Removed(tobeRemoved); // Удаляем группу
            Assert.AreEqual(oldGroups.Count - 1, app.Groups.GetGroupCount());
           
            List<GroupDatacs> newGroups = GroupDatacs.GetAll(); //Список групп ПОСЛЕ //app.Groups.GetGroupList();
            oldGroups.RemoveAt(0);
            Assert.AreEqual(oldGroups, newGroups);
            foreach (GroupDatacs group in newGroups)
            {
                Assert.AreNotEqual(group.Id, tobeRemoved.Id);
            }
        }
    }
}
