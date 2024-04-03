using EntityCodeFirstDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EntityCodeFirstDemo
{
    public partial class TrainerDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            StudentDbContext db = new StudentDbContext();
            gvTrainers.DataSource = db.Trainers.ToList();
            gvTrainers.DataBind();
        }
    }
}