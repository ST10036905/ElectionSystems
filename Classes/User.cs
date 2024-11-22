using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionSystems
{
    public class User
    {
        /// <summary>
        /// Declaring properties for the User class using getters and setters.
        /// These properties represent the user's details.
        /// </summary>

        public string Email { get; set; } 
        public string Name { get; set; } 
        public int Age { get; set; } 
        public string Role { get; set; } 
        public string Password { get; set; } 
        public string Address { get; set; } 

        /// <summary>
        /// Default constructor for the User class.
        /// This constructor is used when creating an instance of the User class without initializing it with any values.
        /// </summary>
        public User()
        {
            // Default constructor doesn't require any specific initialization.
        }

        /// <summary>
        /// Parameterized constructor to initialize a User object with specific values.
        /// </summary>
        /// <param name="email">Email of the user.</param>
        /// <param name="name">Name of the user.</param>
        /// <param name="age">Age of the user.</param>
        /// <param name="role">Role of the user (e.g., Voter, Commissioner).</param>
        /// <param name="password">Password of the user.</param>
        /// <param name="address">Address of the user.</param>
        public User(string email, string name, int age, string role, string password, string address)
        {
            Email = email; 
            Name = name; 
            Age = age; 
            Role = role; 
            Password = password; 
            Address = address; 
        }

        /// <summary>
        /// Method to set the password of the user by hashing it before storing.
        /// </summary>
        /// <param name="password">Raw password entered by the user.</param>
        public void SetPassword(string password)
        {
            this.Password = HashPassword(password); // Hash and store the password.
        }

        /// <summary>
        /// Method to hash the password using SHA256 hashing algorithm.
        /// </summary>
        /// <param name="password">Raw password to be hashed.</param>
        /// <returns>The hashed password as a Base64 encoded string.</returns>
        public string HashPassword(string password)
        {
            // Using SHA256 to hash the password.
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                // Convert the hashed bytes to Base64 string and return it.
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
