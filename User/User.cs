using FitTrack.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FitTrack.User
{
    class User : Person
    {
        public string SecurityQuestion { get; private set; }
        public string SecurityAnswer { get; private set; }
        public string Country { get; private set; }

       
        public User(string username, string password, string country, string securityQuestion, string securityAnswer) : base(username, password)

        {
            this.Country = country;
            this.SecurityQuestion = securityQuestion;
            this.SecurityAnswer = securityAnswer;
        }
        
        public User(string username, string password, string country) : base(username, password)

        {
            this.Country = country;
            
        }



        public void ResetPassword(string newPassword)
        {
            Password = newPassword;
            Console.WriteLine("Password has been reset.");
        }

        public override bool SignIn(string username, string password)
        {
            if (this.Username == username && this.Password == password)
            {
                Console.WriteLine("Sign-in successful.");
                
                
                return true;
                
                
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
                MessageBox.Show("Invalid credentials");
                return false;
            }
        }


    }
}
