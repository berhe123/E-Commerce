using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
namespace E_Commerce.Controllers
{
    public class ProductTypeController : Controller
    {
        private DBCOntexEntities db = new DBCOntexEntities();
        // GET: /ProductType/   
         [Authorize(Roles = "admin,berhe,newadmin")]
        public ActionResult Index(string search)
        {
            return View(db.ProductTypes.Where(r => r.product_type_name.Contains(search) || search == null).ToList());
        }

        // GET: /ProductType/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType producttype = db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }

        // GET: /ProductType/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: /ProductType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="producttypeId,product_type_name")] ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                 var nexit = from b in db.ProductTypes
                            where b.product_type_name == producttype.product_type_name                       
                            select b;
                 if (nexit.ToList().Count() > 0)
                 {
                     return RedirectToAction("Errorpage");
                 }
                 else
                 {
                     db.ProductTypes.Add(producttype);
                     db.SaveChanges();
                     return RedirectToAction("createsuccesss");
                 }
            }
            return View(producttype);
        }
        // GET: /ProductType/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType producttype = db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }
        // POST: /ProductType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="producttypeId,product_type_name")] ProductType producttype)
        {
            if (ModelState.IsValid)
            {
                db.Entry(producttype).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("success");
            }
            return View(producttype);
        }

        // GET: /ProductType/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductType producttype = db.ProductTypes.Find(id);
            if (producttype == null)
            {
                return HttpNotFound();
            }
            return View(producttype);
        }

        // POST: /ProductType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductType producttype = db.ProductTypes.Find(id);
            db.ProductTypes.Remove(producttype);
            db.SaveChanges();
            return RedirectToAction("deletesuccesss");
        }
        //[Route("Errorpage/Errorpage")]
        public ActionResult Errorpage()
        {
            return View();
        }
        public ActionResult createsuccesss()
        {
            return View();
        }
        public ActionResult success()
        {
            return View();
        }
        public ActionResult deletesuccesss()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
