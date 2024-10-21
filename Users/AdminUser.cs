using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class AdminUser : User
    {
        public List<Workout> Workouts { get; set; }
        

        public AdminUser(string username, string password)
            : base(username, password)
        {
            IsAdmin = true;
            Workouts = new List<Workout>();
        }

        public static void ManageAllWorkouts()
        {
            Console.WriteLine("ManageAllWorkouts()");
        }
        

        public override void RegisterUser(string username, string password)
        {
            var newUser = new AdminUser(username, password);
            userList.Add(newUser);
        }

        public override bool SignIn(string username, string password)
        {
            foreach (var user in userList)
            {
                if (user.Username == username && user.Password == password)
                {
                    
                    Person.CurrentUser = user;
                    MessageBox.Show($"User {username} signed in.");
                    return true;
                }
            }
            MessageBox.Show("Invalid credentials.");
            return false;
        }

        public override void AddSecAuth(string Question, string Answer)
        {
            throw new NotImplementedException();
        }
    }
    
}
