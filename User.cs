using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjhoadon
{
    class User
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }

        public User(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
        }
    }
}
