namespace FreeLanceBilal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.CityId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientTypeId = c.Int(nullable: false),
                        ClientTypeName = c.String(),
                        ClientName = c.String(),
                        Proprietor = c.String(),
                        Representative = c.String(),
                        Address = c.String(),
                        CityId = c.Int(nullable: false),
                        CityName = c.String(),
                        StateId = c.Int(nullable: false),
                        StateName = c.String(),
                        ReturntypeId = c.Int(nullable: false),
                        ReturnTypeName = c.String(),
                        Services = c.Boolean(nullable: false),
                        Importer = c.Boolean(nullable: false),
                        Exporter = c.Boolean(nullable: false),
                        WholeSeller = c.Boolean(nullable: false),
                        Retailer = c.Boolean(nullable: false),
                        CommercialImporter = c.Boolean(nullable: false),
                        RegistrationDate = c.DateTime(nullable: false),
                        SalesTaxNumber = c.Int(nullable: false),
                        CNICNumber = c.String(),
                        NTNNumber = c.String(),
                        EmailAddress = c.String(),
                        MobileNumber1 = c.String(),
                        MobileNumber2 = c.String(),
                        OfficeNumber1 = c.String(),
                        OfficeNumber2 = c.String(),
                        UserId = c.String(),
                        Password = c.String(),
                        PIN = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.ClientTypes",
                c => new
                    {
                        ClienttypeId = c.Int(nullable: false, identity: true),
                        ClientTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ClienttypeId);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        DocumentId = c.Int(nullable: false, identity: true),
                        DocumentName = c.String(),
                        Document = c.String(),
                    })
                .PrimaryKey(t => t.DocumentId);
            
            CreateTable(
                "dbo.ReturnTypes",
                c => new
                    {
                        ReturnTypeId = c.Int(nullable: false, identity: true),
                        ReturnTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ReturnTypeId);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        StateId = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.StateId);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserAccounts");
            DropTable("dbo.States");
            DropTable("dbo.ReturnTypes");
            DropTable("dbo.Documents");
            DropTable("dbo.ClientTypes");
            DropTable("dbo.Clients");
            DropTable("dbo.Cities");
        }
    }
}
