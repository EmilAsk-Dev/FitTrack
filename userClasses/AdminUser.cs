﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTrack.User
{
    class AdminUser : Person
    {
        
        public AdminUser(string Username, string Password) : base(Username, Password) 
        {
            
        }

        public static void ManageAllWorkouts()
        {
            Console.WriteLine("ManageAllWorkouts()");
            // Check all workouts (maybe some search with Username that displays all their workouts using INotifyOnChange 
        }

        public override void RegisterUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public override bool SignIn(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}