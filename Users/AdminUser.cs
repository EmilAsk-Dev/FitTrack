using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    // Klass för administratörsanvändare som ärver från User
    public class AdminUser : User
    {
        // Konstruktor för AdminUser med specifika användaruppgifter och egenskaper
        public AdminUser(string username, string password, string country, string securityQuestion, string securityAnswer)
        : base(username, password, country, securityQuestion, securityAnswer)
        {
            IsAdmin = true;  // Sätter användaren som administratör
        }

        // Statisk metod för att hantera och samla alla användares träningspass
        public static List<Workout> ManageAllWorkouts(List<User> users)
        {
            List<Workout> allWorkouts = new List<Workout>();

            // Lägg till alla träningspass från varje användare
            foreach (var user in users)
            {
                allWorkouts.AddRange(user.Workouts);
            }

            return allWorkouts;  // Returnerar lista med alla träningspass
        }
    }

}
