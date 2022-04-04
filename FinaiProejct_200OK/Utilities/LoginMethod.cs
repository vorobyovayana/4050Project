using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinaiProejct_200OK.Utilities
{
    class LoginMethod
    {
        public static int Login(string userName, string passwrod)
        {
            // Search userName in table


            string userPassword = ""; // From database
            string key = ""; // From database
            int userId = 0; // From database
            // Compare the password
            if (passwrod == Encrypt.DecryptPassword(key, userPassword))
            {
                return userId;
            } else
            {
                return -1;
            }
        }
    }
}
