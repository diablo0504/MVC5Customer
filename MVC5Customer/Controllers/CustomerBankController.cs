using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity;

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
            return View();
        }
        [HttpPost]
        public ActionResult Create(客戶銀行資訊 customerBank)
        {
            if (ModelState.IsValid)
            {
                db.客戶銀行資訊.Add(customerBank);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View();
        }
    }
}