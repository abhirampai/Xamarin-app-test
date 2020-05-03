using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App4.Models
{
    public class User
    {
        [PrimaryKey]
        public int id { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public string email { get; set; }
        public User() { }
        public User(string email, string password) {
            this.email = email;
            this.passWord = password;
        }
        public bool checkinfo()
        {
            if (!this.email.Equals("") && !this.passWord.Equals(""))
            {
                return true;
            }
            else
                return false; 
        }

    }
}
