using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity.Validation;

namespace MVC5Customer.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        CustomerEntities db = new CustomerEntities();
        public ActionResult Index()
        {
            var list = db.客戶資料.AsQueryable();
            var data = list.Where(c => c.IsDelete == false).OrderByDescending(c => c.Id);
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(客戶資料 c)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             
            return View();
        }
        public ActionResult Edit(int id)
        {
            var dataEdit = db.客戶資料.Find(id);
            return View(dataEdit);
        }
        [HttpPost]
        public ActionResult Edit(int id,客戶資料 customer)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶資料.Find(id);
                item.電話 = customer.電話;
                item.統一編號 = customer.統一編號;
                item.客戶銀行資訊 = customer.客戶銀行資訊;
                item.客戶聯絡人 = customer.客戶聯絡人;
                item.客戶名稱 = customer.客戶名稱;
                item.地址 = customer.地址;
                item.傳真 = customer.傳真;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var data = db.客戶資料.Find(id);
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var customer = db.客戶資料.Find(id);
            db.客戶聯絡人.RemoveRange(customer.客戶聯絡人);
            db.客戶銀行資訊.RemoveRange(customer.客戶銀行資訊);
            
            customer.IsDelete = true;
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
            return RedirectToAction("Index");
        }

  
    
    }
}