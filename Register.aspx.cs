using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;

// Handles commission member registration functionality for the Election System.

namespace ElectionSystems
{
    public partial class Register : System.Web.UI.Page 
    {
        /// <summary>
        /// Encapsulation: The User object encapsulates user-related data like email, name, role, etc.
        /// </summary>
        private User userData = new User();

        /// <summary>
        /// Encapsulation: These fields encapsulate the database collections and restrict direct access to them.
        /// </summary>
        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<User> commissionerCollection;

        // MongoDB client initialization
        private readonly MongoClient client;
        private readonly IMongoDatabase database;

        /// <summary>
        /// Constructor initializes MongoDB client and collections.
        /// Encapsulation: Database connection and initialization are abstracted within the constructor.
        /// </summary>
        public Register()
        {
            var client = new MongoClient("mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true");
            var database = client.GetDatabase("ElectionSystemDB");

            userCollection = database.GetCollection<User>("User");
            commissionerCollection = database.GetCollection<User>("Commissioner");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // No specific logic required on page load.
        }

        /// <summary>
        /// Polymorphism: SaveBtn_Click dynamically handles different roles and applies logic specific to the selected role.
        /// </summary>
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            // Validation logic to ensure fields are filled and correct.
            if (string.IsNullOrEmpty(EmailTxtBox.Text) ||
                string.IsNullOrEmpty(NameTxtBox.Text) ||
                string.IsNullOrEmpty(AgeTxtBox.Text) ||
                string.IsNullOrEmpty(PasswordTxtBox.Text) ||
                string.IsNullOrEmpty(ReEnterPasswordTxtBox.Text) ||
                string.IsNullOrEmpty(AddressTxtBox.Text))
            {
                ErrorMessageLabel.Text = "Please fill in all the fields.";
                return;
            }

            // Polymorphism: Handle role-specific restrictions dynamically.
            if (DropDownListRole.SelectedValue == "Voter")
            {
                ErrorMessageLabel.Text = "Voters cannot register, please contact our admin and commission member.";
                ClearFields();
                return;
            }

            // Check if passwords match
            if (PasswordTxtBox.Text.Trim() != ReEnterPasswordTxtBox.Text.Trim())
            {
                ErrorMessageLabel.Text = "Passwords do not match.";
                return;
            }

            // Hash the password before storing it
            string hashedPassword = userData.HashPassword(PasswordTxtBox.Text.Trim());

            // Encapsulation: User data is stored securely within the User object.
            userData.Email = EmailTxtBox.Text;
            userData.Name = NameTxtBox.Text;
            userData.Role = DropDownListRole.SelectedValue;
            userData.Address = AddressTxtBox.Text;

            // Validate age
            if (!int.TryParse(AgeTxtBox.Text, out int age))
            {
                ErrorMessageLabel.Text = "Please enter a valid age.";
                return;
            }

            // Encapsulation: Securely set the password.
            userData.SetPassword(hashedPassword);

            var newUser = new User
            {
                Email = userData.Email,
                Name = userData.Name,
                Age = age,
                Role = userData.Role,
                Password = userData.Password,
                Address = userData.Address
            };

            try
            {
                RegisterUser(newUser);
                SucessMessageLabel.Text = "Registration successful....Loading";
                string script = "setTimeout(function(){ window.location = 'Login.aspx'; }, 2000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        /// <summary>
        /// Registers the user based on their role.
        /// Encapsulation: The database interaction is abstracted within this method.
        /// Polymorphism: Dynamically handles Commissioner-specific and general user registrations differently.
        /// </summary>
        private void RegisterUser(User newUser)
        {
            try
            {
                // Restrict voter registration
                if (newUser.Role == "Voter")
                {
                    throw new Exception("Voters cannot register. Please contact an admin.");
                }

                // Role-based registration logic
                if (newUser.Role == "Commissioner")
                {
                    userCollection.InsertOne(newUser); // General user tracking
                    commissionerCollection.InsertOne(newUser); // Commissioner-specific tracking
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred while saving the user data: " + ex.Message;
            }
        }

        /// <summary>
        /// Clears all input fields on the form.
        /// Encapsulation: Field-clearing logic is encapsulated within a reusable method.
        /// </summary>
        private void ClearFields()
        {
            EmailTxtBox.Text = "";
            NameTxtBox.Text = "";
            AgeTxtBox.Text = "";
            DropDownListRole.SelectedIndex = -1;
            PasswordTxtBox.Text = "";
            ReEnterPasswordTxtBox.Text = "";
            AddressTxtBox.Text = "";
        }

        /// <summary>
        /// Redirects the user back to the default page.
        /// Inheritance: Uses the ClientScript functionality inherited from System.Web.UI.Page.
        /// </summary>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            ClearFields();
            ErrorMessageLabel.Text = "Operation being canceled...";
            string script = "setTimeout(function(){ window.location = 'Default.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    }
}
