using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2.Models
{
    public class User
    {
        public User()
        {
            
        }
        public User(string username, string email, string salt, string hash)
        {
            Username = username;
            Email = email;
            DateRegistered = DateTime.Now;
            Salt = salt;
            PasswordHash = hash;
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistered { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
    }
}
