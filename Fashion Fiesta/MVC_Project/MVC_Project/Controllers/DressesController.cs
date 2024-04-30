using MVC_Project.Models;
using MVC_Project.Models.View_Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    [Authorize]
    public class DressesController : Controller
    {
        
        FashionFiestaDbContext db = new FashionFiestaDbContext();
        // GET: Dresses
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult DressInfo(int page = 1)
        {
            var data = db.Dresses.Include(x => x.Stocks)
                                 .Include(x => x.DressCategory)
                                 .Include(x => x.Brand)
                                 .OrderBy(x => x.DressID)
                                 .ToPagedList(page, 5);
            return PartialView("_DressInfo", data);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateForm()
        {
            DressInputModel inputModel = new DressInputModel();
            inputModel.Stocks.Add(new Stock());
            ViewBag.DressCategory = db.DressCategories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return PartialView("_CreateForm",inputModel);
        }
        [HttpPost]
        public ActionResult Create(DressInputModel inputModel, string act = "")
        {
            if (act == "add")
            {
                inputModel.Stocks.Add(new Stock());
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                inputModel.Stocks.RemoveAt(index);
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var dress = new Dress
                    {
                        Name = inputModel.Name,
                        LaunchDate = inputModel.LaunchDate,
                        DressCategoryID = inputModel.DressCategoryID,
                        BrandID = inputModel.BrandID,
                        SaleStatus = inputModel.SaleStatus
                    };
                    //For Image Save
                    string ext = Path.GetExtension(inputModel.Picture.FileName);
                    string file = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Images"), file);
                    inputModel.Picture.SaveAs(savePath);
                    dress.Picture = file;

                    db.Dresses.Add(dress);
                    db.SaveChanges();
                    //Stocks
                    foreach (var s in inputModel.Stocks)
                    {
                        db.Database.ExecuteSqlCommand($"spInsertStock {(int)s.Size},{s.Price},{(int)s.Quantity},{dress.DressID}");
                    }
                    DressInputModel newModel = new DressInputModel()
                    {
                        Name = "",
                        LaunchDate = DateTime.Today
                    };
                    newModel.Stocks.Add(new Stock());

                    ViewBag.DressCategory = db.DressCategories.ToList();
                    ViewBag.Brands = db.Brands.ToList();
                    foreach (var e in ModelState.Values)
                    {
                        e.Value = null;
                    }
                    return View("_CreateForm", newModel);
                }
            }
            ViewBag.DressCategory = db.DressCategories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            return View("_CreateForm", inputModel);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        public ActionResult EditForm(int id)
        {
            var data = db.Dresses.FirstOrDefault(x => x.DressID == id);
            if (data == null) return new HttpNotFoundResult();
            db.Entry(data).Collection(x => x.Stocks).Load();
            DressEditModel model = new DressEditModel
            {
                DressID = id,
                BrandID = data.BrandID,
                DressCategoryID = data.DressCategoryID,
                Name = data.Name,
                LaunchDate = data.LaunchDate,
                SaleStatus = data.SaleStatus,
                Stocks = data.Stocks.ToList()
            };
            ViewBag.DressCategory = db.DressCategories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.CurrentPic = data.Picture;
            return PartialView("_EditForm", model);
        }

        [HttpPost]
        public ActionResult Edit(DressEditModel editModel, string act = "")
        {
            if (act == "add")
            {
                editModel.Stocks.Add(new Stock());
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                editModel.Stocks.RemoveAt(index);
                foreach (var e in ModelState.Values)
                {
                    e.Errors.Clear();
                    e.Value = null;
                }
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var dress = db.Dresses.FirstOrDefault(x => x.DressID == editModel.DressID);
                    if (dress == null) { return new HttpNotFoundResult(); }
                    dress.Name = editModel.Name;
                    dress.LaunchDate = editModel.LaunchDate;
                    dress.SaleStatus = editModel.SaleStatus;
                    dress.BrandID = editModel.BrandID;
                    dress.DressCategoryID = editModel.DressCategoryID;
                    if (editModel.Picture != null)
                    {
                        string ext = Path.GetExtension(editModel.Picture.FileName);
                        string file = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Path.Combine(Server.MapPath("~/Images"), file);
                        editModel.Picture.SaveAs(savePath);
                        dress.Picture = file;
                    }
                    else
                    {

                    }
                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand($"EXEC spDeleteStock {dress.DressID}");
                    foreach (var d in editModel.Stocks)
                    {
                        db.Database.ExecuteSqlCommand($"EXEC spInsertStock {(int)d.Size}, {d.Price}, {d.Quantity}, {dress.DressID}");
                    }
                }
            }
            ViewBag.DressCategory = db.DressCategories.ToList();
            ViewBag.Brands = db.Brands.ToList();
            ViewBag.CurrentPic = db.Dresses.FirstOrDefault(x => x.DressID == editModel.DressID)?.Picture;
            return View("_EditForm", editModel);
        }

        public ActionResult Delete(int? id)
        {
            var dress = db.Dresses.Find(id);
            if(dress != null)
            {
                var deleteStock = db.Stocks.Where(x => x.DressID == id).ToList();
                db.Stocks.RemoveRange(deleteStock);
                db.Dresses.Remove(dress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}