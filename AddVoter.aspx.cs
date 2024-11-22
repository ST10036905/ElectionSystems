using ElectionSystems.Classes; 
using MongoDB.Driver;          
using System;                  
using System.Web.UI;        

// Code-behind for the AddVoter web page. 
// Handles voter registration by interacting with the MongoDB database.
// Demonstrates object-oriented principles such as encapsulation and inheritance.

namespace ElectionSystems
{
  
    public partial class AddVoter : System.Web.UI.Page
    {
        #region Fields and MongoDB Connection Setup

        /// <summary>
        /// Encapsulated MongoDB collections for voters and users.
        /// </summary>
        private readonly IMongoCollection<Voter> votersCollection; // Encapsulation: Access restricted to this class.
        private readonly IMongoCollection<User> usersCollection;   // Encapsulation: Access restricted to this class.

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor initializes the MongoDB client and connects to the ElectionSystemDB database.
        /// </summary>
        public AddVoter()
        {
            // Polymorphism: MongoClient uses MongoDB.Driver's underlying implementation for database connectivity.
            var client = new MongoClient("mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true");

            // Get the required database
            var database = client.GetDatabase("ElectionSystemDB");

            // Access collections for Voter and User entities
            votersCollection = database.GetCollection<Voter>("Voter");
            usersCollection = database.GetCollection<User>("User");
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handles the page load event.
        /// Currently, this method is unused but serves as an entry point for initializing page-level resources.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) { }

        /// <summary>
        /// Handles the save button click event to validate user input and save voter data.
        /// Implements input validation and securely stores data using MongoDB.
        /// </summary>
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            #region Input Validation

            // Check for missing input fields
            if (string.IsNullOrEmpty(NameTxtBox.Text) ||
                string.IsNullOrEmpty(EmailTxtBox.Text) ||
                string.IsNullOrEmpty(AddressTxtBox.Text) ||
                string.IsNullOrEmpty(PasswordTxtBox.Text) ||
                string.IsNullOrEmpty(ReEnterPasswordTxtBox.Text))
            {
                ErrorMessageLabel.Text = "Please fill in all the fields.";
                return;
            }

            // Validate and parse the age input
            if (!int.TryParse(AgeTxtBox.Text, out int age))
            {
                ErrorMessageLabel.Text = "Please enter a valid age.";
                return;
            }

            // Ensure passwords match
            if (PasswordTxtBox.Text.Trim() != ReEnterPasswordTxtBox.Text.Trim())
            {
                ErrorMessageLabel.Text = "Passwords do not match.";
                return;
            }

            #endregion

            #region Voter Object Creation

            // Encapsulation: Data stored in an object with restricted access.
            Voter voterData = new Voter
            {
                Name = NameTxtBox.Text.Trim(),
                Email = EmailTxtBox.Text.Trim(),
                Address = AddressTxtBox.Text.Trim(),
                Age = age,
                Role = "Voter"
            };

            // Hash the password before storing it
            voterData.SetPassword(PasswordTxtBox.Text.Trim());

            #endregion

            #region Save to Database

            try
            {
                // Save the voter data
                SaveVoterToDatabase(voterData);
                SucessMessageLabel.Text = "Voter registration successful...Redirecting";

                // Redirect to dashboard after 2 seconds
                string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 2000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
            }
            catch (Exception ex)
            {
                // Display error message to the user
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }

            #endregion
        }

        /// <summary>
        /// Handles the cancel button click event.
        /// Clears all input fields and redirects the user to the dashboard.
        /// </summary>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            // Clear all input fields
            ClearFields();

            // Inform the user about the cancellation
            ErrorMessageLabel.Text = "Operation being canceled...";

            // Redirect to the dashboard after 2 seconds
            string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Saves the voter data into the MongoDB collections.
        /// Adds data to both the Voter collection and the User collection for authentication.
        /// Demonstrates encapsulation by keeping database logic within a private method.
        /// </summary>
        /// <param name="voter">The voter data object to be saved.</param>
        private void SaveVoterToDatabase(Voter voter)
        {
            try
            {
                // Insert voter data into the Voter collection
                votersCollection.InsertOne(voter);

                // Create a corresponding user object for authentication purposes
                var user = new User
                {
                    Email = voter.Email,
                    Password = voter.Password, // Store hashed password
                    Age = voter.Age,
                    Role = voter.Role,
                    Address = voter.Address,
                    Name = voter.Name
                };

                // Insert user data into the User collection
                usersCollection.InsertOne(user);
            }
            catch (Exception ex)
            {
                // Throw an exception with a detailed error message
                throw new Exception("Error while saving voter data: " + ex.Message);
            }
        }

        /// <summary>
        /// Clears all input fields on the page.
        /// </summary>
        private void ClearFields()
        {
            NameTxtBox.Text = "";
            EmailTxtBox.Text = "";
            AgeTxtBox.Text = "";
            AddressTxtBox.Text = "";
            PasswordTxtBox.Text = "";
            ReEnterPasswordTxtBox.Text = "";
        }

        #endregion
    }
}
