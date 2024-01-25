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
    public class SupplyController : Controller
    {
        private DBCOntexEntities db = new DBCOntexEntities();

        // GET: /Supply/
         [Authorize(Roles = "admin,berhe,newadmin")]
        public ActionResult Index(string search)
        {
            return View(db.Supplies.Where(r => r.supplyname.Contains(search) || search == null).ToList());
        }

        // GET: /Supply/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // GET: /Supply/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: /Supply/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="suppyId,supplyname,telephone,adress,Email")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                var exit = from x in db.Supplies
                            where x.Email == supply.Email
                            && x.adress == supply.adress
                            && x.telephone == supply.telephone
                            && x.supplyname == supply.supplyname                          
                            select x;
               if(exit.ToList().Count() > 0 )
               {
                   return RedirectToAction("Errorpage");
                }
                else
                 {
                    db.Supplies.Add(supply);
                    db.SaveChanges();
                    return RedirectToAction("createsuccesss");
                 }
              }
             return View(supply);
        }
        // GET: /Supply/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // POST: /Supply/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="suppyId,supplyname,telephone,adress,Email")] Supply supply)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supply).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("success");
            }
            return View(supply);
        }

        // GET: /Supply/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supply supply = db.Supplies.Find(id);
            if (supply == null)
            {
                return HttpNotFound();
            }
            return View(supply);
        }

        // POST: /Supply/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supply supply = db.Supplies.Find(id);
            db.Supplies.Remove(supply);
            db.SaveChanges();
            return RedirectToAction("deletesuccesss");
        }
       // [Route("Errorpage/Errorpage")]
        public ActionResult Errorpage()
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
        public ActionResult createsuccesss()
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
