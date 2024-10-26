using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class User : Person
    {
        public string Country { get; set; }
        private string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }
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

        public static bool ValidateUserAndPass(string username, string password, out User matchedUser)
        {
            matchedUser = null;
            foreach (var user in ManageUser.userList)
            {
                if (user.Username.Equals(username, StringComparison.OrdinalIgnoreCase) && user.Password == password)
                {
                    matchedUser = user;
                    return true;
                }
            }
            return false;
        }

        public override bool SignIn(string username, string password)
        {
            if (ValidateUserAndPass(username, password, out User user))
            {
                ManageUser.currentUser = user;
                return true;
            }
            return false;
        }

        public void RegisterExisting(User user)
        {
            ManageUser.userList.Add(user);
        }

        public override void RegisterUser(string username, string password)
        {
            User newUser = new User(username, password);
            ManageUser.userList.Add(newUser);
        }

        public void RegisterUser(string username, string password, string country, string securityQuestion, string securityAnswer)
        {
            User newUser = new User(username, password);
            ManageUser.userList.Add(newUser);
        }

        public static void AddUser(User user)
        {
            ManageUser.userList.Add(user);
        }

        public static void ResetPassword(string username, string newPassword)
        {
            foreach (var user in ManageUser.userList)
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
            foreach (var user in ManageUser.userList)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IfSecurityAnswer(string securityAnswer)
        {
            return securityAnswer.Equals(SecurityAnswer, StringComparison.OrdinalIgnoreCase);
        }

        public bool HaveAuth(User user)
        {
            return !(user.SecurityAnswer == null && user.SecurityQuestion == null);
        }

        public override void AddSecAuth(string question, string answer)
        {
            SecurityQuestion = question;
            SecurityAnswer = answer;
        }

        public void GetSecurityQA(out string answer, out string question)
        {
            answer = SecurityAnswer;
            question = SecurityQuestion;
        }

        public static string GetUserDetails(User user)
        {
            if (user == null)
            {
                return "User not found.";
            }

            return $"Username: {user.Username}\n" +
                   $"Country: {user.Country}\n" +
                   $"Security Question: {user.SecurityQuestion}\n" +
                   $"Security Answer: {user.SecurityAnswer}\n" +
                   $"Workouts Count: {user.Workouts.Count}";
        }

        
        public void SaveUserDetails(User user, string newUsername = null, string newPassword = null, string newCountry = null, string newSecurityQuestion = null, string newSecurityAnswer = null)
        {
            
            if (!string.IsNullOrEmpty(newUsername) && newUsername != Username)
            {
                user.Username = newUsername;
            }

            
            if (!string.IsNullOrEmpty(newPassword) && newPassword != Password)
            {
                user.Password = newPassword; 
            }

            
            if (!string.IsNullOrEmpty(newCountry) && newCountry != Country)
            {
                user.Country = newCountry;
            }

            
            if (!string.IsNullOrEmpty(newSecurityQuestion) && newSecurityQuestion != SecurityQuestion)
            {
                user.SecurityQuestion = newSecurityQuestion;
            }

            
            if (!string.IsNullOrEmpty(newSecurityAnswer) && newSecurityAnswer != SecurityAnswer)
            {
                user.SecurityAnswer = newSecurityAnswer;
            }            
            MessageBox.Show("User details updated successfully!");
        }
    }
}
