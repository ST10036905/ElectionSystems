using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectionSystems
{
    public class User
    {

        /// <summary>
        /// declaring getters and setters for user.
        /// </summary>
        public string Email { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
  


        /// <summary>
        /// default constructor
        /// </summary>
        public User()
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
        public User(string email,  string name, int age, string role, string password, string address)
        {
            Email = email;
            Name = name;
            Age = age;
            Role = role;
            Password = password;
            Address = address;
        }//_____________________________________________________________________________________________________________

        /// <summary>
        /// setting the hashed password.
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            this.Password = HashPassword(password);
        }//_____________________________________________________________________________________________________________


        /// <summary>
        /// method that hashes password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string HashPassword(string password)
        {

            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }//_____________________________________________________________________________________________________________

    }
}