using ElectionSystems.Classes;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Web;
using System.Web.UI;

namespace ElectionSystems
{
    public partial class AddVoter : System.Web.UI.Page
    {
        /// <summary>
        /// Declaring an instance of the User class (used for voter data).
        /// </summary>
        Voter voterData = new Voter();

        /// <summary>
        /// MongoDB connection setup.
        /// </summary>
        private readonly string connectionString = "mongodb://localhost:27017"; // Connection string to MongoDB
        private readonly string databaseName = "ElectionSystemDB"; // Database name
        private readonly string votersCollectionName = "Voter"; // Collection name for voters
        private readonly string usersCollectionName = "User"; // Collection name for users

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

            // Setting voter data
            voterData.Name = NameTxtBox.Text;
            voterData.Email = EmailTxtBox.Text;
            voterData.Address = AddressTxtBox.Text;

            // Hashing password before saving
            voterData.SetPassword(PasswordTxtBox.Text);

            // Verifying password match
            if (!voterData.Password.Equals(PasswordTxtBox.Text) || !PasswordTxtBox.Text.Equals(ReEnterPasswordTxtBox.Text))
            {
                ErrorMessageLabel.Text = "Password does not match.";
                return;
            }

            // Save voter data with hashed password
            SaveVoterData(voterData.Name, voterData.Email, voterData.Address, voterData.Password, voterData.Role);

            SucessMessageLabel.Text = "Voter registration successful.";
            string script = "setTimeout(function(){ window.location = 'MemberDashboard.aspx'; }, 3000);";
            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", script, true);
        }

        public void SaveVoterData(string name, string email, string address, string password, string role)
        {
            try
            {
                // Connect to MongoDB
                var client = new MongoClient(connectionString);
                var database = client.GetDatabase(databaseName);

                // Get collections
                var votersCollection = database.GetCollection<BsonDocument>(votersCollectionName);
                var usersCollection = database.GetCollection<BsonDocument>(usersCollectionName);

                // Create voter document
                var voterDocument = new BsonDocument
                {
                    { "Name", name },
                    { "Email", email },
                    { "Address", address },
                    { "Password", password }, // Save the hashed password
                    { "Role", role }
                };

                // Insert voter document into the 'Voters' collection
                votersCollection.InsertOne(voterDocument);

                // Create user document (for user authentication)
                var userDocument = new BsonDocument
                {
                    { "Email", email },
                    { "Password", password }, // Save the hashed password
                    { "Role", role }
                };

                // Insert user document into the 'Users' collection
                usersCollection.InsertOne(userDocument);
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
