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
    public partial class CreateStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTrainers();
            }
        }

        private void LoadTrainers()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["B22DB"].ConnectionString;

            SqlConnection con = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from trainer", con);

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            ddlTrainers.DataValueField = "Id";
            ddlTrainers.DataTextField = "Name";

            ddlTrainers.DataSource = reader;
            ddlTrainers.DataBind();

            ddlTrainers.Items.Insert(0, new ListItem() { Value = "-1", Text = "-- Select Trainer --" });

            con.Close();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string dbImagePath = "";
            if (ImageFile.HasFile &&
                (ImageFile.PostedFile.FileName.Split('.')[1] == "jpg"
                || ImageFile.PostedFile.FileName.Split('.')[1] == "png")    )
            {
                string fileName = ImageFile.FileName;

                // upload
                // 1. save file on server

                string folderPath = Server.MapPath("~/Images");

                string imagePath = $"{folderPath}/{fileName}";
                ImageFile.SaveAs(imagePath);

                dbImagePath = $"~/Images/{fileName}";
                try
                {
                    string name = txtName.Text;
                    // string gender = txtGender.Text;
                    var items = rblGender.Items.Cast<ListItem>();
                    string gender = items.FirstOrDefault(i => i.Selected == true)?.Value;
                    // string trainerId = txtTrainer.Text;
                    int trainerId = int.Parse(ddlTrainers.SelectedValue);

                    // ado.net code to insert student in database

                    string connectionString =
                        ConfigurationManager.ConnectionStrings["B22DB"].ConnectionString;

                    SqlConnection con = new SqlConnection(connectionString);

                    string cmdText = $"insert into student12 values ('{name}', '{gender}', {trainerId}, '{dbImagePath}')";
                    SqlCommand cmd = new SqlCommand(cmdText, con);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        Response.Redirect("StudentDashboard.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Error in creating student";
                    }
                }
                catch
                {
                    lblMessage.Text = "Error in creating student";
                }
            }
            else
            {
                lblMessage.Text = "Incorrect file selected";
            }
        }
    }
}