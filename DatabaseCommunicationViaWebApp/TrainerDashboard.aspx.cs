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
    public partial class TrainerDashboard : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["B22DB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridView();
            }
        }

        protected void BindGridView()
        {
            //if (Cache["Trainers"] == null)
            //{
            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from trainer", con);

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            List<Trainer> trainers = new List<Trainer>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Trainer t = new Trainer()
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                        City = reader["City"].ToString(),
                        Experience = (int)reader["Experience"]
                    };

                    trainers.Add(t);
                }
            }


            // Cache["Trainers"] = trainers;

            GridView1.DataSource = trainers;
            GridView1.DataBind();

            lblMessage.Text = "Trainers Loaded From Database";

            con.Close();
            //}
            //else
            //{
            //    GridView1.DataSource = Cache["Trainers"];
            //    GridView1.DataBind();

            //    lblMessage.Text = "Trainers Loaded From Cache";
            //}
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int trainerId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            GridViewRow row = GridView1.Rows[e.RowIndex];
            string name = ((TextBox)row.Cells[1].Controls[0]).Text;
            string city = ((TextBox)row.Cells[2].Controls[0]).Text;
            int experience = int.Parse(((TextBox)row.Cells[3].Controls[0]).Text);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE Trainer SET Name = @Name, City = @City, Experience = @Experience WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@City", city);
                cmd.Parameters.AddWithValue("@Experience", experience);
                cmd.Parameters.AddWithValue("@Id", trainerId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int trainerId = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM Trainer WHERE Id=@Id", con);
                cmd.Parameters.AddWithValue("@Id", trainerId);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            BindGridView();
        }

        //protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "AddNew")
        //    {
        //        TextBox name = (TextBox)GridView2.FooterRow.FindControl("txtName");
        //        TextBox city = (TextBox)GridView2.FooterRow.FindControl("txtCity");
        //        TextBox experience = (TextBox)GridView2.FooterRow.FindControl("txtExperience");

        //        using (SqlConnection con = new SqlConnection(connectionString))
        //        {
        //            SqlCommand cmd = new SqlCommand("INSERT INTO Trainer (Name, City, Experience) VALUES (@Name, @City, @Experience)", con);
        //            cmd.Parameters.AddWithValue("@FirstName", name.Text);
        //            cmd.Parameters.AddWithValue("@LastName", city.Text);
        //            cmd.Parameters.AddWithValue("@Department", experience.Text);
        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //        BindGridView();
        //    }
        //}
    }
}