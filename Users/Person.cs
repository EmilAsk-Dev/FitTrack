using System;
using System.Text.RegularExpressions;

namespace FitTrack.Users
{
    public abstract class Person
    {
        

        private string username;
        public string Username 
        {
            get
            {
                return username;
            }
            set 
            { 
                
                foreach (User person in ManageUser.userList)
                {
                    if(person.Username.ToLower() == value.ToLower())
                    {
                        throw new Exception("User does already exist");
                    }                    
                }
                username = value;
            }
        }

        private string password;

        public string Password
        {
            get
            {
                return password;
            } 
            set
            {
                string error = CheckPassword(value);
                if (error == string.Empty)
                {
                    password = value;
                }
                else
                {
                    throw new Exception(error);
                }
            }
        }

        public bool IsAdmin { get; set; } = false;


        public static Person CurrentUser { get; set; }

        public Person(string username, string password)
        {
            Username = username;
            Password = password;
        }
        

        public static string CheckPassword(string value)
        {
            bool isEight = false;
            bool containsSpecial = false;
            bool numberExist = false;
            string specialCharacters = "!#¤%&/()=?";

            if (value.Length >= 8)
            {
                isEight = true;
            }

            foreach (char item in specialCharacters.ToCharArray())
            {
                containsSpecial = value.Contains(item);
                if (containsSpecial == true)
                {
                    break;
                }
            };


            foreach (char item in value.ToCharArray())
            {
                int num = 0;
                numberExist = int.TryParse(item.ToString(), out num);
                if (numberExist == true)
                {
                    break;
                }
            };

            if (numberExist == false || containsSpecial == false || isEight == false)
            {
                string stringError = "";
                if (numberExist == false)
                {
                    stringError = "Your password must contain a number, ";
                }

                if (containsSpecial == false)
                {
                    stringError += "your Password must contain a special character, ";
                }

                if (isEight == false)
                {
                    stringError += "your Password must contain eight Characters, ";
                }

                return stringError;

            }
            return string.Empty;
        }

        public static Person FindUser(string username)
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


        


        public abstract bool SignIn(string username, string password);
        public abstract void RegisterUser(string username, string password);
        public abstract void AddSecAuth(string Question, string Answer);
    }
}
