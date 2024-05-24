﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace addressbook_web_tets
{
    public class ContactDatas: IEquatable<ContactDatas>, IComparable<ContactDatas>
    {
        private string allPhones;
        private string allEmails;

        public ContactDatas(string Fname, string Lname)
        {
            FName = Fname;
            LName = Lname;
        }
        public ContactDatas()
        {
        }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string Email2 { get; set; }
        public string Email3 { get; set; }
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

        public string IdContact { get; set; }


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
    }
}
