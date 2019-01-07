namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Car",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        Year = c.Int(nullable: false),
                        VIN = c.String(),
                        Series = c.Int(nullable: false),
                        Photo = c.String(),
                        Buy_Date = c.DateTime(nullable: false),
                        Sell_Date = c.DateTime(),
                        Buy_Cost = c.Int(nullable: false),
                        Sell_Cost = c.Int(),
                        OwnerID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Owner", t => t.OwnerID)
                .Index(t => t.OwnerID);
            
            CreateTable(
                "dbo.Owner",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Adverts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(nullable: false),
                        Description = c.String(),
                        Active = c.Boolean(nullable: false),
                        Price = c.Int(nullable: false),
                        Photo = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Car", t => t.CarID, cascadeDelete: true)
                .Index(t => t.CarID);
            
            CreateTable(
                "dbo.Parts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Cost_Buy = c.Int(nullable: false),
                        Cost_Sell = c.Int(),
                        Buy_Date = c.DateTime(nullable: false),
                        Sell_Date = c.DateTime(),
                        RepairModel_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Repairs", t => t.RepairModel_ID)
                .Index(t => t.RepairModel_ID);
            
            CreateTable(
                "dbo.Repairs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Car", t => t.CarID)
                .Index(t => t.CarID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Parts", "RepairModel_ID", "dbo.Repairs");
            DropForeignKey("dbo.Repairs", "CarID", "dbo.Car");
            DropForeignKey("dbo.Adverts", "CarID", "dbo.Car");
            DropForeignKey("dbo.Car", "OwnerID", "dbo.Owner");
            DropIndex("dbo.Repairs", new[] { "CarID" });
            DropIndex("dbo.Parts", new[] { "RepairModel_ID" });
            DropIndex("dbo.Adverts", new[] { "CarID" });
            DropIndex("dbo.Car", new[] { "OwnerID" });
            DropTable("dbo.Repairs");
            DropTable("dbo.Parts");
            DropTable("dbo.Adverts");
            DropTable("dbo.Owner");
            DropTable("dbo.Car");
        }
    }
}
