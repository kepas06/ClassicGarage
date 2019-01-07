namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeParts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "CarID", c => c.Int());
            CreateIndex("dbo.Parts", "CarID");
            AddForeignKey("dbo.Parts", "CarID", "dbo.Car", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parts", "CarID", "dbo.Car");
            DropIndex("dbo.Parts", new[] { "CarID" });
            DropColumn("dbo.Parts", "CarID");
        }
    }
}
