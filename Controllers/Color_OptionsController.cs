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
    public class Color_OptionsController : Controller
    {
        private DBCOntexEntities db = new DBCOntexEntities();
        // GET: /Color_Options/
         [Authorize(Roles = "admin,berhe,newadmin")]
        public ActionResult Index(string search)
        {
            var color_options = db.Color_Options.Include(c => c.Product);
            return View(db.Color_Options.Where(r => r.color_Type.Contains(search) || search == null).ToList());
        }
        // GET: /Color_Options/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color_Options color_options = db.Color_Options.Find(id);
            if (color_options == null)
            {
                return HttpNotFound();
            }
            return View(color_options);
        }

        // GET: /Color_Options/Create
        public ActionResult Create()
        {
            ViewBag.productId = new SelectList(db.Products, "productId", "model");
            return View();
        }
        // POST: /Color_Options/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="color_OptionsId,color_Type,productId")] Color_Options color_options)
        {           
            if (ModelState.IsValid)
            {
                var exist = from x in db.Color_Options
                            where x.productId == color_options.productId
                            && x.color_Type == color_options.color_Type
                            select x;
                if(exist.ToList().Count() > 0)
                {
                    return RedirectToAction("Errorpage");
                    
                }
                else { 

                db.Color_Options.Add(color_options);
                db.SaveChanges();
                return RedirectToAction("createsuccesss");
                }
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "model", color_options.productId);
            return View(color_options);
        }
      //  public ActionResult Create(string color_Type)
        //{
         //   Color_Options db = new Color_Options();
           // var exist = db.color_Type().FirstOrDefault(m => m.color_Type == color_Type);
          //  if (exist != null)
           // {
           //     return Json(false, JsonRequestBehavior.AllowGet);
          //  }
         //   else
          //  {
          //  return Json(true, JsonRequestBehavior.AllowGet);
          //  }
      //  }
        












        // GET: /Color_Options/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color_Options color_options = db.Color_Options.Find(id);
            if (color_options == null)
            {
                return HttpNotFound();
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "model", color_options.productId);
            return View(color_options);
        }

        // POST: /Color_Options/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="color_OptionsId,color_Type,productId")] Color_Options color_options)
        {
            if (ModelState.IsValid)
            {
                db.Entry(color_options).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("success");
            }
            ViewBag.productId = new SelectList(db.Products, "productId", "model", color_options.productId);
            return View(color_options);
        }

        // GET: /Color_Options/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Color_Options color_options = db.Color_Options.Find(id);
            if (color_options == null)
            {
                return HttpNotFound();
            }
            return View(color_options);
        }

        // POST: /Color_Options/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Color_Options color_options = db.Color_Options.Find(id);
            db.Color_Options.Remove(color_options);
            db.SaveChanges();
            return RedirectToAction("deletesuccesss");
        }

     //   [Route("Errorpage/Errorpage")]
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
