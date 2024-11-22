using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ElectionSystems
{
    public partial class AddCandidate : System.Web.UI.Page
    {
        /// <summary>
        /// Declaring and instantiating an object from the users class
        /// </summary>
        User userData = new User();

        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
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

                // Insert candidate data into the database
                AddCandidateToDatabase(CandidateName.Text, Description.Value, imagePath);

                // Display success message and redirect
                SucessMessageLabel.Text = "Candidate added successfully! Redirecting...";
                string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 3000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

            }
            catch (Exception ex)
            {
                // Log exception (placeholder for future logging implementation)
                ErrorMessageLabel.Text = $"An error occurred: {ex.Message}";
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

                    // Generate unique file name to avoid overwriting existing files
                    string fileName = Path.GetFileNameWithoutExtension(CandidateImage.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(CandidateImage.PostedFile.FileName);
                    string uniqueFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                    string fullPath = Path.Combine(folderPath, uniqueFileName);

                    // Save file to server
                    CandidateImage.PostedFile.SaveAs(fullPath);

                    // Return the relative path to store in the database
                    return $"~/UploadedImages/{uniqueFileName}";
                }
            }
            catch (Exception ex)
            {
                // Log exception (placeholder for future logging implementation)
                ErrorMessageLabel.Text = $"Image upload failed: {ex.Message}";
            }
            return null;
        }
        /// <summary>
        /// Method to insert the candidate to the database
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="imagePath"></param>
        /// <exception cref="ApplicationException"></exception>
        private void AddCandidateToDatabase(string name, string description, string imagePath)
        {
            try
            {
                // Initialize MongoDB client
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var candidatesCollection = database.GetCollection<BsonDocument>("Candidates");

                // Create a BSON document to insert
                var candidateDocument = new BsonDocument
                {
                    { "Name", name },
                    { "Description", description },
                    { "Image", imagePath },
                    { "CreatedDate", DateTime.UtcNow }
                };

                // Insert the document into the collection
                candidatesCollection.InsertOne(candidateDocument);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error inserting candidate into MongoDB.", ex);
            }
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