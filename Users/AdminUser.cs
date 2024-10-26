using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class AdminUser : User
    {
        public List<Workout> Workouts { get; set; }
        

        public AdminUser(string username, string password, string country, string securityQuestion, string securityAnswer)
            : base(username, password, country, securityQuestion,securityAnswer)
        {
            IsAdmin = true;
            Workouts = new List<Workout>();
        }

        public static void ManageAllWorkouts()
        {
            Console.WriteLine("ManageAllWorkouts()");
        }                        
    }
    
}
