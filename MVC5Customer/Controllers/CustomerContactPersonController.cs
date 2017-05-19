using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Net;

namespace MVC5Customer.Controllers
{
    public class CustomerContactPersonController : Controller
    {
       // CustomerEntities db = new CustomerEntities();

        客戶聯絡人Repository  repo = RepositoryHelper.Get客戶聯絡人Repository();
        客戶資料Repository repoCus = RepositoryHelper.Get客戶資料Repository();

        // GET: CustomerContactPerson
        public ActionResult Index(string keyword,string PersonJob, string sortOrder)
        {
            ViewBag.JobSortParm = sortOrder == "job" ? "job_desc" : "job";
            ViewBag.NameSortParm = sortOrder == "name" ? "name_desc" : "name";
            ViewBag.EmailSortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.PhoneSortParm = sortOrder == "phone" ? "phone_desc" : "phone";
            ViewBag.TellSortParm = sortOrder == "tell" ? "tell_desc" : "tell";
            //ViewBag.CusNameSortParm = sortOrder == "cus" ? "cus_desc" : "cus";

            var data = repo.GetCustomerPersonList(false, keyword , PersonJob, sortOrder);
            ViewData.Model = data;

            var dropJob = repo.GetPersonJobTitleList();
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Text = "職稱篩選", Value = "0" });
            foreach (var name in dropJob)
            {
                items.Add(new SelectListItem()
                {
                    Text = name.ToString(),
                    Value = name.ToString()
                });
            }
            ViewBag.PersonJob = items;

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
        public ActionResult Create(客戶聯絡人 person , string 客戶Id)
        {
            if (ModelState.IsValid)
            {
                int test;
                if (int.TryParse(客戶Id, out test))
                {
                    person.客戶Id = int.Parse(客戶Id);
                    repo.Add(person);
                    repo.UnitOfWork.Commit();
                    return RedirectToAction("Index");
                }
            }else
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
            }
            return View();


            //if (ModelState.IsValid)
            //{
            //    db.客戶聯絡人.Add(person);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    var customerNameList = db.客戶資料.AsQueryable().Where(p => p.IsDelete == false);
            //    List<SelectListItem> items = new List<SelectListItem>();
            //    foreach (var name in customerNameList)
            //    {
            //        items.Add(new SelectListItem()
            //        {
            //            Text = name.客戶名稱.ToString(),
            //            Value = name.Id.ToString()
            //        });
            //    }
            //    ViewBag.items = items;
            //}
            //return View();
        }
        public ActionResult Edit(int? id )
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 cusbank = repo.GetOneCustomerPersonDataByID(id.Value);
            if (cusbank == null)
            {
                return HttpNotFound();
            }
            return View(cusbank);
        }
        [HttpPost]
        public ActionResult Edit(int id, 客戶聯絡人 person)
        {

            var cusperson = repo.GetOneCustomerPersonDataByID(id);
            if (ModelState.IsValid)
            {

                cusperson.電話 = person.電話;
                cusperson.職稱 = person.職稱;
                cusperson.手機 = person.手機;
                cusperson.客戶資料 = person.客戶資料;
                cusperson.姓名 = person.姓名;
                cusperson.Email = person.Email;

                repo.Update(cusperson);
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
            客戶聯絡人 customerperson = repo.GetOneCustomerPersonDataByID(id.Value);
            return View(customerperson);

        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 person = repo.GetOneCustomerPersonDataByID(id.Value);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);

        }

        [HttpPost]
        public ActionResult Delete(int? id, 客戶聯絡人 customerperson)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 person = repo.GetOneCustomerPersonDataByID(id.Value);
            try
            {
                repo.Delete(person);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(person);
            }
        }



        //[HttpPost]
        //public ActionResult Index(SearchContactViewMode Contact)
        //{
        //    var list = db.客戶聯絡人.AsQueryable();
        //    if (string.IsNullOrEmpty(Contact.ContactQuery))
        //    {
        //        Contact.ContactQuery = "";
        //    }
        //    var data = list.Where(c => (c.姓名.Contains(Contact.ContactQuery)|| c.職稱.Contains(Contact.ContactQuery)) && c.IsDelete == false).OrderByDescending(c => c.Id);
        //    return View(data);
        //    //return View();
        //}
    }
}