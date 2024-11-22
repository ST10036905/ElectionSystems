using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectionSystems
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// Declaring and instantiating an object from the users class
        /// </summary>
        User userData = new User();

        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";
        private readonly string databaseName = "ElectionSystemDB";
        private readonly string commisionerCollection = "commissioner";
        private readonly string voterCollection = "voter";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(EmailTxtBox.Text) || string.IsNullOrEmpty(PasswordTxtBox.Text))
                {
                    ErrorMessageLabel.Text = "Please enter email and password.";
                    return;
                }

                userData.Email = EmailTxtBox.Text;
                userData.Password = PasswordTxtBox.Text;

                var userDetail = ValidateLogin(userData.Email, userData.Password);

                if (userDetail != null)
                {
                    if (userDetail.Value.Role == "Voter")
                    {
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

        public (string UserId, string Role)? ValidateLogin(string email, string password)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);
                var userCollection = database.GetCollection<User>("User");

                var user = userCollection.Find(u => u.Email == email).FirstOrDefault();

                if (user != null)
                {
                    string storedHashedPassword = user.Password;
                    if (storedHashedPassword == userData.HashPassword(password)) 
                    {
                        return (user.Email, user.Role);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }

            return null;
        }

        /// <summary>
        /// Method to cancel login operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            EmailTxtBox.Text = "";
            PasswordTxtBox.Text = "";

            //Using a startup script to redirect after 2 seconds
            ErrorMessageLabel.Text = "Operation being canceled...Loading";
            string script = "setTimeout(function(){ window.location = 'Default.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }//_________________________________________________________________________________________________________________

    }
}