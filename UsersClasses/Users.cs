using FitTrack.User;
using System.Windows;

namespace FitTrack.Users
{
    public class User : Person
    {
        
        public static User CurrentUser { get; set; }

        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        private static List<User> userList = new List<User>();

        
        public List<Workout.Workout> Workouts { get; set; } = new List<Workout.Workout>();

        
        public User(string username, string password, string country, string securityQuestion, string securityAnswer)
            : base(username, password)
        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
        }

        public User(string username, string password) : base(username, password) { }

        
        public void AddWorkout(Workout.Workout workout)
        {
            Workouts.Add(workout);
        }

        public override bool SignIn(string username, string password)
        {
            foreach (var user in userList)
            {
                if (user.Username == username && user.Password == password)
                {
                    CurrentUser = user;  
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

        public static bool IfUserExist(string username)
        {
            foreach (var user in userList)
            {
                if (user.Username == username)
                {
                    Console.WriteLine($"User: {username}");
                    return true;
                }
            }
            return false;
        }


        public static void ResetPassword(string username, string newPassword)
        {
            foreach (var user in userList)
            {
                if (user.Username == username)
                {
                    user.Password = newPassword;
                    MessageBox.Show($"Password reset successfully for {username}.");
                    Console.WriteLine($"Password reset successfully for {username}.");
                    return;
                }
            }

            MessageBox.Show($"User with username {username} not found.");
        }


    }
}
