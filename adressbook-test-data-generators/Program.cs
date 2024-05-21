/*using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_web_tets;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tests_data_generators
{
    class Program
    {
        static void Main(string[] args) 
        {
            int count = Convert.ToInt32(args[0]);
            StreamWriter writer = StreamWriter(args[1]);
            for (int i = 0; i < count; i++) 
            {
                writer.WriteLine(String.Format("${0},${1},${2}",
                    TestBase.GenerateRandomString(10),
                    TestBase.GenerateRandomString(100),
                    TestBase.GenerateRandomString(100)));                   
            }
        }
    }
}*/
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_web_tets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tests_data_generators
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = Convert.ToInt32(args[0]);
            using (StreamWriter writer = new StreamWriter(args[1]))
            {
                for (int i = 0; i < count; i++)
                {
                    writer.WriteLine(String.Format("{0},{1},{2}",
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(10),
                        TestBase.GenerateRandomString(10)));
                }
                writer.Close();
            }
        }
    }
}