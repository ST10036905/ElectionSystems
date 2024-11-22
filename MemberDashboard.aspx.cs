using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ElectionSystems
{
    public partial class MemberDashboard : System.Web.UI.Page
    {

        private readonly string connectionString = "mongodb+srv://st10036905:helloWorld@firstcluster.l38xc.mongodb.net/test?retryWrites=true&w=majority";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStatistics();
            }
        }

        private void LoadStatistics()
        {
            try
            {
                // Initialize MongoDB client
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("ElectionSystemDB");

                // Get the Voters collection and count documents
                var votersCollection = database.GetCollection<BsonDocument>("Voters");
                var voterCount = votersCollection.CountDocuments(new BsonDocument());
                VoterCountLabel.Text = voterCount.ToString();

                // Get the Candidates collection and count documents
                var candidatesCollection = database.GetCollection<BsonDocument>("Candidates");
                var candidateCount = candidatesCollection.CountDocuments(new BsonDocument());
                CandidatesCountLabel.Text = candidateCount.ToString();
            }
            catch (Exception ex)
            {
                // Handle errors and display error message
                ErrorMessageLabel.Text = $"An error occurred while loading statistics: {ex.Message}";
            }
        }

    }
}
