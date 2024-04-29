using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupRemovalTests : AuthTestBase//TestBase
    {
        [Test]
        public void GroupRemovalTest() 
        {
            // Проверяем наличие группы перед ее удалением
            if (!app.Groups.IsGroupPresent(1))
            {
                GroupDatacs group = new GroupDatacs("qw", "as", "zx");
                app.Groups.Create(group);
                // Возвращаемся на страницу групп после создания новой группы
                app.Navigator.GoToGroupsPage();
            }

            // Удаляем группу
            app.Groups.Remove(1);
        }
    }
}
