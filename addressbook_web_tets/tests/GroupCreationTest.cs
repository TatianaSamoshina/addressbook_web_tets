using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupCreationTests : AuthTestBase
    {
        public static IEnumerable<GroupDatacs> RandomGroupDataProvider()
        {
            List<GroupDatacs> groups = new List<GroupDatacs>();
            for (int i = 0; i<5; i++)
            {
                groups.Add(new GroupDatacs(GenerateRandomString(30), GenerateRandomString(100), GenerateRandomString(100))
                /*{
                    Header = GenerateRandomString(100),
                    Footer = GenerateRandomString(100)
                }*/);
            }

            return groups;
        }

 
        [Test, TestCaseSource("RandomGroupDataProvider")]
        public void GroupCreationTest(GroupDatacs group)
        {
            //GroupDatacs group = new GroupDatacs("qw", "as", "zx");
            //Список групп ДО
            List<GroupDatacs> oldGroups = app.Groups.GetGroupList();
            //Создание группы
            app.Groups.Create(group);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            //Список групп ПОСЛЕ
            List<GroupDatacs> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            //Проверка создания
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(oldGroups.SequenceEqual(newGroups));
        }
    }
}