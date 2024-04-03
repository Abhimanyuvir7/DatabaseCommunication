using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DatabaseCommunicationViaWebApp
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["B22DB"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from student12", con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            gvStudents.DataSource = reader;
            gvStudents.DataBind();

            con.Close();
        }
    }
}