using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.User
{
    abstract class Person
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Person(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public abstract bool SignIn(string username, string password);
    }
}
