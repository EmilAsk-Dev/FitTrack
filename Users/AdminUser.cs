using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class AdminUser : User
    {



        public AdminUser(string username, string password, string country, string securityQuestion, string securityAnswer)
        : base(username, password, country, securityQuestion, securityAnswer)
        {
            IsAdmin = true;            
        }

        public static List<Workout> ManageAllWorkouts(List<User> users)
        {
            List<Workout> allWorkouts = new List<Workout>();

            foreach (var user in users)
            {
                allWorkouts.AddRange(user.Workouts);
            }

            return allWorkouts;
        }
    }
    
}
