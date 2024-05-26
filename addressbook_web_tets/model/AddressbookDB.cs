using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;
using MySql.Data.MySqlClient;

namespace addressbook_web_tets
{
    public class AddressbookDB : LinqToDB.Data.DataConnection
    {
        //public AddressbookDB() : base("Addressbook") { }
        public AddressbookDB() : base(ProviderName.MySql, @"server=localhost; database=addressbook; port=3306; Uid=root; Pwd=; charset=utf8; Allow Zero Datetime=true") { }
        public ITable<GroupDatacs> Groups { get { return this.GetTable<GroupDatacs>(); } }
        public ITable<ContactDatas> Contacts { get { return this.GetTable<ContactDatas>(); } }

    }
}
