using FitTrack.Users;
using FitTrack.Workouts;
using System;
using System.Collections.Generic;

namespace FitTrack
{
    internal class AddUsersAndWorkouts
    {
        public static void AddUsersAndWorkoutsToDatabase()
        {
           
            User user = new User("User", "12345678!", "Sweden", "where u bron", "malmo");
            //Person admin = new AdminUser("Admin", "12345678!","Sweden", "Where where you born", "malmo");

            AddRandomWorkoutsForPerson(user);
            //AddRandomWorkoutsForPerson(admin);

            
            //AdminUser.RegisterUser(admin);



            user.Workouts.Add(new StrenghtWorkout(DateTime.Now.AddDays(-3), "strenght", TimeSpan.FromMinutes(30), 2, "hej", 10));
            User.AddUser(user);






        }

        private static void AddRandomWorkoutsForPerson(User user)
        {
            if (user is User userBase)
            {
                AddWorkoutsToUser(userBase);
            }
            else if (user is AdminUser adminBase)
            {
                AddWorkoutsToAdmin(adminBase);
            }
        }

        private static void AddWorkoutsToUser(User user)
        {
            user.Workouts.Add(new CardioWorkout(
                DateTime.Now.AddDays(-3),
                "Cardio",
                TimeSpan.FromMinutes(30),
                300,
                "Morning run",
                5
            ));

            user.Workouts.Add(new StrenghtWorkout(
                DateTime.Now.AddDays(-2),
                "Strength",
                TimeSpan.FromMinutes(45),
                400,
                "Weight lifting at gym",
                20
            ));

            user.Workouts.Add(new CardioWorkout(
                DateTime.Now.AddDays(-1),
                "Cardio",
                TimeSpan.FromMinutes(45),
                350,
                "Cycling",
                15
            ));
        }

        private static void AddWorkoutsToAdmin(AdminUser adminBase)
        {
            adminBase.Workouts.Add(new CardioWorkout(
                DateTime.Now.AddDays(-3),
                "Cardio",
                TimeSpan.FromMinutes(30),
                300,
                "Morning run",
                5
            ));

            adminBase.Workouts.Add(new StrenghtWorkout(
                DateTime.Now.AddDays(-2),
                "Strength",
                TimeSpan.FromMinutes(45),
                400,
                "Weight lifting at gym",
                20
            ));

            adminBase.Workouts.Add(new CardioWorkout(
                DateTime.Now.AddDays(-1),
                "Cardio",
                TimeSpan.FromMinutes(45),
                350,
                "Cycling",
                15
            ));
        }
    }
}
