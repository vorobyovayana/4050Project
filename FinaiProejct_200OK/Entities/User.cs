using FinaiProejct_200OK.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FinaiProejct_200OK.Entities
{
    class User
    {
        private string password;
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }        

        public List<Favorite> Favorites { get; set; }
        public List<Review> Reviews { get; set; }

        private string key = "b14ca5898a4e4133bbce2ea2315a1916";

        public User()
        {
            
        }

        public User(string nUserName, string nPassword)
        {
            this.UserName = nUserName;
            this.password = nPassword;
        }

        public void setPassword(string nPassword)
        {
            this.password = Encrypt.EncryptPassword(key, nPassword);
            MessageBox.Show(password);
        }

        public string getPassword()
        {
            return Encrypt.DecryptPassword(key, password);
        }
    }
}
