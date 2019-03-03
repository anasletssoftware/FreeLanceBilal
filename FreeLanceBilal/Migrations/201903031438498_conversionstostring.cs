namespace FreeLanceBilal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class conversionstostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clients", "SalesTaxNumber", c => c.String());
            AlterColumn("dbo.Clients", "PIN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clients", "PIN", c => c.Int(nullable: false));
            AlterColumn("dbo.Clients", "SalesTaxNumber", c => c.Int(nullable: false));
        }
    }
}
