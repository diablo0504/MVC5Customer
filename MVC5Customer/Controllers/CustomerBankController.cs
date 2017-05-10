using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MVC5Customer.Models
{
   
    public class CustomerBankController : Controller
    {
        CustomerEntities db = new CustomerEntities();
        // GET: CustomerBank
        public ActionResult Index()
        {
            var list = db.客戶銀行資訊.AsQueryable();
            var data = list.OrderByDescending(c => c.Id);
            return View(data);
        }
        public ActionResult Create()
        {
            var customerNameList = db.客戶資料.AsQueryable().Where(c => c.IsDelete ==false);
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var name in customerNameList)
            {
                items.Add(new SelectListItem()
                {
                    Text = name.客戶名稱,
                    Value = name.Id.ToString()
                });
            }
            ViewBag.items = items;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶銀行資訊 customerBank, string 客戶名稱)
        {
            if (ModelState.IsValid)
            {
                int test;
                if(int.TryParse(客戶名稱 ,out test))
                {
                    customerBank.客戶Id = int.Parse(客戶名稱);
                    var ss = 客戶名稱;
                    db.客戶銀行資訊.Add(customerBank);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            var data = db.客戶銀行資訊.Find(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int id , 客戶銀行資訊 customerBank)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶銀行資訊.Find(id);
                item.銀行名稱 = customerBank.銀行名稱;
                item.銀行代碼 = customerBank.銀行代碼;
                item.帳戶號碼 = customerBank.帳戶號碼;
                item.帳戶名稱 = customerBank.帳戶名稱;
                item.客戶資料 = customerBank.客戶資料;
                //item.客戶Id = customerBank.客戶Id;
                item.分行代碼 = customerBank.分行代碼;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var data = db.客戶銀行資訊.Find(id);
            return View(data);
        }
        public ActionResult Delete(int id )
        {
            var bank = db.客戶銀行資訊.Find(id);
            //db.客戶銀行資訊.Remove(bank);
            bank.IsDelete = true;
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