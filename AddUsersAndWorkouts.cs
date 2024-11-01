using FitTrack.Users;
using FitTrack.Workouts;
using System;

namespace FitTrack
{
    internal class AddUsersAndWorkouts
    {
        public static void AddUsersAndWorkoutsToDatabase()  
        {
            
            User user1 = new User("user1", "Password1!", "Sweden", "Where do you live", "malmo");
            User user2 = new User("user2", "Password1!", "Sweden", "Where do you live", "hej");
            User admin = new AdminUser("admin", "Password1!", "Sweden", "Where do you live", "hej");

            Console.WriteLine($"Username: User1 - Password: Password1! ");
            Console.WriteLine($"Username: User2 - Password: Password1! ");
            Console.WriteLine($"Username: Admin - Password: Password1! ");

            AddWorkoutsToUser1(user1);
            

            
            AddWorkoutsToUser2(user2);

           
            AddWorkoutsToAdmin(admin);

            
            User.AddUser(user1);
            User.AddUser(user2);
            User.AddUser(admin); 
        }

        private static void AddWorkoutsToUser1(User user)
        {
            user.Workouts.Add(new StrenghtWorkout("Bänkpress", DateTime.Now.AddDays(-3), "strength", TimeSpan.FromMinutes(30), "Bra övning för bröst", 10));
            user.Workouts.Add(new CardioWorkout("Jogging", DateTime.Now.AddDays(-2), "cardio", TimeSpan.FromMinutes(25), "Jogging in the park", 3));
            user.Workouts.Add(new StrenghtWorkout("Benpress", DateTime.Now.AddDays(-1), "strength", TimeSpan.FromMinutes(40), "Focusing on legs", 15));
            user.Workouts.Add(new CardioWorkout("Simning", DateTime.Now.AddDays(-4), "cardio", TimeSpan.FromMinutes(30), "Swimming laps", 500));
            user.Workouts.Add(new StrenghtWorkout("Axelpress", DateTime.Now.AddDays(-5), "strength", TimeSpan.FromMinutes(35), "Shoulder workout", 12));
        }

        private static void AddWorkoutsToUser2(User user)
        {
            user.Workouts.Add(new StrenghtWorkout("Marklyft", DateTime.Now.AddDays(-3), "strength", TimeSpan.FromMinutes(45), "Full body strength", 20));
            user.Workouts.Add(new CardioWorkout("Cykling", DateTime.Now.AddDays(-2), "cardio", TimeSpan.FromMinutes(35), "Biking through trails", 10));
            user.Workouts.Add(new StrenghtWorkout("Triceps Dips", DateTime.Now.AddDays(-1), "strength", TimeSpan.FromMinutes(30), "Strengthening triceps", 15));
            user.Workouts.Add(new CardioWorkout("Högintensiv intervallträning", DateTime.Now.AddDays(-4), "cardio", TimeSpan.FromMinutes(20), "HIIT workout", 5));
            user.Workouts.Add(new StrenghtWorkout("Squats", DateTime.Now.AddDays(-5), "strength", TimeSpan.FromMinutes(30), "Leg strength", 18));
        }

        private static void AddWorkoutsToAdmin(User admin)
        {
            admin.Workouts.Add(new StrenghtWorkout("Styrketräning", DateTime.Now.AddDays(-3), "strength", TimeSpan.FromMinutes(30), "En bra övning", 10));
            admin.Workouts.Add(new CardioWorkout("Löpning", DateTime.Now.AddDays(-2), "cardio", TimeSpan.FromMinutes(30), "Morgonlöpning", 5));
            admin.Workouts.Add(new StrenghtWorkout("Benpress", DateTime.Now.AddDays(-1), "strength", TimeSpan.FromMinutes(45), "Bra för benen", 12));
            admin.Workouts.Add(new CardioWorkout("Cykling", DateTime.Now.AddDays(-4), "cardio", TimeSpan.FromMinutes(40), "Cykeltur i parken", 10));
            admin.Workouts.Add(new StrenghtWorkout("Bänkpress", DateTime.Now.AddDays(-5), "strength", TimeSpan.FromMinutes(35), "Bra övning för bröst", 8));
        }
    }
}
