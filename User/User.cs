using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.User
{
    class User : Person
    {
        public User(string Username, string Password) : base(Username, Password) 
        {
            this.UserName = Username;
            this.Password = Password;
        }


        public override void SignIn(string username, string password)
        {
            
        }
    }
}
