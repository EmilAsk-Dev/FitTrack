using System;

namespace FitTrack.User
{
    public abstract class Person 
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Person(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public abstract bool SignIn(string username, string password);
        public abstract void RegisterUser(string username, string password);
    }
}
