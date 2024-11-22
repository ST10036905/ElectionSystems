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
        /// Declaring properties for the Voter class using getters and setters.
        /// </summary>
        public string Email { get; set; } 
        public string Name { get; set; } 
        public int Age { get; set; } 

        public string Role = "Voter"; 
        public string Password { get; set; } 
        public string Address { get; set; } 

        /// <summary>
        /// Default constructor for the Voter class.
        /// </summary>
        public Voter()
        {
            // No initialization required for the default constructor.
        }

        /// <summary>
        /// Parameterized constructor to initialize a Voter object with specific values.
        /// </summary>
        /// <param name="email">Email of the voter.</param>
        /// <param name="name">Name of the voter.</param>
        /// <param name="age">Age of the voter.</param>
        /// <param name="role">Role of the user, default is "Voter".</param>
        /// <param name="password">Password of the voter.</param>
        /// <param name="address">Address of the voter.</param>
        public Voter(string email, string name, int age, string role, string password, string address)
        {
            Email = email;
            Name = name; 
            Age = age; 
            Role = role; 
            Password = password; 
            Address = address; 
        }

        /// <summary>
        /// Sets the hashed password for the voter.
        /// </summary>
        /// <param name="password">Raw password to hash and store.</param>
        public void SetPassword(string password)
        {
            Password = HashPassword(password); // Hash and store the password.
        }

        /// <summary>
        /// Generates a hashed representation of a password using SHA256.
        /// </summary>
        /// <param name="password">The raw password to hash.</param>
        /// <returns>The hashed password as a string.</returns>
        public static string HashPassword(string password)
        {
            // Create an SHA256 hashing object.
            using (SHA256 sha256 = SHA256.Create())
            {
                // Compute the hash of the input password.
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                // Convert the hash bytes to a hexadecimal string and return it.
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        /// <summary>
        /// Verifies if an input password matches the stored hashed password.
        /// </summary>
        /// <param name="inputPassword">The raw password entered by the user.</param>
        /// <returns>True if the password matches, otherwise false.</returns>
        public bool VerifyPassword(string inputPassword)
        {
            // Compare the input password after hashing it with the stored hashed password.
            return Password.Equals(HashPassword(inputPassword));
        }
    }
}
