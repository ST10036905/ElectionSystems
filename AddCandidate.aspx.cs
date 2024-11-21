using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectionSystems
{
    public partial class AddCandidate : System.Web.UI.Page
    {

        User userData = new User();

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            // Checking if any of the text boxes are empty
            //    if (string.IsNullOrEmpty(EmailTxtBox.Text) ||
            //        string.IsNullOrEmpty(NameTxtBox.Text) ||
            //        string.IsNullOrEmpty(AgeTxtBox.Text) ||
            //        string.IsNullOrEmpty(PasswordTxtBox.Text) ||
            //        string.IsNullOrEmpty(ReEnterPasswordTxtBox.Text) ||
            //        string.IsNullOrEmpty(AddressTxtBox.Text))
            //    {
            //        ErrorMessageLabel.Text = "Please fill in all the fields.";
            //        return;
            //    }
            //    //filling the text boxes with form data
            //    userData.Email = EmailTxtBox.Text;
            //    userData.Name = NameTxtBox.Text;
            //    userData.Role = DropDownListRole.Text;
            //    userData.Password = PasswordTxtBox.Text.Trim();
            //    userData.Address = AddressTxtBox.Text;
            //    //performing validation on age.
            //    int age;
            //    if (!int.TryParse(AgeTxtBox.Text, out age))
            //    {
            //        ErrorMessageLabel.Text = "Please enter a valid age.";
            //        return;
            //    }
            //    //performing validation to ensure password and confirm passsword match.
            //    string confirmPassword = ReEnterPasswordTxtBox.Text.Trim();
            //    if (userData.Password != confirmPassword)
            //    {
            //        ErrorMessageLabel.Text = "Passwords do not match.";
            //        return;
            //    }
            //    //saving the user data to the database.
            //    userData.SaveUser();
            //    ErrorMessageLabel.Text = "User saved successfully.";
            //}
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            //Response.Redirect("AdminDashboard.aspx");
        }

    }
}