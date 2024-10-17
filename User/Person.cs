using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.User
{
    abstract class Person
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Person(string Username, string Password)
        {
            this.UserName = Username;
            this.Password = Password;
        }

        public abstract void SignIn(string username, string password);

    }
}
