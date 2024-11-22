using System;
using System.Data.SqlClient;

namespace ElectionSystems
{
    public partial class MemberDashboard : System.Web.UI.Page
    {

        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mayra\\source\\repos\\ElectionSystems\\App_Data\\SystemDatabase.mdf;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStatistics();
            }
        }

        private void LoadStatistics()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                // Count voters
                string voterQuery = "SELECT COUNT(*) FROM Voter";
                using (SqlCommand voterCommand = new SqlCommand(voterQuery, conn))
                {
                    VoterCountLabel.Text = voterCommand.ExecuteScalar().ToString();
                }

                // Count candidates
                string candidateQuery = "SELECT COUNT(*) FROM Candidate";
                using (SqlCommand candidateCommand = new SqlCommand(candidateQuery, conn))
                {
                    CandidatesCountLabel.Text = candidateCommand.ExecuteScalar().ToString();
                }
            }
        }

    }
}
