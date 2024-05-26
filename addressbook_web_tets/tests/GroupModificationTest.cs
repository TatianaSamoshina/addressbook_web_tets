using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupModificationTest : GroupTestBase
    {
        [Test]
        public void GroupModificationTests()
        {
            if (!app.Groups.IsGroupPresent(1))
            {
                GroupDatacs newData = new GroupDatacs("qw", "as", null);
                app.Groups.Create(newData);
                app.Navigator.GoToGroupsPage(); // Возвращаемся на страницу групп после создания новой группы
            }

            GroupDatacs newDataForModification = new GroupDatacs("newName", "newHeader", "newFooter");
            List<GroupDatacs> oldGroups = GroupDatacs.GetAll();
            GroupDatacs oldData = oldGroups[0];
            app.Groups.Modify2(oldData, newDataForModification); 
            Assert.AreEqual(oldGroups.Count, app.Groups.GetGroupCount());
            List<GroupDatacs> newGroups = GroupDatacs.GetAll();
            oldGroups[0].Name = newDataForModification.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.IsTrue(oldGroups.SequenceEqual(newGroups));

            foreach (GroupDatacs group in newGroups)
            {
                if (group.Id == oldData.Id)
                {
                    Assert.AreEqual(newDataForModification.Name, group.Name);
                }
            }           
        }
    }
}
