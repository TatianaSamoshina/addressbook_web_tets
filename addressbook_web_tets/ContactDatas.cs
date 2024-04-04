using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_web_tets
{
    internal class ContactDatas
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
    }
}
