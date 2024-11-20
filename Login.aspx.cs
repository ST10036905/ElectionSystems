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
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginBtn_Click(object sender, EventArgs e)
        {
            try
            {
                //// Checking if username or password is empty
                //if (string.IsNullOrEmpty(UsernameTxtBox.Text) || string.IsNullOrEmpty(PasswordTxtBox.Text))
                //{
                //    ErrorMessageLabel.Text = "Please enter username and password.";
                //    return;
                //}

                ////filling the text boxes with form data
                //userData.Username = UsernameTxtBox.Text;
                //userData.Password = PasswordTxtBox.Text;
                //var userDetail = ValidateLogin(userData.Username, userData.Password);

                ////validating based on role who is to user in order to redirect them to right form.
                //if (userDetail != null)
                //{
                //    if (userDetail.Value.Role == "Farmer")
                //    {
                //        Session["FarmerId"] = userDetail.Value.UserId;

                //        // Using a startup script to redirect after 5 seconds
                //        SucessMessageLabel.Text = "Login succesful...Redirecting farmer";
                //        string script = "setTimeout(function(){ window.location = 'Dashboard.aspx'; }, 5000);";
                //        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                //    }
                //    else if (userDetail.Value.Role == "Employee")
                //    {
                //        Session["EmployeeId"] = userDetail.Value.UserId;

                //        // Using a startup script to redirect after 5 seconds
                //        SucessMessageLabel.Text = "Welcome back :) Redirecting.......";
                //        string script = "setTimeout(function(){ window.location = 'EmployeeDashboard.aspx'; }, 5000);";
                //        ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                //    }
                //}
                //else
                //{
                //    ErrorMessageLabel.Text = "Invalid username or password";
                //}
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "Error: " + ex.Message;
            }
        }//_________________________________________________________________________________________________________________


        /// <summary>
        /// Method to cancel login operation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            UsernameTxtBox.Text = "";
            PasswordTxtBox.Text = "";

            //Using a startup script to redirect after 5 seconds
            ErrorMessageLabel.Text = "Operation being canceled...Loading";
            string script = "setTimeout(function(){ window.location = 'Default.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }//_________________________________________________________________________________________________________________


    }
}