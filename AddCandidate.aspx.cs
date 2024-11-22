using System;                  
using System.Data.SqlClient;   
using System.IO;               
using System.Linq;             
using System.Web;              
using System.Web.UI;          
using System.Web.UI.WebControls; 
using MongoDB.Bson;            
using MongoDB.Driver;

// Code-behind for the AddCandidate web page.
// Handles candidate registration and image upload.
// Demonstrates encapsulation, inheritance, and polymorphism principles.

namespace ElectionSystems
{
  
    public partial class AddCandidate : System.Web.UI.Page 
    {
        /// <summary>
        /// Encapsulated user data object.
        /// </summary>
        private readonly User userData = new User(); // Encapsulation: Restricting direct access to the user data.

        /// <summary>
        /// Encapsulated MongoDB connection string for secure database access.
        /// </summary>
        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";

        /// <summary>
        /// Handles the Page_Load event.
        /// This method is currently a placeholder for any initialization logic during page load.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e) { }

        /// <summary>
        /// Handles the Submit button click event.
        /// Validates user input, saves the uploaded image, and adds candidate data to the database.
        /// </summary>
        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            try
            {
                #region Input Validation

                // Validate if required fields are filled
                if (string.IsNullOrWhiteSpace(CandidateName.Text) ||
                    string.IsNullOrWhiteSpace(Description.Value))
                {
                    ErrorMessageLabel.Text = "Please fill in all required fields.";
                    return;
                }

                #endregion

                #region Image Upload

                // Save the uploaded image to the server
                string imagePath = SaveImage();

                // Check if image upload was successful
                if (string.IsNullOrEmpty(imagePath))
                {
                    ErrorMessageLabel.Text = "Failed to upload the image.";
                    return;
                }

                #endregion

                #region Database Interaction

                // Add candidate information to the database
                AddCandidateToDatabase(CandidateName.Text, Description.Value, imagePath);

                // Success message and redirect
                SucessMessageLabel.Text = "Candidate added successfully! Redirecting...";
                string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 3000);";
                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);

                #endregion
            }
            catch (Exception ex)
            {
                // Log the error message (placeholder for logging functionality)
                ErrorMessageLabel.Text = $"An error occurred: {ex.Message}";
            }
        }

        /// <summary>
        /// Saves the uploaded image to the server.
        /// Encapsulation: This private method handles image storage logic separately.
        /// </summary>
        /// <returns>The relative file path of the saved image or null if upload failed.</returns>
        private string SaveImage()
        {
            try
            {
                if (CandidateImage.PostedFile != null && CandidateImage.PostedFile.ContentLength > 0)
                {
                    string folderPath = Server.MapPath("~/UploadedImages/");

                    // Ensure the upload directory exists
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    // Generate a unique file name for the uploaded image
                    string fileName = Path.GetFileNameWithoutExtension(CandidateImage.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(CandidateImage.PostedFile.FileName);
                    string uniqueFileName = $"{fileName}_{Guid.NewGuid()}{fileExtension}";
                    string fullPath = Path.Combine(folderPath, uniqueFileName);

                    // Save the uploaded image file to the server
                    CandidateImage.PostedFile.SaveAs(fullPath);

                    // Return the relative file path for database storage
                    return $"~/UploadedImages/{uniqueFileName}";
                }
            }
            catch (Exception ex)
            {
                // Log upload failure (placeholder for logging functionality)
                ErrorMessageLabel.Text = $"Image upload failed: {ex.Message}";
            }

            return null; // Return null if upload fails
        }

        /// <summary>
        /// Inserts the candidate's data into the MongoDB database.
        /// Demonstrates encapsulation by handling all database logic within a private method.
        /// </summary>
        /// <param name="name">Candidate's name.</param>
        /// <param name="description">Candidate's description.</param>
        /// <param name="imagePath">Path to the candidate's uploaded image.</param>
        /// <exception cref="ApplicationException">Thrown if database insertion fails.</exception>
        private void AddCandidateToDatabase(string name, string description, string imagePath)
        {
            try
            {
                // Initialize MongoDB client and access database and collection
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var candidatesCollection = database.GetCollection<BsonDocument>("Candidates");

                // Create a BSON document for the candidate's data
                var candidateDocument = new BsonDocument
                {
                    { "Name", name },
                    { "Description", description },
                    { "Image", imagePath },
                    { "CreatedDate", DateTime.UtcNow } // Store UTC timestamp for record creation
                };

                // Insert the document into the MongoDB collection
                candidatesCollection.InsertOne(candidateDocument);
            }
            catch (Exception ex)
            {
                // Wrap and rethrow exceptions for better error context
                throw new ApplicationException("Error inserting candidate into MongoDB.", ex);
            }
        }

        /// <summary>
        /// Handles the Cancel button click event.
        /// Clears input fields and redirects the user to the dashboard.
        /// </summary>
        protected void CancelBtn_Click(object sender, EventArgs e)
        {
         
            //calling method to clear fields
            ClearFields();

            // Notify the user of the cancellation
            ErrorMessageLabel.Text = "Operation being canceled...";

            // Redirect to the dashboard after 2 seconds
            string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 2000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        /// <summary>
        /// Clears all input fields on the page.
        /// </summary>
        private void ClearFields()
        {
            // Reset input fields
            CandidateName.Text = "";
            Description.Value = "";
        }
    }
}
