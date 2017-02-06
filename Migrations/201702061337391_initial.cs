namespace Practice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, unicode: false, storeType: "text"),
                        Time = c.Int(nullable: false),
                        Incentive = c.Int(nullable: false),
                        Notes = c.String(unicode: false, storeType: "text"),
                    })
                .PrimaryKey(t => t.ProjectId);
            
            CreateTable(
                "dbo.Responses",
                c => new
                    {
                        ResponseId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        Checked = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        ProjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ResponseId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Responses", "UserId", "dbo.Users");
            DropForeignKey("dbo.Responses", "ProjectId", "dbo.Projects");
            DropIndex("dbo.Responses", new[] { "ProjectId" });
            DropIndex("dbo.Responses", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Responses");
            DropTable("dbo.Projects");
        }
    }
}
