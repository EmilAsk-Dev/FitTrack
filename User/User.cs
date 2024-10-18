using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.User
{
    class User : Person
    {
        private string SecurityQuestion {  get; set; }
        private string SecurityAnswer {  get; set; }
        private string Country { get; set; }

        public User(string Username, string Password, string Country, string SecurityQuestion, string SecurityAnswer) : base(Username, Password) 
        {
            this.UserName = Username;
            this.Password = Password;
            
        }


        public void ResetPassword(string Password)
        {
            //Password = value
        }

        public override void SignIn(string username, string password)
        {
            // if Username and password == input -> signin
        }

        
    }
}
