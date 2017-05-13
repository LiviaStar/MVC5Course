using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : BaseController
    {
        //FabricsEntities db = new FabricsEntities(); //全類別共用
        // GET: EF
        public ActionResult Index()
        {         
            var all = db.Product.AsQueryable();

            var data = all.Where(p => p.isDelete == false &&
            p.Active == true &&
            p.ProductName.Contains("Black"));

            return View(data);
        }

        public ActionResult Detail(int id)
        {
            var data = db.Database.SqlQuery<Product>("SELECT * FROM dbo.Product WHERE ProductId=@p0", id)
                .FirstOrDefault();

            return View(data);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if(ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        
        public ActionResult Edit(int id)
        {
            var item = db.Product.Find(id);
            return View(item);
        }

        [HttpPost]
        public ActionResult Edit(int id, Product product)
        {
            if(ModelState.IsValid)
            {
                var item = db.Product.Find(id);
                item.ProductName = product.ProductName;         
                item.Price = product.Price;
                item.Stock = product.Stock;
                item.Active = product.Active;

                db.SaveChanges();

                return RedirectToAction("Index");

            }
            return View(product);
        }

        public ActionResult Remove(int id)
        {
            //刪除會有錯誤(有關聯資料)
            //Product item = db.Product.Find(id);
            ////if (ModelState.IsValid)
            ////{
            //    db.Product.Remove(item);
            //    db.SaveChanges();

            //    return RedirectToAction("Index");
            ////}

            ////return View();

            //Product product = db.Product.Find(id);

            //foreach (var item in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(item);
            //}

            ////db.OrderLine.RemoveRange(product.OrderLine); //同樣寫法

            //db.Product.Remove(product);
            //db.SaveChanges(); //不要放到 foreach 裡, 其中一交易失敗則會所有rollback

            Product product = db.Product.Find(id);
            product.isDelete = true;

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var errMsg = ex.EntityValidationErrors
                    .First().ValidationErrors.First().ErrorMessage;

                throw ex;
            }
            

            return RedirectToAction("Index");
        }


    }
}