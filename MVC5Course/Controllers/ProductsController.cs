﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        //private FabricsEntities db = new FabricsEntities();

        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            //return View(db.Product.ToList());
            //return View(db.Product.Take(10));
            //db.Product.OrderByDescending(p => p.ProductId).Take(10)

            //var data = repo.All() //db.Product
            //    .Where(p => p.Active.HasValue && p.Active.Value == Active)
            //    .OrderByDescending(p => p.ProductId).Take(10);

            var data = repo.GetProduct列表頁資料(Active);

            return View(data);
        }

        public ActionResult unknown()
        {
            return View(Content("Hello"));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            //Product product = repo.All().FirstOrDefault(p => p.ProductId == id.Value);
            Product product = repo.Get單筆資料ByProductId(id.Value);


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                //repo.Add一筆資料(product);
                repo.Add(product);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();

                repo.Update(product);
                repo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductId(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();

            Product product = repo.Get單筆資料ByProductId(id);
            repo.Delete(product);
            repo.UnitOfWork.Commit();

            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public ActionResult ListProduct()
        {
            //var data = db.Product
            //.Where(p => p.Active == true)

            var data = repo.GetProduct列表頁資料(true)
                .Select(p => new ProductLiteVM()
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View(data);
        }

       
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductLiteVM data)
        {
            if(ModelState.IsValid)
            {
                //TODO 儲存資料進資料庫
                

                return RedirectToAction("ListProduct");
            }
            //驗證失敗,繼續顯示原本的表單
            return View();
        }
    }
}
