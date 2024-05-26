using System;
//using System.Threading;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Org.BouncyCastle.Tls.Crypto;
using LinqToDB;

namespace addressbook_web_tets
{
    [TestFixture]
    public class GroupCreationTests : GroupTestBase
    {
        public static IEnumerable<GroupDatacs> RandomGroupDataProvider()
        {
            List<GroupDatacs> groups = new List<GroupDatacs>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupDatacs(GenerateRandomString(30), GenerateRandomString(100), GenerateRandomString(100)));
            }
            return groups;
        }

        public static IEnumerable<GroupDatacs> GroupDataFromCsvFile()
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

        public static IEnumerable<GroupDatacs> GroupDataFromXmlFile()
        {
            return (List<GroupDatacs>)
                new XmlSerializer(typeof(List<GroupDatacs>)).Deserialize(new StreamReader(@"groups.xml"));
        }
        public static IEnumerable<GroupDatacs> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupDatacs>>(File.ReadAllText(@"groups.json"));
        }

 
        [Test, TestCaseSource("GroupDataFromXmlFile")]
        public void GroupCreationTest(GroupDatacs group)
        {
            List<GroupDatacs> oldGroups = GroupDatacs.GetAll();
            app.Groups.Create(group);
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.AreEqual(oldGroups.Count + 1, app.Groups.GetGroupCount());
            List<GroupDatacs> newGroups = GroupDatacs.GetAll();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(oldGroups.SequenceEqual(newGroups));
        }

        [Test]
        public void TestDBConnectivity()
        {
            DateTime start = DateTime.Now;
            List<GroupDatacs> fromUi = app.Groups.GetGroupList();
            DateTime end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));

            start = DateTime.Now;
            List<GroupDatacs> fromDb = GroupDatacs.GetAll();
            end = DateTime.Now;
            System.Console.Out.WriteLine(end.Subtract(start));
        }
    }
}
