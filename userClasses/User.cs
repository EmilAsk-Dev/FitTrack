using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows;

namespace FitTrack.User
{
    public class User : Person
    {
        // Static property to hold the current user
        public static User CurrentUser { get; set; }
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        private static List<User> userList = new List<User>();

        public User(string username, string password, string country, string securityQuestion, string securityAnswer) : base(username, password)

        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
           


        }
        
        public User(string username, string password) : base(username, password)
        {
           
        }

        public override bool SignIn(string username, string password)
        {
            foreach (var user in userList)
            {
                if (user.Username == username && user.Password == password)
                {
                    
                    MessageBox.Show($"User {username} signed in.");
                    return true;
                }
            }
            MessageBox.Show("Invalid credentials.");
            return false;
        }

        public override void RegisterUser(string username, string password)
        {
            var newUser = new User(username, password);
            userList.Add(newUser); 
            MessageBox.Show("User registered successfully.");
        }

        public static void AddUser(User user)
        {
            userList.Add(user);
        }

        
    }
}
