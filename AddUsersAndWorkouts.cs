using FitTrack.Users;
using FitTrack.Workouts;
using System;

namespace FitTrack
{
    internal class AddUsersAndWorkouts
    {
        public static void AddUsersAndWorkoutsToDatabase()  
        {
            
            User user1 = new User("User1", "Password1!", "Sweden", "Where do u live", "malmo");
            User user2 = new User("User2", "Password1!", "Sweden", "hej", "hej");
            User admin = new AdminUser("Admin", "Password1!", "Sweden", "hej", "hej");

            
            AddWorkoutsToUser1(user1);

            
            AddWorkoutsToUser2(user2);

           
            AddWorkoutsToAdmin(admin);

            
            User.AddUser(user1);
            User.AddUser(user2);
            User.AddUser(admin); 
        }

        private static void AddWorkoutsToUser1(User user)
        {
            user.Workouts.Add(new StrenghtWorkout("Bänkpress", DateTime.Now.AddDays(-3), "strength", TimeSpan.FromMinutes(30), 2, "Bra övning för bröst", 10));
            user.Workouts.Add(new CardioWorkout("Jogging", DateTime.Now.AddDays(-2), "cardio", TimeSpan.FromMinutes(25), 200, "Jogging in the park", 3));
            user.Workouts.Add(new StrenghtWorkout("Benpress", DateTime.Now.AddDays(-1), "strength", TimeSpan.FromMinutes(40), 250, "Focusing on legs", 15));
            user.Workouts.Add(new CardioWorkout("Simning", DateTime.Now.AddDays(-4), "cardio", TimeSpan.FromMinutes(30), 300, "Swimming laps", 500));
            user.Workouts.Add(new StrenghtWorkout("Axelpress", DateTime.Now.AddDays(-5), "strength", TimeSpan.FromMinutes(35), 200, "Shoulder workout", 12));
        }

        private static void AddWorkoutsToUser2(User user)
        {
            user.Workouts.Add(new StrenghtWorkout("Marklyft", DateTime.Now.AddDays(-3), "strength", TimeSpan.FromMinutes(45), 300, "Full body strength", 20));
            user.Workouts.Add(new CardioWorkout("Cykling", DateTime.Now.AddDays(-2), "cardio", TimeSpan.FromMinutes(35), 350, "Biking through trails", 10));
            user.Workouts.Add(new StrenghtWorkout("Triceps Dips", DateTime.Now.AddDays(-1), "strength", TimeSpan.FromMinutes(30), 150, "Strengthening triceps", 15));
            user.Workouts.Add(new CardioWorkout("Högintensiv intervallträning", DateTime.Now.AddDays(-4), "cardio", TimeSpan.FromMinutes(20), 400, "HIIT workout", 5));
            user.Workouts.Add(new StrenghtWorkout("Squats", DateTime.Now.AddDays(-5), "strength", TimeSpan.FromMinutes(30), 220, "Leg strength", 18));
        }

        private static void AddWorkoutsToAdmin(User admin)
        {
            admin.Workouts.Add(new StrenghtWorkout("Styrketräning", DateTime.Now.AddDays(-3), "strength", TimeSpan.FromMinutes(30), 2, "En bra övning", 10));
            admin.Workouts.Add(new CardioWorkout("Löpning", DateTime.Now.AddDays(-2), "cardio", TimeSpan.FromMinutes(30), 300, "Morgonlöpning", 5));
            admin.Workouts.Add(new StrenghtWorkout("Benpress", DateTime.Now.AddDays(-1), "strength", TimeSpan.FromMinutes(45), 350, "Bra för benen", 12));
            admin.Workouts.Add(new CardioWorkout("Cykling", DateTime.Now.AddDays(-4), "cardio", TimeSpan.FromMinutes(40), 400, "Cykeltur i parken", 10));
            admin.Workouts.Add(new StrenghtWorkout("Bänkpress", DateTime.Now.AddDays(-5), "strength", TimeSpan.FromMinutes(35), 300, "Bra övning för bröst", 8));
        }
    }
}
