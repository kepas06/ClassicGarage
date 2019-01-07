namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeparts2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Repairs", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Repairs", "Email");
        }
    }
}
