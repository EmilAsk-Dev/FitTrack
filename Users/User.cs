using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class User : Person
    {
        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        public List<Workout> Workouts { get; set; }

        

        public User(string username, string password, string country, string securityQuestion, string securityAnswer)
            : base(username, password)
        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
            Workouts = new List<Workout>();
        }

        public User(string username, string password)
            : base(username, password)
        {
            Workouts = new List<Workout>();
        }

        public override bool SignIn(string username, string password)
        {
            foreach (var user in Person.userList) 
            {
                bool isAdmin = user.GetType() == typeof(AdminUser);
                if (user.Username == username && user.Password == password)
                {
                    // Set Person.CurrentUser for all user types
                    Person.CurrentUser = user;
                    return true;
                }
            }
            MessageBox.Show("Invalid credentials.");
            return false;
        }

        public void RegisterExisting(User user)
        {
            userList.Add(user);
        }

        public override void RegisterUser(string username, string password)
        {
            var newUser = new User(username, password);
            userList.Add(newUser);
        }

        public static void AddUser(User user)
        {
            userList.Add(user);
        }

        public static void ResetPassword(string username, string newPassword)
        {
            foreach (var user in Person.userList)
            {
                if (user.Username == username)
                {
                    user.Password = newPassword;
                    MessageBox.Show($"Password reset successfully for {username}.");
                    return;
                }
            }

            MessageBox.Show($"User with username {username} not found.");
        }

        public static bool IfUserExist(string username)
        {
            foreach (var user in userList)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
