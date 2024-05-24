using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_web_tets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace tests_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Usage: program <type> <count> <output file> <format>");
                return;
            }

            string dataType = args[0];
            int count;
            if (!int.TryParse(args[1], out count) || count < 0)
            {
                Console.WriteLine("Invalid count provided. Please provide a non-negative integer.");
                return;
            }

            string outputFile = args[2];
            string format = args[3];

            try
            {
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    if (dataType == "group")
                    {
                        List<GroupDatacs> groups = GenerateGroups(count);
                        if (format == "csv")
                        {
                            WriteGroupsToCsvFile(groups, writer);
                        }
                        else if (format == "xml")
                        {
                            WriteGroupsToXmlFile(groups, writer);
                        }
                        else if (format == "json")
                        {
                            WriteGroupsToJsonFile(groups, writer);
                        }
                        else
                        {
                            Console.WriteLine("Invalid format provided. Supported formats are 'csv' and 'xml'.");
                        }
                    }
                    else if (dataType == "contact")
                    {
                        List<ContactDatas> contacts = GenerateContacts(count);
                        if (format == "csv")
                        {
                            WriteContactsToCsvFile(contacts, writer);
                        }
                        else if (format == "xml")
                        {
                            WriteContactsToXmlFile(contacts, writer);
                        }
                        else if (format == "json")
                        {
                            WriteContactsToJsonFile(contacts, writer);
                        }
                        else
                        {
                            Console.WriteLine("Invalid format provided. Supported formats are 'csv','xml' and 'json'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid data type provided. Supported types are 'group' and 'contact'.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while writing to the file: {ex.Message}");
            }
        }

        static List<GroupDatacs> GenerateGroups(int count)
        {
            List<GroupDatacs> groups = new List<GroupDatacs>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new GroupDatacs(
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10)));
            }
            return groups;
        }

        static List<ContactDatas> GenerateContacts(int count)
        {
            List<ContactDatas> contacts = new List<ContactDatas>();
            for (int i = 0; i < count; i++)
            {
                contacts.Add(new ContactDatas(
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(10)));
            }
            return contacts;
        }

        static void WriteGroupsToCsvFile(List<GroupDatacs> groups, StreamWriter writer)
        {
            foreach (GroupDatacs group in groups)
            {
                writer.WriteLine($"{group.Name},{group.Header},{group.Footer}");
            }
        }

        static void WriteGroupsToXmlFile(List<GroupDatacs> groups, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<GroupDatacs>)).Serialize(writer, groups);
        }
        static void WriteGroupsToJsonFile(List<GroupDatacs> groups, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(groups, Newtonsoft.Json.Formatting.Indented));
        }

        static void WriteContactsToCsvFile(List<ContactDatas> contacts, StreamWriter writer)
        {
            foreach (ContactDatas contact in contacts)
            {
                writer.WriteLine($"{contact.FName},{contact.LName},{contact.Address},{contact.HomePhone},{contact.MobilePhone},{contact.WorkPhone},{contact.Email},{contact.Email2},{contact.Email3}");
            }
        }

        static void WriteContactsToXmlFile(List<ContactDatas> contacts, StreamWriter writer)
        {
            new XmlSerializer(typeof(List<ContactDatas>)).Serialize(writer, contacts);
        }
        static void WriteContactsToJsonFile(List<ContactDatas> contacts, StreamWriter writer)
        {
            writer.Write(JsonConvert.SerializeObject(contacts, Newtonsoft.Json.Formatting.Indented));
        }
    }
}