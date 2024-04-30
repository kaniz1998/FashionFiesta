namespace MVC_Project.Migrations.FFDB
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScriptProjct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.BrandID);
            
            CreateTable(
                "dbo.Dresses",
                c => new
                    {
                        DressID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        LaunchDate = c.DateTime(nullable: false, storeType: "date"),
                        SaleStatus = c.Boolean(nullable: false),
                        Picture = c.String(),
                        DressCategoryID = c.Int(nullable: false),
                        BrandID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DressID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.DressCategories", t => t.DressCategoryID, cascadeDelete: true)
                .Index(t => t.DressCategoryID)
                .Index(t => t.BrandID);
            
            CreateTable(
                "dbo.DressCategories",
                c => new
                    {
                        DressCategoryID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DressCategoryID);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockID = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        DressID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockID)
                .ForeignKey("dbo.Dresses", t => t.DressID, cascadeDelete: true)
                .Index(t => t.DressID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "DressID", "dbo.Dresses");
            DropForeignKey("dbo.Dresses", "DressCategoryID", "dbo.DressCategories");
            DropForeignKey("dbo.Dresses", "BrandID", "dbo.Brands");
            DropIndex("dbo.Stocks", new[] { "DressID" });
            DropIndex("dbo.Dresses", new[] { "BrandID" });
            DropIndex("dbo.Dresses", new[] { "DressCategoryID" });
            DropTable("dbo.Stocks");
            DropTable("dbo.DressCategories");
            DropTable("dbo.Dresses");
            DropTable("dbo.Brands");
        }
    }
}
