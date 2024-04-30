using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Project.Models.View_Models
{
    public class DressEditModel
    {
        public int DressID { get; set; }
        [Required, Display(Name = "Name:")]
        public string Name { get; set; }
        [Required, Column(TypeName = "date"), Display(Name = "Launch Date:"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LaunchDate { get; set; }
        [Display(Name = "Sale Status:")]
        public bool SaleStatus { get; set; }
        [Display(Name = "Picture:")]
        public HttpPostedFileBase Picture { get; set; }
        [Display(Name = "Category:")]
        public int DressCategoryID { get; set; }
        [Display(Name = "Brand:")]
        public int BrandID { get; set; }
        public virtual List<Stock> Stocks { get; set; } = new List<Stock>();
    }
}