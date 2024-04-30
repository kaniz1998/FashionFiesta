using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class DressCategoriesController : Controller
    {
        private FashionFiestaDbContext db = new FashionFiestaDbContext();


        public ActionResult Index()
        {
            return View(db.DressCategories.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DressCategoryID,CategoryName")] DressCategory dressCategory)
        {
            if (ModelState.IsValid)
            {
                db.DressCategories.Add(dressCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dressCategory);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DressCategory dressCategory = db.DressCategories.Find(id);
            if (dressCategory == null)
            {
                return HttpNotFound();
            }
            return View(dressCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DressCategoryID,CategoryName")] DressCategory dressCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dressCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dressCategory);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DressCategory dressCategory = db.DressCategories.Find(id);
            if (dressCategory == null)
            {
                return HttpNotFound();
            }
            return View(dressCategory);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DressCategory dressCategory = db.DressCategories.Find(id);
            db.DressCategories.Remove(dressCategory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
