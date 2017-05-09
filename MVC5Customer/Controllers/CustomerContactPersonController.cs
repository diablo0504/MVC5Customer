﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity;

namespace MVC5Customer.Controllers
{
    public class CustomerContactPersonController : Controller
    {
        CustomerEntities db = new CustomerEntities();
        // GET: CustomerContactPerson
        public ActionResult Index()
        {
            var list = db.客戶聯絡人.AsQueryable();
            var data = list.OrderByDescending(p => p.Id);
            return View(data);
        }
        public ActionResult Create()
        {
            var customerNameList = db.客戶資料.AsQueryable();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var name in customerNameList)
            {
                items.Add(new SelectListItem()
                {
                    Text = name.客戶名稱.ToString(),
                    Value = name.Id.ToString()
                });
            }
            ViewBag.items = items;
            return View();
        }
        [HttpPost]
        public ActionResult Create(客戶聯絡人 person)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id )
        {
            var data = db.客戶聯絡人.Find(id);
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 person)
        {
            if (ModelState.IsValid)
            {
                var item = db.客戶聯絡人.Find(id);
                item.電話 = person.電話;
                item.職稱 = person.職稱;
                item.手機 = person.手機;
                item.客戶資料 = person.客戶資料;
                item.姓名 = person.姓名;
                item.Email = person.Email;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Details(int id)
        {
            var data = db.客戶聯絡人.Find(id);
            return View(data);
        }
        public ActionResult Delete(int id)
        {
            var person = db.客戶聯絡人.Find(id);
            db.客戶聯絡人.Remove(person);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}