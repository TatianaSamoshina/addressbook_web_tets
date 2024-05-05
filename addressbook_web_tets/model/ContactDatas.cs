using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tets
{
    public class ContactDatas: IEquatable<ContactDatas>, IComparable<ContactDatas>
    {
        public ContactDatas(string Fname, string Lname)
        {
            FName = Fname;
            LName = Lname;
        }
        public string FName { get; set; }

        public string LName { get; set; }
  
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
                // Если имена не равны, возвращаем результат сравнения имен
                return nameComparison;
            }
            else
            {
                // Если имена равны, сравниваем фамилии
                return LName.CompareTo(other.LName);
            }
        }

        public override int GetHashCode()
        { return FName.GetHashCode(); }

        public override string ToString()
        { return "name=" + FName; }
    }
}
