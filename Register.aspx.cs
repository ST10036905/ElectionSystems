using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Driver;

namespace ElectionSystems
{
    public partial class Register : System.Web.UI.Page
    {

        /// <summary>
        /// Creating an instance of the User class.
        /// </summary>
        User userData = new User();

        private readonly IMongoCollection<User> userCollection;
        private readonly IMongoCollection<User> commissionerCollection;

        public Register()
        {
            var client = new MongoClient("mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true");
            var database = client.GetDatabase("ElectionSystemDB");

            userCollection = database.GetCollection<User>("User");
            commissionerCollection = database.GetCollection<User>("Commissioner");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Button click event that saves registration data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            // Checking if any of the text boxes are empty
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

            // Checking if the selected role is "Voter", and prevent registration if it is
            if (DropDownListRole.SelectedValue == "Voter")
            {
                ErrorMessageLabel.Text = "Voters cannot register, please contact our admin and commission member.";
                ClearFields();
                return;
            }

            // Filling the text boxes with form data
            userData.Email = EmailTxtBox.Text;
            userData.Name = NameTxtBox.Text;
            userData.Role = DropDownListRole.Text;
            userData.Address = AddressTxtBox.Text;

            // Performing validation on age.
            if (!int.TryParse(AgeTxtBox.Text, out int age))
            {
                ErrorMessageLabel.Text = "Please enter a valid age.";
                return;
            }

            // Performing validation to ensure password and confirm password match.
            string password = PasswordTxtBox.Text.Trim();
            string confirmPassword = ReEnterPasswordTxtBox.Text.Trim();
            if (password != confirmPassword)
            {
                ErrorMessageLabel.Text = "Password does not match.";
                return;
            }

            // Hash the password before saving
            userData.SetPassword(password);

            var newUser = new User
            {
                Email = userData.Email,
                Name = userData.Name,
                Age = age,
                Role = userData.Role,
                Password = userData.Password, // Hashed password
                Address = userData.Address
            };

            // Proceed with registration
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


        private void RegisterUser(User newUser)
        {
            var client = new MongoClient("mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true");
            var database = client.GetDatabase("ElectionSystemDB");
            var userCollection = database.GetCollection<User>("User");

            // Insert the user into the database
            userCollection.InsertOne(newUser);
        }


        /// <summary>
        /// Button user selects to redirect to cancel the operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
           
            ClearFields();
            //Using a startup script to redirect after 2 seconds
            ErrorMessageLabel.Text = "Operation being canceled...";
            string script = "setTimeout(function(){ window.location = 'Default.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }//_____________________________________________________________________________________________________________

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

    }
 }