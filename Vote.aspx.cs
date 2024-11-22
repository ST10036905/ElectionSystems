using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ElectionSystems
{
    public partial class Vote : System.Web.UI.Page
    {
        /// <summary>
        /// Connection string for the database
        /// </summary>
        // private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mayra\\source\\repos\\ElectionSystems\\App_Data\\SystemDatabase.mdf;Integrated Security=True";


        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority&tls=true";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCandidates();
            }
        }

        /// <summary>
        /// Fetch candidates from the database and bind them to the Repeater.
        /// </summary>
        private void LoadCandidates()
        {
            try
            {
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");
                var candidatesCollection = database.GetCollection<BsonDocument>("Candidate");
                // Fetchs all candidates from the database
                var candidates = candidatesCollection.Find(new BsonDocument()).ToList();

                if (candidates.Count > 0)
                {
                    CandidatesRepeater.DataSource = candidates;
                    CandidatesRepeater.DataBind();
                    CandidatesRepeater.Visible = true;
                }
                else
                {
                    CandidatesRepeater.Visible = false;
                }                
            }
            catch (Exception ex)
            {
                // Handle errors appropriately (e.g., log them)
                Response.Write("<script>alert('An error occurred while loading candidates: " + ex.Message + "');</script>");
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
            if  (SavedVoted(userId, candidateId))
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
