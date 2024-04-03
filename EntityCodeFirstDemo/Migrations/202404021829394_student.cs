namespace EntityCodeFirstDemo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class student : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.B22DBCodeFirst",
                c => new
                    {
                        RollNumber = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 50, unicode: false),
                        City = c.String(maxLength: 100),
                        Email = c.String(nullable: false),
                        TrainerId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RollNumber)
                .ForeignKey("dbo.Trainers", t => t.TrainerId, cascadeDelete: true)
                .Index(t => t.TrainerId);
            
            CreateTable(
                "dbo.Trainers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Experience = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.B22DBCodeFirst", "TrainerId", "dbo.Trainers");
            DropIndex("dbo.B22DBCodeFirst", new[] { "TrainerId" });
            DropTable("dbo.Trainers");
            DropTable("dbo.B22DBCodeFirst");
        }
    }
}
