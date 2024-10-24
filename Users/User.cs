﻿using FitTrack.Workouts;
using System.Collections.Generic;
using System.Windows;

namespace FitTrack.Users
{
    public class User : Person
    {
        

        public string Country { get; set; }
        private string SecurityQuestion { get; set; }
        private string SecurityAnswer { get; set; }

        public List<Workout> Workouts { get; set; }

        
        public User(string username, string password, string country, string securityQuestion, string securityAnswer)
            : base(username, password)
        {
            Country = country;
            SecurityQuestion = securityQuestion;
            SecurityAnswer = securityAnswer;
            Workouts = new List<Workout>();
        }

        public User(string username, string password)
            : base(username, password)
        {
            Workouts = new List<Workout>();
        }

        public override bool SignIn(string username, string password)
        {
            foreach (var user in ManageUser.userList)
            {
                bool isAdmin = user.GetType() == typeof(AdminUser);
                if (user.Username.ToLower() == username.ToLower() && user.Password == password)
                {                    
                    ManageUser.currentUser = user;
                    return true;
                }
            }
            MessageBox.Show("Invalid credentials.");
            return false;
        }

        public void RegisterExisting(User user)
        {
            ManageUser.userList.Add(user);
        }

        public static void RegisterUser(User user)
        {
            User newUser = new User(user.Username, user.Password, user.Country, user.SecurityAnswer, user.SecurityQuestion);
            ManageUser.userList.Add(newUser);
        }

        public override void RegisterUser(string username, string password)
        { 
            User newUser = new User(username, password);
            ManageUser.userList.Add(newUser);
        }

        public void RegisterUser(string username, string password, string country, string securityQuestion, string securityAnswer)
        {
            User newUser = new User(username, password);
            ManageUser.userList.Add(newUser);
        }

        public static void AddUser(User user)
        {
            ManageUser.userList.Add(user);
        }

        public static void ResetPassword(string username, string newPassword)
        {
            foreach (var user in ManageUser.userList)
            {
                if (user.Username == username)
                {
                    user.Password = newPassword;
                    MessageBox.Show($"Password reset successfully for {username}.");
                    return;
                }
            }

            MessageBox.Show($"User with username {username} not found.");
        }

        public static bool IfUserExist(string username)
        {
            foreach (var user in ManageUser.userList)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IfSecurityAnswer(string securityAnswer)
        {
            if(securityAnswer.ToLower() == SecurityAnswer.ToLower())
            {
                return true;
            }
            return false;
                        
        }

        public bool HaveAuth(User user)
        {
            if(user.SecurityAnswer == null && user.SecurityQuestion == null) 
            {
                return false;
            }
            return true;
        }

        public override void AddSecAuth(string question, string answer)
        {
            this.SecurityQuestion = question;
            this.SecurityAnswer = answer;
        }

        public void GetSecurityQA(out string answer, out string question)
        {
            answer = this.SecurityAnswer;
            question = this.SecurityQuestion;
        }
    }
}
