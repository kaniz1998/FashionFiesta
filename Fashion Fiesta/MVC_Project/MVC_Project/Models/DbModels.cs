using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVC_Project.Models
{
    public enum Size { XS32 = 32, XS34 = 34, S36 = 36, S38 = 38, L40 = 40, L42 = 42, XL44 = 44, XL46 = 46 }
    public class Brand
    {
        public int BrandID { get; set; }
        [Required, Display(Name = "Brand:")]
        public string BrandName { get; set; }
        public virtual ICollection<Dress> Dresses { get; set; } = new List<Dress>();
    }

    public class DressCategory
    {
        public int DressCategoryID { get; set; }
        [Required, Display(Name = "Category:")]
        public string CategoryName { get; set; }
        public virtual ICollection<Dress> Dresses { get; set; } = new List<Dress>();

    }

    public class Dress
    {
        public int DressID { get; set; }
        [Required, Display(Name = "Name:")]
        public string Name { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Launch Date:"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }
        [Display(Name = "Sale Status:")]
        public bool SaleStatus { get; set; }
        [Display(Name = "Picture:")]
        //Foreign Key
        public string Picture { get; set; }
        [ForeignKey("DressCategory")]
        [Display(Name = "Category:")]
        public int DressCategoryID { get; set; }
        [ForeignKey("Brand")]
        public int BrandID { get; set; }
        //Nevigation
        public virtual Brand Brand { get; set; }
        public virtual DressCategory DressCategory { get; set; }
        public virtual ICollection<Stock> Stocks  { get; set; } = new List<Stock>();

    }
    public class Stock
    {
        public int StockID { get; set; }
        public Size Size { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        //Foreign Key
        [Required, Display(Name = "Dress:")]
        [ForeignKey("Dress")]
        public int DressID { get; set; }
        //Navigation
        public virtual Dress Dress { get; set; }
    }
    public class FashionFiestaDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<DressCategory> DressCategories { get; set; }
        public DbSet<Dress> Dresses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }
}