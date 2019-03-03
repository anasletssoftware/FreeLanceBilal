namespace FreeLanceBilal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CompanyNameaddedtoclients : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "CompanyName", c => c.String());
            DropColumn("dbo.Clients", "ClientName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Clients", "ClientName", c => c.String());
            DropColumn("dbo.Clients", "CompanyName");
        }
    }
}
