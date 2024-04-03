namespace EntityCodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newstudent : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.B22DBCodeFirst", newName: "Student");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Student", newName: "B22DBCodeFirst");
        }
    }
}
