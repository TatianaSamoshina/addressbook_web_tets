using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tets
{
    public class ContactDatas: IEquatable<ContactDatas>, IComparable<ContactDatas>
    {
        private string Fname;
        private string Lname;
        public ContactDatas(string Fname, string Lname)
        {
            this.Fname = Fname;
            this.Lname = Lname;
        }
        public string FName
        {
            get { return Fname; }
            set { Fname = value; }
        }
        public string LName
        {
            get { return Lname; }
            set { Lname = value; }
        }



        public bool Equals(ContactDatas other)
        {
            if (Object.ReferenceEquals(other, null)) { return false; }
            if (Object.ReferenceEquals(this, other)) { return true; }
            return Fname == other.Fname;
        }

        public int CompareTo(ContactDatas other)
        {
            if (Object.ReferenceEquals(other, null)) { return 1; }
            return Fname.CompareTo(other.Fname);
        }

        public override int GetHashCode()
        { return Fname.GetHashCode(); }

        public override string ToString()
        { return "name=" + Fname; }
    }
}
