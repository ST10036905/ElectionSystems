using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ElectionSystems
{
    public partial class AddCandidate : System.Web.UI.Page
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

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(CandidateName.Text) ||
                string.IsNullOrWhiteSpace(Description.Value))
            {
                ErrorMessageLabel.Text = "Please fill in all required fields.";
                return;
            }


            // Save the image to the server
            string imagePath = SaveImage();

            if (string.IsNullOrEmpty(imagePath))
            {
                ErrorMessageLabel.Text = "Failed to upload the image.";
                return;
            }

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Candidate (Name, Description, Image) " +
                                   "VALUES (@Name, @Description, @Image)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", CandidateName.Text);
                        command.Parameters.AddWithValue("@Description", Description.Value);
                        command.Parameters.AddWithValue("@Image", imagePath);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }

                SucessMessageLabel.Text = "Candidate added successfully...Redirecting";
                string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 3000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                //Response.Redirect("MemberDashboard.aspx");
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        private string SaveImage()
        {
            try
            {
                if (CandidateImage.PostedFile != null && CandidateImage.PostedFile.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/UploadedImages/");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    string fileName = Path.GetFileName(CandidateImage.PostedFile.FileName);
                    string fullPath = Path.Combine(folderPath, fileName);
                    CandidateImage.PostedFile.SaveAs(fullPath);

                    // Return the relative path to store in the database
                    return "~/UploadedImages/" + fileName;
                }
            }
            catch (Exception ex)
            {
                ErrorMessageLabel.Text = "Image upload failed: " + ex.Message;
            }

            return null;
        }

        protected void CancelBtn_Click(object sender, EventArgs e)
        {
            CandidateName.Text = "";
            Description.Value = "";

            ErrorMessageLabel.Text = "Operation being canceled...";
            string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

    }
}