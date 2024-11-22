using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;

// Represents the voting cast for the Election System.
namespace ElectionSystems
{
    public partial class Vote : System.Web.UI.Page
    {
        /// <summary>
        /// MongoDB connection string for establishing a connection to the election database.
        /// </summary>
        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";

        /// <summary>
        /// The Page_Load method demonstrates the concept of **encapsulation**.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Ensures that candidate data is loaded only during the first request (not a postback).
            if (!IsPostBack)
            {
                // Calls the LoadCandidates method to retrieve candidate information from the MongoDB database.
                LoadCandidates();
            }
        }

        /// <summary>
        /// Fetches the candidates from MongoDB and binds them to the Repeater control for display.
        /// Demonstrates **encapsulation** by isolating the data fetching and binding logic in a single method.
        /// </summary>
        private void LoadCandidates()
        {
            try
            {
                // MongoDB client initialization demonstrates **encapsulation** by using an encapsulated connection string.
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var collection = database.GetCollection<BsonDocument>("Candidates");

                // Fetches candidate documents from MongoDB.
                var candidates = collection.Find(new BsonDocument()).ToList();

                // Converts each BsonDocument to an anonymous object for easier binding to the UI.
                var candidatesList = candidates.Select(candidate => new
                {
                    // Encapsulates the candidate data into a more suitable object for display.
                    Name = candidate["Name"].AsString,
                    Description = candidate["Description"].AsString,
                    // Resolves relative path for the candidate image.
                    Image = ResolveUrl(candidate["Image"].AsString)
                }).ToList();

                // Binds the transformed candidate data to the Repeater control for dynamic UI rendering.
                CandidatesRepeater.DataSource = candidatesList;
                CandidatesRepeater.DataBind();
            }
            catch (Exception ex)
            {
                // If an error occurs while loading candidates, displays an error message on the UI.
                ErrorMessageLabel.Text = $"Error loading candidates: {ex.Message}";
            }
        }

        /// <summary>
        /// Event handler for handling user interaction when they click the "Vote" button for a candidate.
        /// Demonstrates **polymorphism** by handling events in a flexible manner based on the command argument (candidateId).
        /// </summary>
        protected void CandidatesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            // Retrieves the user ID from session state. This is a form of **encapsulation** where session data is managed.
            string userId = Session["userId"] as string;

            // Retrieves the candidate ID from the button's CommandArgument (demonstrating **polymorphism** as different button clicks will trigger different candidate IDs).
            string candidateId = ((System.Web.UI.WebControls.Button)e.CommandSource).CommandArgument;

            // Checks if the user is logged in. If not, displays an alert and prevents further actions.
            if (string.IsNullOrEmpty(userId))
            {
                Response.Write("<script>alert('You need to login to vote.');</script>");
                return;
            }

            // Checks if the user has already voted using encapsulated business logic in the HasUserVoted method.
            if (HasUserVoted(userId))
            {
                Response.Write("<script>alert('You have already voted.');</script>");
                return;
            }

            // Attempts to save the user's vote using the encapsulated logic in SavedVoted method.
            if (SavedVoted(userId, candidateId))
            {
                Response.Write("<script>alert('You have successfully voted.');</script>");
                return;
            }
            else
            {
                // If saving the vote fails, an error message is displayed.
                Response.Write("<script>alert('An error occurred while voting.');</script>");
            }
        }

        /// <summary>
        /// Checks whether the user has already voted. This method uses **encapsulation** to hide the implementation details of checking votes.
        /// </summary>
        private bool HasUserVoted(string userId)
        {
            try
            {
                // MongoDB query to check if the user has voted by querying the "Votes" collection.
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var votesCollection = database.GetCollection<BsonDocument>("Votes");
                var filter = Builders<BsonDocument>.Filter.Eq("VoterId", userId);
                var vote = votesCollection.Find(filter).FirstOrDefault();

                // Returns true if a vote is found for the user.
                return vote != null;
            }
            catch (Exception ex)
            {
                // If there's an error in checking, return true and display an error message.
                Response.Write("<script>alert('An error occurred while checking if user has voted: " + ex.Message + "');</script>");
                return true;
            }
        }

        /// <summary>
        /// Saves the user's vote to MongoDB. This method is an example of **encapsulation** because it isolates the logic for saving a vote.
        /// </summary>
        /// <param name="userId">The ID of the user who is voting.</param>
        /// <param name="candidateId">The ID of the candidate the user is voting for.</param>
        /// <returns>Returns true if the vote was saved successfully, false otherwise.</returns>
        private bool SavedVoted(string userId, string candidateId)
        {
            try
            {
                // Encapsulated logic for saving the vote to the MongoDB database.
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var votesCollection = database.GetCollection<BsonDocument>("Votes");

                // Create a new document to represent the user's vote.
                var vote = new BsonDocument
                {
                    { "UserId", userId },
                    { "CandidateId", candidateId },
                    { "VoteDate", DateTime.UtcNow }
                };

                // Insert the vote into the "Votes" collection.
                votesCollection.InsertOne(vote);
                return true;
            }
            catch (Exception ex)
            {
                // If there is an error while saving the vote, return false and show an error message.
                Response.Write("<script>alert('An error occurred while saving vote: " + ex.Message + "');</script>");
                return false;
            }
        }
    }
}
