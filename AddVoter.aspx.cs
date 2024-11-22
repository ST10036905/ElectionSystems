using ElectionSystems.Classes;
using System;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;

namespace ElectionSystems
{
    public partial class AddVoter : System.Web.UI.Page
    {
        /// <summary>
        /// declaring an instance of voters class
        /// </summary>
        Voter voterData = new Voter();

        /// <summary>
        /// declaring and instantiating connection string
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mayra\\source\\repos\\ElectionSystems\\App_Data\\SystemDatabase.mdf;Integrated Security=True";


        protected void Page_Load(object sender, EventArgs e) { }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NameTxtBox.Text) || 
                string.IsNullOrEmpty(PasswordTxtBox.Text) ||
                string.IsNullOrEmpty(ReEnterPasswordTxtBox.Text) ||
                string.IsNullOrEmpty(EmailTxtBox.Text) ||
                string.IsNullOrEmpty(AddressTxtBox.Text))
            {
                ErrorMessageLabel.Text = "Please fill in all the fields.";
                return;
            }

            voterData.Name = NameTxtBox.Text;
            voterData.Email = EmailTxtBox.Text;
            voterData.Address = AddressTxtBox.Text;
            voterData.SetPassword(PasswordTxtBox.Text);

            if (!voterData.VerifyPassword(ReEnterPasswordTxtBox.Text))
            {
                ErrorMessageLabel.Text = "Password does not match.";
                return;
            }

            SaveVoterData(voterData.Name, voterData.Email, voterData.Address, voterData.Password, voterData.Role);

            SucessMessageLabel.Text = "Voter registration successful.";
            string script = "setTimeout(function(){ window.location = 'MemberDashvoard.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            //Response.Redirect("MemberDashboard.aspx");
        }

        public void SaveVoterData(string name, string email, string address, string password,string role)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        string voterQuery = "INSERT INTO Voter (Name, Password, Email, Address, Role) VALUES (@Name,@Password, @Email, @Address, @Role)";
                        using (SqlCommand voterCommand = new SqlCommand(voterQuery, conn, transaction))
                        {
                            voterCommand.Parameters.AddWithValue("@Name", name);
                            voterCommand.Parameters.AddWithValue("@Email", email);
                            voterCommand.Parameters.AddWithValue("@Address", address);
                            voterCommand.Parameters.AddWithValue("@Password", password);
                            voterCommand.Parameters.AddWithValue("@Role", role);
                            voterCommand.ExecuteNonQuery();
                        }

                        string userQuery = "INSERT INTO [User] (Email, Password, Role) VALUES (@Email, @Password, @Role)";
                        using (SqlCommand userCommand = new SqlCommand(userQuery, conn, transaction))
                        {
                            userCommand.Parameters.AddWithValue("@Email", email);
                            userCommand.Parameters.AddWithValue("@Password", password);
                            userCommand.Parameters.AddWithValue("@Role", role);
                            userCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred while saving the voter: " + ex.Message;
            }
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            NameTxtBox.Text = "";
            PasswordTxtBox.Text = "";
            ReEnterPasswordTxtBox.Text = "";
            EmailTxtBox.Text = "";
            AddressTxtBox.Text = "";

            ErrorMessageLabel.Text = "Operation being canceled...";
            string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

    }
}
