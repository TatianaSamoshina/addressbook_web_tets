using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.IO;

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
                groups.Add(new GroupDatacs(GenerateRandomString(30), GenerateRandomString(100), GenerateRandomString(100)));
            }

            return groups;
        }

        public static IEnumerable<GroupDatacs> GroupDataFromFile()
        {
            List<GroupDatacs> groups = new List<GroupDatacs>();
            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupDatacs(parts[0], parts[1], parts[2]));
            }
            return groups;
        }


        [Test, TestCaseSource("GroupDataFromFile")]
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