using FitTrack.Users;
using FitTrack.Workouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack
{
    class ManageUser
    {
        public static List<User> userList = new List<User>();

        public static User currentUser {  get; set; }

        
        
        public static User FindUser(string username)
        {
            foreach (User person in ManageUser.userList)
            {
                if (person.Username.ToLower() == username.ToLower())
                {
                    return person;
                }
            }
            return null;
        }

        public static List<User> GetAllUsers()
        {
            return userList; 
        }

        public static void RemoveWorkoutFromUser(Workout workout, User targetUser)
        {
            if (targetUser.Workouts.Contains(workout))
            {
                targetUser.Workouts.Remove(workout); //tar bort träningspasset för en specifik användare
            }
        }

        public static User GetUserByWorkout(Workout workout)
        {
            
            foreach (User user in userList) 
            {
                if (user.Workouts.Contains(workout))
                {
                    return user; //kastar tillbaka workouten som ägs av ägaren
                }
            }
            return null; //om den inte finns
        }

    }
}
