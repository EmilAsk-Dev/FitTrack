﻿using System.Collections.Generic;
using System.Windows;

namespace FitTrack.User
{
    public class User : Person
    {
        
        public static User CurrentUser { get; set; }

        public string Country { get; set; }
        public string SecurityQuestion { get; set; }
        public string SecurityAnswer { get; set; }

        
        public List<string> Workouts { get; set; }

        private static List<User> userList = new List<User>();

        public User(string username, string password, string country, string securityQuestion, string securityAnswer)
            : base(username, password)
        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
            Workouts = new List<string>(); 
        }

        public User(string username, string password)
            : base(username, password)
        {
            Workouts = new List<string>(); 
        }

        public override bool SignIn(string username, string password)
        {
            foreach (var user in userList)
            {
                if (user.Username == username && user.Password == password)
                {
                    User.CurrentUser = user;
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

        public static void AddUser(User user)
        {
            userList.Add(user);
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
    }
}
