using FitTrack.Users;
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

    }
}
