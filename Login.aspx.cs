using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        /// <summary>
        /// declaring and instantiating connection string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mayra\\source\\repos\\ElectionSystems\\App_Data\\SystemDatabase.mdf;Integrated Security=True";


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Checking if email or password is empty
                if (string.IsNullOrEmpty(EmailTxtBox.Text) || string.IsNullOrEmpty(PasswordTxtBox.Text))
                {
                    ErrorMessageLabel.Text = "Please enter email and password.";
                    return;
                }

                //filling the text boxes with form data
                userData.Email = EmailTxtBox.Text;
                userData.Password = PasswordTxtBox.Text;
                var userDetail = ValidateLogin(userData.Email, userData.Password);

                //validating based on role who is to user in order to redirect them to right form.
                if (userDetail != null)
                {
                    if (userDetail.Value.Role == "Voter")
                    {
                        Session["VoterId"] = userDetail.Value.UserId;

                        // Using a startup script to redirect after 2 seconds
                        SucessMessageLabel.Text = "Login succesful...Redirecting voter";
                        string script = "setTimeout(function(){ window.location = 'Vote.aspx'; }, 2000);";
                        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                    }
                    else if (userDetail.Value.Role == "Commissioner")
                    {
                        Session["CommissionerId"] = userDetail.Value.UserId;

                        // Using a startup script to redirect after 2 seconds
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
        }//_________________________________________________________________________________________________________________


        /// <summary>
        /// Method used to validate login credentials using a query
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public (string UserId, string Role)? ValidateLogin(string email, string password)
        {
            (string UserId, string Role)? userDetail = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string query = @"
                        SELECT CommissionerId AS UserId, 'Commissioner' AS Role, Password 
                        FROM Commissioner 
                        WHERE Email = @Email
                        UNION ALL 
                        SELECT VoterId AS UserId, 'Voter' AS Role, Password 
                        FROM Voter 
                        WHERE Email = @Email";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string storedHashedPassword = reader["Password"].ToString();
                                string hashedPassword = userData.HashPassword(password);

                                if (hashedPassword == storedHashedPassword)
                                {
                                    userDetail = (reader["UserId"].ToString(), reader["Role"].ToString());
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }
            return userDetail;
        }//_________________________________________________________________________________________________________________


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