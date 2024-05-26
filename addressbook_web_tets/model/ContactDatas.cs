using System;
using System.Linq;
using System.Text.RegularExpressions;
using LinqToDB.Mapping;
using System.Collections.Generic;

namespace addressbook_web_tets
{
    [Table(Name = "addressbook")]
    public class ContactDatas: IEquatable<ContactDatas>, IComparable<ContactDatas>
    {
        private string allPhones;
        private string allEmails;

        public ContactDatas(string Fname, string Lname)
        {
            FName = Fname;
            LName = Lname;
        }
        public ContactDatas() { }

        [Column(Name = "firstname")]
        public string FName { get; set; }
        
        [Column(Name = "lastname")]
        public string LName { get; set; }
        
        [Column(Name = "address")]
        public string Address { get; set; }
        
        [Column(Name = "home")]
        public string HomePhone { get; set; }
        
        [Column(Name = "mobile")]
        public string MobilePhone { get; set; }
        
        [Column(Name = "work")]
        public string WorkPhone { get; set; }
        
        [Column(Name = "email")]
        public string Email { get; set; }
        
        [Column(Name = "email2")]
        public string Email2 { get; set; }
        
        [Column(Name = "email3")]
        public string Email3 { get; set; }
        
        [Column(Name = "id"), PrimaryKey, Identity]
        public string IdContact { get; set; }
        
        public string AllPhones
        {
            get 
            {
                if (allPhones != null) { return allPhones; }
                else { return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim(); }
            }
            set { allPhones = value; } 
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null) { return allEmails; }
                else { return (CleanUp(Email) + CleanUp(Email2) + CleanUp(Email3)).Trim(); }
            }
            set { allEmails = value; }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "") { return ""; }
            return Regex.Replace(phone, "[ ()-]", "") + "\n";
        }

        public bool Equals(ContactDatas other)
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            if (Object.ReferenceEquals(this, other)) { return true; }
            return FName == other.FName;
        }

        public int CompareTo(ContactDatas other)
        {
            if (Object.ReferenceEquals(other, null)) { return 1; }

            // Сначала сравниваем имена
            int nameComparison = FName.CompareTo(other.FName);
            if (nameComparison != 0)
            {               
                return nameComparison;// Если имена не равны, возвращаем результат сравнения имен
            }
            else
            {               
                return LName.CompareTo(other.LName);// Если имена равны, сравниваем фамилии
            }
        }
        
        public override int GetHashCode()
        { return FName.GetHashCode(); }

        public override string ToString()
        { return "Fname=" + FName + "\nLname =" + LName; }
        public static List<ContactDatas> GetAll()
        {
            using (AddressbookDB db = new AddressbookDB())
            {
                return (from g in db.Contacts select g).ToList();
            }
        }
    }
}
