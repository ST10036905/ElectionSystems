using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace ElectionSystems
{
    public partial class Vote : System.Web.UI.Page
    {
        /// <summary>
        /// Connection string for the database
        /// </summary>
        private string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Mayra\\source\\repos\\ElectionSystems\\App_Data\\SystemDatabase.mdf;Integrated Security=True";

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
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT Name, Description, Image FROM Candidate";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();

                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            DataTable candidatesTable = new DataTable();
                            adapter.Fill(candidatesTable);

                            // Check if there are candidates to display
                            if (candidatesTable.Rows.Count > 0)
                            {
                                CandidatesRepeater.DataSource = candidatesTable;
                                CandidatesRepeater.DataBind();
                                CandidatesRepeater.Visible = true;
                            }
                            else
                            {
                                CandidatesRepeater.Visible = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle errors appropriately (e.g., log them)
                Response.Write("<script>alert('An error occurred while loading candidates: " + ex.Message + "');</script>");
            }
        }
    }
}
