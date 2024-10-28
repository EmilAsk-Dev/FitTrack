using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class User : Person
    {
        // Egenskaper för land, säkerhetsfråga och svar samt lista med användarens träningspass
        public string Country { get; set; }
        private string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }
        public List<Workout> Workouts { get; set; }
        public bool IsAdmin { get; set; } = false;

        // Konstruktor för användare med fullständiga detaljer
        public User(string username, string password, string country, string securityQuestion, string securityAnswer)
            : base(username, password)
        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
            Workouts = new List<Workout>();
        }

        // Konstruktor för användare med enbart användarnamn och lösenord
        public User(string username, string password)
            : base(username, password)
        {
            Workouts = new List<Workout>();
        }

        // Validerar användarnamn och lösenord och returnerar matchad användare om det finns
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

        // Logga in en användare baserat på användarnamn och lösenord
        public override bool SignIn(string username, string password)
        {
            if (ValidateUserAndPass(username, password, out User user))
            {
                ManageUser.currentUser = user;
                return true;
            }
            return false;
        }

        // Registrerar en redan existerande användare
        public void RegisterExisting(User user)
        {
            ManageUser.userList.Add(user);
        }

        // Registrerar en ny användare med enbart användarnamn och lösenord
        public override void RegisterUser(string username, string password)
        {
            User newUser = new User(username, password);
            ManageUser.userList.Add(newUser);
        }

        // Överlagrad metod för att registrera användare med ytterligare detaljer
        public void RegisterUser(string username, string password, string country, string securityQuestion, string securityAnswer)
        {
            User newUser = new User(username, password, country, securityQuestion, securityAnswer);
            ManageUser.userList.Add(newUser);
        }

        // Lägger till en användare direkt till användarlistan
        public static void AddUser(User user)
        {
            ManageUser.userList.Add(user);
        }

        // Återställ lösenordet för en användare och visa meddelande om användaren finns
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

        // Kontrollera om en användare med ett specifikt användarnamn finns
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

        // Validerar säkerhetssvaret för en användare
        public bool IfSecurityAnswer(string securityAnswer)
        {
            return securityAnswer.Equals(SecurityAnswer, StringComparison.OrdinalIgnoreCase);
        }

        // Kontroll om en användare har säkerhetsinformation
        public bool HaveAuth(User user)
        {
            return !(user.SecurityAnswer == null && user.SecurityQuestion == null);
        }

        // Lägger till säkerhetsfråga och svar för användaren
        public override void AddSecAuth(string question, string answer)
        {
            SecurityQuestion = question;
            SecurityAnswer = answer;
        }

        // Hämtar säkerhetsfråga och svar
        public void GetSecurityQA(out string answer, out string question)
        {
            answer = SecurityAnswer;
            question = SecurityQuestion;
        }

        // Metod för att hämta detaljer om användaren som en sträng
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

        // Uppdaterar användarens detaljer och visar ett meddelande om uppdateringen lyckades
        public void SaveUserDetails(string newUsername = null, string newPassword = null, string newCountry = null, string newSecurityQuestion = null, string newSecurityAnswer = null)
        {
            if (!string.IsNullOrEmpty(newUsername) && newUsername != Username)
            {
                Username = newUsername;
            }

            if (!string.IsNullOrEmpty(newPassword) && newPassword != Password)
            {
                Password = newPassword;
            }

            if (!string.IsNullOrEmpty(newCountry) && newCountry != Country)
            {
                Country = newCountry;
            }

            if (!string.IsNullOrEmpty(newSecurityQuestion) && newSecurityQuestion != SecurityQuestion)
            {
                SecurityQuestion = newSecurityQuestion;
            }

            if (!string.IsNullOrEmpty(newSecurityAnswer) && newSecurityAnswer != SecurityAnswer)
            {
                SecurityAnswer = newSecurityAnswer;
            }
            MessageBox.Show("User details updated successfully!");
        }

        
    }
}
