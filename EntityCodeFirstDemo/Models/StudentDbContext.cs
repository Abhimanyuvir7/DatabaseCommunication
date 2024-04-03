using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EntityCodeFirstDemo.Models
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext() : base("name=B22DBCodeFirst")
        {
            // this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Trainer> Trainers { get; set; }

        public DbSet<Student> Students { get; set; }
    }
}