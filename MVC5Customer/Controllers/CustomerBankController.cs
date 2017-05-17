using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Net;

namespace MVC5Customer.Models
{
   
    public class CustomerBankController : Controller
    {
        //CustomerEntities db = new CustomerEntities();
        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();
        客戶資料Repository repoCus = RepositoryHelper.Get客戶資料Repository();
        // GET: CustomerBank
        public ActionResult Index(string keyword)
        {
            var data = repo.GetCustomerBankList(false, keyword);
            ViewData.Model = data;
            return View(data);
        }
        public ActionResult Create()
        {
            var customerNameList = repoCus.GetCustomerList();
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
                    repo.Add(customerBank);
                    repo.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 cusbank = repo.GetOneCustomerDataByID(id.Value);
            if(cusbank == null)
            {
                return HttpNotFound();
            }
            return View(cusbank);
        }
        [HttpPost]
        public ActionResult Edit(int id , 客戶銀行資訊 customerBank)
        {
            var cusbank = repo.GetOneCustomerDataByID(id);
            if (ModelState.IsValid)
            {
                cusbank.銀行名稱 = customerBank.銀行名稱;
                cusbank.銀行代碼 = customerBank.銀行代碼;
                cusbank.帳戶號碼 = customerBank.帳戶號碼;
                cusbank.帳戶名稱 = customerBank.帳戶名稱;
                cusbank.客戶資料 = customerBank.客戶資料;
                cusbank.分行代碼 = customerBank.分行代碼;

                repo.Update(cusbank);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int? id)
        {
            //var data = db.客戶銀行資訊.Find(id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 customer = repo.GetOneCustomerDataByID(id.Value);
            return View(customer);
            //return View(data);
        }
        public ActionResult Delete(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 customerbank = repo.GetOneCustomerDataByID(id.Value);
            if (customerbank == null)
            {
                return HttpNotFound();
            }
            return View(customerbank);
            //var bank = db.客戶銀行資訊.Find(id);
            ////db.客戶銀行資訊.Remove(bank);
            //bank.IsDelete = true;
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    throw ex;
            //}
            //return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int? id, 客戶銀行資訊 customerbank)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 cusbank = repo.GetOneCustomerDataByID(id.Value);
            try
            {
                repo.Delete(cusbank);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(cusbank);
            }
        }




        //[HttpPost]
        //public ActionResult Index(SearchBankViewMode Bank)
        //{
        //    var list = db.客戶銀行資訊.AsQueryable();
        //    if (string.IsNullOrEmpty(Bank.BankQuery))
        //    {
        //        Bank.BankQuery = "";
        //    }
        //    var data = list.Where(c => c.銀行名稱.Contains(Bank.BankQuery) && c.IsDelete == false).OrderByDescending(c => c.Id);
        //    return View(data);
        //    //return View();
        //}
    }
}