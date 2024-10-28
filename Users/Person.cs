using System;
using System.Text.RegularExpressions;
using System.Windows;

namespace FitTrack.Users
{
    // Abstrakt klass Person som fungerar som grundklass för användare
    public abstract class Person
    {
        private string username;

        // Egenskap för användarnamn
        public string Username
        {
            get
            {
                return username;
            }
            set
            {

                if(value != null)
                {
                    
                    if (value.Length > 3)
                    {

                        username = value;  // Sätter användarnamnet
                    }
                    else
                    {
                        MessageBox.Show("Username must be lager than 3 characters");
                    }
                        
                }
                else
                {
                    MessageBox.Show("Username shall not be empty");
                }
                
               
            }
        }

        private string password;

        // Egenskap för lösenord, inkluderar validering genom CheckPassword-metoden
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
                    password = value;  // Sätter lösenordet om det är giltigt
                }
                else
                {
                    throw new Exception(error);  // Kastar undantag om lösenordet är ogiltigt
                }
            }
        }

        // Egenskap för att ange om användaren är administratör
        

        // Statisk egenskap för att lagra aktuell inloggad användare
        public static Person CurrentUser { get; set; }

        // Konstruktor för Person-objekt
        public Person(string username, string password)
        {
            Username = username;
            Password = password;
        }

        // Metod för att kontrollera lösenordets styrka
        public static string CheckPassword(string value)
        {
            bool isEight = false;  // Kontroll om lösenordet är minst 8 tecken
            bool containsSpecial = false;  // Kontroll om lösenordet innehåller specialtecken
            bool numberExist = false;  // Kontroll om lösenordet innehåller en siffra
            string specialCharacters = "!#¤%&/()=?";

            if(value != null)
            {
                if (value.Length >= 8)
                {
                    isEight = true;
                }
            }
            

            // Loop för att kontrollera om lösenordet innehåller specialtecken
            foreach (char item in specialCharacters.ToCharArray())
            {
                if(value != null)
                {
                    containsSpecial = value.Contains(item);
                    if (containsSpecial == true)
                    {
                        break;
                    }
                }
                
            };

            if(value != null) { 
            // Loop för att kontrollera om lösenordet innehåller siffror
                foreach (char item in value.ToCharArray())
                {
                    int num = 0;
                    numberExist = int.TryParse(item.ToString(), out num);
                    if (numberExist == true)
                    {
                        break;
                    }
                };
            }

            // Returnerar felmeddelanden om lösenordet inte uppfyller kraven
            if (numberExist == false || containsSpecial == false || isEight == false)
            {
                string stringError = "";
                if (numberExist == false)
                {
                    stringError = "Lösenordet måste innehålla en siffra, ";
                }

                if (containsSpecial == false)
                {
                    stringError += "lösenordet måste innehålla ett specialtecken, ";
                }

                if (isEight == false)
                {
                    stringError += "lösenordet måste innehålla minst åtta tecken, ";
                }

                return stringError;
            }
            return string.Empty;
        }

        // Abstrakt metod för att logga in användare
        public abstract bool SignIn(string username, string password);

        // Abstrakt metod för att registrera användare
        public abstract void RegisterUser(string username, string password);

        // Abstrakt metod för att lägga till säkerhetsfråga och svar
        public abstract void AddSecAuth(string Question, string Answer);
    }
}
