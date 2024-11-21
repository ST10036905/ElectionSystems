using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ElectionSystems.Classes
{
    public class Voter
    {

        /// <summary>
        /// declaring getters and setters for user.
        /// </summary>
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public string Role = "Voter";
        public string Password { get; set; }
        public string Address { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public Voter()
        {
        }

        /// <summary>
        /// parameterised constructor for registration form.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="name"></param>
        /// <param name="age"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <param name="address"></param>
        public Voter(string email, string name, int age, string role, string password, string address)
        {
            Email = email;
            Name = name;
            Age = age;
            Role = role;
            Password = password;
            Address = address;
        }

        public void SetPassword(string password)
        {
            Password = HashPassword(password);
        }

        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string inputPassword)
        {
            return Password.Equals(HashPassword(inputPassword));
        }

    }
}