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
        [BsonId]
        public ObjectId Id { get; set; }
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

        public string HashPassword(string password)
        {
            // Using bcrypt to hash the password
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public void SetPassword(string password)
        {
            this.Password = HashPassword(password); // Hash and store the password.
        }
    }
}
