using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_Commerce.Models;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.IO;
using System.Data.Entity.Validation;
namespace E_Commerce.Controllers
{
    public class ProductController : Controller
    {
        
        private DBCOntexEntities db = new DBCOntexEntities();
        // GET: /Product/System.Data.Entity.Infrastructure
       [Authorize(Roles = "admin,berhe,newadmin")]
        public ActionResult Index(string search )
        {
            var products = db.Products.Include(p => p.ProductType).Include(p => p.Supply);
            return View(db.Products.Where(r=>r.salemodel.Contains(search) || search== null ).ToList());
        }
       
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // GET: /Product/Create
        public ActionResult Create()
        {
            ViewBag.producttypeId = new SelectList(db.ProductTypes, "producttypeId", "product_type_name");
            ViewBag.suppyId = new SelectList(db.Supplies, "suppyId", "supplyname");
            return View();
        }
        // POST: /Product/Create     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productId,model,suppyId,price,producttypeId,salemodel,photo")] Product product, HttpPostedFileBase file)
        {
            if (file !=null)
            {
                DBCOntexEntities dbs = new DBCOntexEntities();
                Product products = new Product();

                int w = (from p in db.Products
                         select p).OrderByDescending(y => y.productId).Select(y => y.productId).First();

                string FileExtension = System.IO.Path.GetExtension(file.FileName);

                string ImageName = (w++).ToString() + FileExtension;

                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalPath);
                product.photo = ImageName;  
               // if (ImageName!= null)  
               // {
                 // db.Products.Add(product);
                   // db.SaveChanges();
                   //return RedirectToAction("Index"); 
              // }                                                  
              // if (db.Products.Where(u => u.photo == product.photo).ToList().Count() > 0)
               // {
                   // db.Products.Add(product);
                   // db.SaveChanges();
                    //return RedirectToAction("Index");                  
               // }                                        
                var exist=from x in db.Products
                          where x.productId == product.productId
                          
                          && x.model == product.model
                          && x.price == product.price
                          && x.producttypeId == product.producttypeId
                          && x.suppyId == product.suppyId 
                          && x.photo == product.photo
                          && x.salemodel == product.salemodel
                             select x;
                if(exist.ToList().Count() > 0){
                    return RedirectToAction ("Errorpage");
                }
               // else if (db.Products.Where(u => u.photo == product.photo).ToList().Count() < 0)
              // {
                   // return RedirectToAction("Error");
             //  }
                else{                   
                db.Products.Add(product);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
            ViewBag.producttypeId = new SelectList(db.ProductTypes, "producttypeId", "product_type_name", product.producttypeId);
            ViewBag.suppyId = new SelectList(db.Supplies, "suppyId", "supplyname", product.suppyId);
            return View(product);
        }
        // GET: /Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.producttypeId = new SelectList(db.ProductTypes, "producttypeId", "product_type_name", product.producttypeId);
            ViewBag.suppyId = new SelectList(db.Supplies, "suppyId", "supplyname", product.suppyId);
            return View(product);
        }
        // POST: /Product/Edit/5     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="productId,model,suppyId,price,producttypeId,salemodel,photo")] Product product, HttpPostedFileBase file)
        {             
            if (file !=null)
            {
                DBCOntexEntities dbs = new DBCOntexEntities();                                         
                      
                string ImageName = System.IO.Path.GetFileName(file.FileName);
                string physicalPath = Server.MapPath("~/Images/" + ImageName);
                file.SaveAs(physicalPath);             
                product.photo = ImageName;                             
                var exist=from x in db.Products
                          where x.producttypeId == product.producttypeId
                          && x.photo == product.photo                     
                          && x.model == product.model
                          && x.price == product.price
                          && x.producttypeId == product.producttypeId
                          && x.suppyId == product.suppyId                          
                             select x;
                if(exist.ToList().Count() > 0){
                    return RedirectToAction ("Errorpage");
                }
                else{
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
            }
        }           
            ViewBag.producttypeId = new SelectList(db.ProductTypes, "producttypeId", "product_type_name", product.producttypeId);
            ViewBag.suppyId = new SelectList(db.Supplies, "suppyId", "supplyname", product.suppyId);
            return View(product);
        }
        
        // GET: /Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        // POST: /Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                .SelectMany(x => x.ValidationErrors)
                .Select(x => x.ErrorMessage);
                //throw the error messages.
            }           
            return RedirectToAction("Index");
        }
        public ActionResult Errorpage()
        {
            return View();
        }


        public ActionResult showpicture(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
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