using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Web.UI;
// Handles user login functionality for the Election System.
// Allows users (Voters and Commissioners) to authenticate and navigate to their respective dashboards.

namespace ElectionSystems
{
    public partial class Login : System.Web.UI.Page 
    {
        /// <summary>
        /// Declaring and instantiating a User object to store user data.
        /// Encapsulation: The User object stores user details such as email and password securely.
        /// </summary>
        private User userData = new User();

        /// <summary>
        /// Declaring and initializing MongoDB connection details.
        /// Encapsulation: These fields are private, ensuring sensitive information (e.g., database connection details) is not directly accessible outside the class.
        /// </summary>
        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";
        private readonly string databaseName = "ElectionSystemDB";
        private readonly string commissionerCollection = "Commissioner";
        private readonly string voterCollection = "Voter";

        /// <summary>
        /// Handles the page load event for the Login page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // No specific action required during page load.
        }

        /// <summary>
        /// Handles the click event for the login button.
        /// Validates user input, checks credentials, and redirects the user based on their role.
        /// Polymorphism: The method demonstrates polymorphism by using different roles ("Voter", "Commissioner") to decide the redirection logic dynamically.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation: Ensures required fields are filled.
                if (string.IsNullOrEmpty(EmailTxtBox.Text) || string.IsNullOrEmpty(PasswordTxtBox.Text))
                {
                    ErrorMessageLabel.Text = "Please enter email and password.";
                    return;
                }

                // Encapsulation: User details are encapsulated within the User object.
                userData.Email = EmailTxtBox.Text;
                userData.Password = PasswordTxtBox.Text;

                // Validate credentials and retrieve user role and ID.
                var userDetail = ValidateLogin(userData.Email, userData.Password);

                if (userDetail.HasValue)
                {
                    if (userDetail.Value.Role == "Voter")
                    {
                        // Inheritance: Redirect logic uses functionality inherited from the Page class.
                        Session["VoterId"] = userDetail.Value.UserId;
                        SucessMessageLabel.Text = "Login successful...Redirecting voter";
                        string script = "setTimeout(function(){ window.location = 'Vote.aspx'; }, 2000);";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                    else if (userDetail.Value.Role == "Commissioner")
                    {
                        Session["CommissionerId"] = userDetail.Value.UserId;
                        SucessMessageLabel.Text = "Welcome back :) Redirecting.......";
                        string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 2000);";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
                    }
                }
                else
                {
                    ErrorMessageLabel.Text = "Invalid username or password";
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "Error: " + ex.Message;
            }
        }

        /// <summary>
        /// Validates user credentials by checking the MongoDB database for matching email and password.
        /// Polymorphism: The method dynamically checks two different collections ("User" and "Commissioner") to validate credentials.
        /// Encapsulation: Sensitive validation logic is encapsulated within this method.
        /// </summary>
        /// <param name="email">The email entered by the user.</param>
        /// <param name="password">The password entered by the user.</param>
        /// <returns>A tuple containing the user ID and role if valid; otherwise, null.</returns>
        public (string UserId, string Role)? ValidateLogin(string email, string password)
        {
            try
            {
                // Connect to MongoDB.
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);

                // Encapsulation: Validation logic for "User" collection.
                var userCollection = database.GetCollection<User>("User");
                var user = userCollection.Find(u => u.Email == email).FirstOrDefault();
                if (user != null && user.Password == userData.HashPassword(password))
                {
                    return (user.Email, user.Role);
                }

                // Encapsulation: Validation logic for "Commissioner" collection.
                var commissionerCollection = database.GetCollection<User>("Commissioner");
                var commissioner = commissionerCollection.Find(c => c.Email == email).FirstOrDefault();
                if (commissioner != null && commissioner.Password == userData.HashPassword(password))
                {
                    return (commissioner.Email, commissioner.Role);
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Handles the click event for the cancel button.
        /// Resets the input fields and redirects the user back to the default page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            EmailTxtBox.Text = "";
            PasswordTxtBox.Text = "";

            // Inheritance: Uses the ClientScript inherited from the Page class to execute JavaScript.
            ErrorMessageLabel.Text = "Operation being canceled...Loading";
            string script = "setTimeout(function(){ window.location = 'Default.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }
    }
}
