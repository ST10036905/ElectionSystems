using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

// Represents the voting cast for the Election System.
// This page is responsible for displaying candidates and allowing users to vote for them.

namespace ElectionSystems
{
    public partial class Vote : System.Web.UI.Page
    {
        /// <summary>
        /// MongoDB connection string for establishing a connection to the election database.
        /// </summary>
        private static readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";

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
        [System.Web.Services.WebMethod]
        public static string VoteForCandidate(string candidateName)
        {
            string userId = HttpContext.Current.Session["userId"] as string;
            var serializer = new JavaScriptSerializer();

            if (string.IsNullOrEmpty(userId))
            {
                return serializer.Serialize(new { success = false, message = "You need to log in to vote." });
            }

            if (HasUserVoted(userId))
            {
                return serializer.Serialize(new { success = false, message = "You have already voted." });
            }

            if (SaveVote(userId, candidateName))
            {
                return serializer.Serialize(new { success = true, message = "Vote submitted successfully." });
            }

            return serializer.Serialize(new { success = false, message = "An error occurred while submitting your vote." });
        }

        /// <summary>
        /// Checks if the user has already voted based on their user ID.
        /// </summary>
        private static bool HasUserVoted(string userId)
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var votesCollection = database.GetCollection<BsonDocument>("Votes");
                var filter = Builders<BsonDocument>.Filter.Eq("VoterId", userId);
                var vote = votesCollection.Find(filter).FirstOrDefault();

                return vote != null;
            }
            catch (Exception)
            {
                return true;
            }
        }

        /// <summary>
        /// Saves the user's vote to MongoDB. This method is an example of **encapsulation** because it isolates the logic for saving a vote.
        /// </summary>
        /// <param name="userId">The ID of the user who is voting.</param>
        /// <param name="candidateName">The name of the candidate the user is voting for.</param>
        /// <returns>Returns true if the vote was saved successfully, false otherwise.</returns>
        private static bool SaveVote(string userId, string candidateName)
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
                    { "VoterId", userId },
                    { "CandidateName", candidateName },
                    { "VoteDate", DateTime.UtcNow }
                };

                // Insert the vote into the "Votes" collection.
                votesCollection.InsertOne(vote);
                return true;
            }
            catch (Exception ex)
            {
                // If there is an error while saving the vote, return false and show an error message.
                return false;
            }
        }
    }
}
