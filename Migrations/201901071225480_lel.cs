namespace ClassicGarage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lel : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Parts", name: "RepairModel_ID", newName: "RepairID");
            RenameIndex(table: "dbo.Parts", name: "IX_RepairModel_ID", newName: "IX_RepairID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Parts", name: "IX_RepairID", newName: "IX_RepairModel_ID");
            RenameColumn(table: "dbo.Parts", name: "RepairID", newName: "RepairModel_ID");
        }
    }
}
