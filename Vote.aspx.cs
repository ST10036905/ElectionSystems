using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;


 
namespace ElectionSystems
    {
        public partial class Vote : System.Web.UI.Page
        {
            // MongoDB connection string
            private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";

            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    // Call method to load candidates from MongoDB
                    LoadCandidates();
                }
            }

        // Method to fetch candidates from MongoDB and bind to Repeater
        private void LoadCandidates()
        {
            try
            {
                // Connect to MongoDB and get the candidate data
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var collection = database.GetCollection<BsonDocument>("Candidates");

                // Fetch the candidates from MongoDB
                var candidates = collection.Find(new BsonDocument()).ToList();

                // Convert the BsonDocument to a more suitable object for binding
                var candidatesList = candidates.Select(candidate => new
                {
                    Name = candidate["Name"].AsString,
                    Description = candidate["Description"].AsString,
                    // Convert relative path to an absolute URL
                    Image = ResolveUrl(candidate["Image"].AsString)
                }).ToList();

                // Bind the data to the Repeater
                CandidatesRepeater.DataSource = candidatesList;
                CandidatesRepeater.DataBind();
            }
            catch (Exception ex)
            {
                // Handle any errors
                ErrorMessageLabel.Text = $"Error loading candidates: {ex.Message}";
            }
        }


        protected void CandidatesRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
            {
                string userId = Session["userId"] as string;
                string candidateId = ((System.Web.UI.WebControls.Button)e.CommandSource).CommandArgument;

                if (string.IsNullOrEmpty(userId))
                {
                    Response.Write("<script>alert('You need to login to vote.');</script>");
                    return;
                }
                if (HasUserVoted(userId))
                {
                    Response.Write("<script>alert('You have already voted.');</script>");
                    return;
                }
                if (SavedVoted(userId, candidateId))
                {
                    Response.Write("<script>alert('You have successfully voted.');</script>");
                    return;
                }
                else
                {
                    Response.Write("<script>alert('An error occurred while voting.');</script>");
                }
            }

            private bool HasUserVoted(string userId)
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
                catch (Exception ex)
                {
                    // Handle errors appropriately (e.g., log them)
                    Response.Write("<script>alert('An error occurred while checking if user has voted: " + ex.Message + "');</script>");
                    return true;
                }
            }
            /// <summary>
            /// Saves the user vote to MongoDb
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="candidateId"></param>
            /// <returns></returns>
            private bool SavedVoted(string userId, string candidateId)
            {
                try
                {
                    var client = new MongoClient(connectionString);
                    var database = client.GetDatabase("ElectionSystemDB");
                    var votesCollection = database.GetCollection<BsonDocument>("Votes");
                    var vote = new BsonDocument
                    {
                        { "UserId", userId },
                        { "CandidateId", candidateId },
                        { "VoteDate", DateTime.UtcNow }
                    };
                    votesCollection.InsertOne(vote);
                    return true;
                }
                catch (Exception ex)
                {
                    // Handle errors appropriately (e.g., log them)
                    Response.Write("<script>alert('An error occurred while saving vote: " + ex.Message + "');</script>");
                    return false;
                }
            }
        }
    } 
