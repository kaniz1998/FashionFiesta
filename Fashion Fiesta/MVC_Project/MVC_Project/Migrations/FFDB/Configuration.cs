namespace MVC_Project.Migrations.FFDB
{
    using MVC_Project.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MVC_Project.Models.FashionFiestaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\FFDB";
        }

        protected override void Seed(MVC_Project.Models.FashionFiestaDbContext db)
        {
            db.Brands.AddRange(new Brand[]
            {
                new Brand{BrandName="Aarong"},
                new Brand{BrandName="Key-Kraft"},
                new Brand{BrandName="Sailor"},
                new Brand{BrandName="Yellow"},
                new Brand{BrandName="Le Reve"}
            });
            db.DressCategories.AddRange(new DressCategory[]
            {
                new DressCategory{CategoryName="Shalwar-Kamiz"},
                new DressCategory{CategoryName="Kurta"},
                new DressCategory{CategoryName="Tops"},
                new DressCategory{CategoryName="Tunics"},
                new DressCategory{CategoryName="Co-Ords"},
                new DressCategory{CategoryName="Shirts"},
                new DressCategory{CategoryName="Fatua"}
            });
            db.SaveChanges();
            Dress d = new Dress
            {
                Name = "Pink Kamiz Set",
                BrandID = 1,
                DressCategoryID = 1,
                LaunchDate = new DateTime(2023, 11, 11),
                SaleStatus = true,
                Picture = "1.png"
            };
            d.Stocks.Add(new Stock { Size = Size.S38, Quantity = 10, Price = 2500 });
            d.Stocks.Add(new Stock { Size = Size.L40, Quantity = 10, Price = 2550 });
            d.Stocks.Add(new Stock { Size = Size.XL44, Quantity = 10, Price = 3000 });
            db.Dresses.Add(d);
            db.SaveChanges();
        }
    }
}
