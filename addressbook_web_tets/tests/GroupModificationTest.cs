using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupModificationTest : AuthTestBase
    {
        [Test]
        public void GroupModificationTests()
        {
            // Проверяем наличие группы перед ее модификацией
            if (!app.Groups.IsGroupPresent(1))
            {
                GroupDatacs newData = new GroupDatacs("qw", "as", null);
                app.Groups.Create(newData);
                // Возвращаемся на страницу групп после создания новой группы
                app.Navigator.GoToGroupsPage();
            }

            // Модифицируем группу
            GroupDatacs newDataForModification = new GroupDatacs("newName", "newHeader", "newFooter");
            List<GroupDatacs> oldGroups = app.Groups.GetGroupList();
            app.Groups.Modify(0, newDataForModification);
            List<GroupDatacs> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newDataForModification.Name;
            oldGroups.Sort();
            newGroups.Sort();

            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(oldGroups.SequenceEqual(newGroups));
        }
    }
}
