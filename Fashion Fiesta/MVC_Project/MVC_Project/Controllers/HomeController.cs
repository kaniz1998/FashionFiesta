using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        FashionFiestaDbContext db = new FashionFiestaDbContext();
        public ActionResult Index()
        {
            int numberOfBrands = db.Brands.Count();
            ViewBag.NumberOfBrands = numberOfBrands;

            decimal MinimumPrice = db.Stocks.Min(x => x.Price);
            ViewBag.MinimumPrice = MinimumPrice;

            decimal MaximumPrice = db.Stocks.Max(x => x.Price);
            ViewBag.MaximumPrice = MaximumPrice;

            decimal PriceAvg = db.Stocks.Average(x => x.Price);
            ViewBag.PriceAvg = PriceAvg;

            decimal PriceSum = db.Stocks.Sum(x => x.Price);
            ViewBag.PriceSum = PriceSum;
            return View();
        }

       
    }
}