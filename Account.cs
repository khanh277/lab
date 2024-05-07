using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjhoadon
{
    class Account
    {
        public string userName;
        public string UserName {
            get => userName;
            private set {
                if (string.IsNullOrWhiteSpace(value)) { 
                    throw new ArgumentNullException("Username cannot be empty or whitespace.");
                }
                userName = value;
            }
        }

        public string passWord;
        public string PassWord {
            get => passWord;
            private set
            { 
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("Password cannot be empty or whitespace.");
                }
                passWord = value;
            }
        }

        public Account(string userName, string passWord)
        {
            UserName = userName;
            PassWord = passWord;
        }
    }
}
