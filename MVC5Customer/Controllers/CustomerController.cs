﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity.Validation;
using System.Net;
using PagedList;

namespace MVC5Customer.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer  CustomerSelectList
        // CustomerEntities db = new CustomerEntities();

        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();
        //private int pageSize = 5;
        public ActionResult Index(string keyword, string drop, string sortOrder , int PageNo = 1)
        {
            //var data = db.客戶資料.Where(c => c.IsDelete == false).AsQueryable();
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.NumSortParm = sortOrder == "num" ? "num_desc" : "num";
            ViewBag.TellSortParm = sortOrder == "tell" ? "tell_desc" : "tell";
            ViewBag.FaxSortParm = sortOrder == "fax" ? "fax_desc" : "fax";
            ViewBag.AddressSortParm = sortOrder == "address" ? "address_desc" : "address";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";

            //int currentPage = 5;

            

            var data = repo.GetCustomerList(false, keyword, drop, sortOrder);

            //var result = data.OrderByDescending(p=>p.Id).ToPagedList(currentPage, pageSize);
            ViewData.Model = data.OrderByDescending(p => p.Id).ToPagedList(PageNo, 2);
            //ViewData.Model = data;

            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "客戶類別篩選", Value = "0" });

            foreach (var name in Enum.GetValues(typeof(customerClass)))
            {
                items.Add(new SelectListItem()
                {
                    Text = name.ToString(),
                    Value = ((int)name).ToString()
                });
            }
            ViewBag.drop = items;
            return View();
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
                //db.客戶資料.Add(c);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                repo.Add(c);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
             
            return View();
        }
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 customer = repo.GetOneCustomerDataByID(id.Value);
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        [HttpPost]
        public ActionResult Edit(int id,客戶資料 customer)
        {
            var data = repo.GetOneCustomerDataByID(id);
            if (ModelState.IsValid)
            {
                repo.Update(customer);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(data);
        }
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 customer = repo.GetOneCustomerDataByID(id.Value);
            return View(customer);
        }
        public ActionResult Delete(int? id)
        {
            if( id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 customer = repo.GetOneCustomerDataByID(id.Value);
            if(customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
            //var customer = db.客戶資料.Find(id);
            //db.客戶聯絡人.RemoveRange(customer.客戶聯絡人);
            //db.客戶銀行資訊.RemoveRange(customer.客戶銀行資訊);
            
            //customer.IsDelete = true;
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
        public ActionResult Delete( int? id ,客戶資料 customer)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 cus = repo.GetOneCustomerDataByID(id.Value);
            try
            {
                repo.Delete(cus);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }catch
            {
                return View(cus);
            } 
        }


        //[HttpPost]
        //public ActionResult Index (SearchViewMode sss)
        //{
        //    var list = db.客戶資料.AsQueryable();
        //    if (string.IsNullOrEmpty(sss.Query))
        //    {
        //        sss.Query = "";
        //    }
           
        //    var data = list.Where(c => c.客戶名稱.Contains(sss.Query) && c.IsDelete==false).OrderByDescending(c => c.Id);
        //    return View(data);
        //    //return View();
        //}
  
    
    }
}