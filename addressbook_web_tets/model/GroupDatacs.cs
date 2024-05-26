using System;
using System.Collections.Generic;
using System.Linq;
using LinqToDB.Mapping;

namespace addressbook_web_tets
{
    [Table(Name = "group_list")]
    public class GroupDatacs: IEquatable<GroupDatacs>, IComparable<GroupDatacs>
    {
        public GroupDatacs(string name, string header, string footer)
        {
            Name = name;
            Header = header;
            Footer = footer;
        }
        public GroupDatacs()  { }

        [Column (Name = "group_name")]
        public string Name { get; set; }
       
        [Column(Name = "group_header")]
        public string Header { get; set; }
       
        [Column(Name = "group_footer")]
        public string Footer { get; set; }
       
        [Column(Name = "group_id"), PrimaryKey, Identity]
        public string Id { get; set; }


        public bool Equals (GroupDatacs other) 
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            if (Object.ReferenceEquals(this, other)) { return true; }
            return Name == other.Name; 
        }

        public int CompareTo(GroupDatacs other)
        {
            if (Object.ReferenceEquals(other, null)) { return 1; }
            return Name.CompareTo(other.Name);
        }

        public override int GetHashCode()
        { return Name.GetHashCode(); }

        public override string ToString()
        { return "name=" + Name + "\nheader=" + Header + "\nfooter =" + Footer; }

        public static List<GroupDatacs> GetAll() 
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Groups select g).ToList();
            }
        }

        public List<ContactDatas> GetContacts() 
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from c in db.Contacts
                        from grc in db.GCR
                        .Where(p => p.GroupId == Id && p.ContactId == c.IdContact)
                        select c).Distinct().ToList();
            }
        }
    }
}
