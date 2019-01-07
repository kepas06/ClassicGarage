namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changecarpartss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Parts", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Parts", "Email");
        }
    }
}
