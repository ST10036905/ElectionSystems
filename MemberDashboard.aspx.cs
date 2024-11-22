using System;
using MongoDB.Bson;
using MongoDB.Driver;

// Represents the Member Dashboard (ADMIN) for the Election System.
// This page is responsible for displaying key statistics such as voter count and candidate count.

namespace ElectionSystems
{
    public partial class MemberDashboard : System.Web.UI.Page
    {
        /// <summary>
        /// MongoDB connection string to connect to the database.
        /// </summary>
        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority";

        /// <summary>
        /// Event triggered when the page loads.
        /// Ensures statistics are loaded on the initial page load.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event arguments containing event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensures the statistics are loaded only on the first page load, not on postbacks.
            if (!IsPostBack)
            {
                LoadStatistics();
            }
        }

        /// <summary>
        /// Loads statistics from the database and updates the UI with voter and candidate counts.
        /// </summary>
        private void LoadStatistics()
        {
            try
            {
                // Initialize a MongoDB client to interact with the database.
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");

                // Fetch the voter count from the Voter collection.
                var votersCollection = database.GetCollection<BsonDocument>("Voter");
                var voterCount = votersCollection.CountDocuments(new BsonDocument()); // Counts all documents in the "Voter" collection.
                VoterCountLabel.Text = voterCount.ToString(); // Updates the VoterCountLabel with the count.

                // Fetch the candidate count from the Candidates collection.
                var candidatesCollection = database.GetCollection<BsonDocument>("Candidates");
                var candidateCount = candidatesCollection.CountDocuments(new BsonDocument()); // Counts all documents in the "Candidates" collection.
                CandidatesCountLabel.Text = candidateCount.ToString(); // Updates the CandidatesCountLabel with the count.
            }
            catch (Exception ex)
            {
                // Handle errors during database interaction.
                // Display the error message on the dashboard.
                ErrorMessageLabel.Text = $"An error occurred while loading statistics: {ex.Message}";
            }
        }

        /// <summary>
        /// Method that is called when the View Candidate button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ViewCandidateButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Redirecting to the Vote.aspx page
                Response.Redirect("Vote.aspx");
            }
            catch (Exception ex)
            {
                // If there's an error, show an error message
                ErrorMessageLabel.Text = "An error occurred: " + ex.Message;
            }
        }
    }
}
