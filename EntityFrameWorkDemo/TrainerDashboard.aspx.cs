using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityFrameWorkDemo
{
    public partial class TrainerDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTrainers();
            }
        }

        void LoadTrainers()
        {
            StudentModel db = new StudentModel();
            gvTrainers.DataSource = db.Trainers.ToList();
            // gvTrainers.DataSource = db.GetTrainers().ToList();
            gvTrainers.DataBind();
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string city = txtCity.Text;
            int experience = int.Parse(txtExperience.Text);

            Trainer trainer = new Trainer()
            {
                name = name,
                city = city,
                Experience = experience
            };

            StudentModel db = new StudentModel();
            db.Trainers.Add(trainer);
            db.SaveChanges();

            LoadTrainers();

            ClearForm();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        void ClearForm()
        {
            txtName.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtExperience.Text = string.Empty;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);

            StudentModel db = new StudentModel();
            Trainer trainer = db.Trainers.FirstOrDefault(t => t.id == id);

            if (trainer != null)
            {
                txtName.Text = trainer.name;
                txtCity.Text = trainer.city;
                txtExperience.Text = trainer.Experience.ToString();

                lblMessage.Text = $"Trainer with id {id} loaded";
            }
            else
            {
                lblMessage.Text = $"Trainer not found for id {id}";
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);
            string name = txtName.Text;
            string city = txtCity.Text;
            int experience = int.Parse(txtExperience.Text);

            StudentModel db = new StudentModel();
            Trainer dbTrainer = db.Trainers.FirstOrDefault(t => t.id == id);

            dbTrainer.name = name;
            dbTrainer.city = city;
            dbTrainer.Experience = experience;

            db.SaveChanges();

            lblMessage.Text = "Trainer data updated";

            LoadTrainers();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtId.Text);

            StudentModel db = new StudentModel();
            Trainer trainer = db.Trainers.FirstOrDefault(t => t.id == id);

            db.Trainers.Remove(trainer);
            db.SaveChanges();

            LoadTrainers();
        }
    }
}