namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdverts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Adverts", "phone", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Adverts", "phone");
        }
    }
}
