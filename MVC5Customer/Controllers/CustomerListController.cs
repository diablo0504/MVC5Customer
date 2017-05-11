using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC5Customer.Models;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace MVC5Customer.Controllers
{
    public class CustomerListController : Controller
    {
        CustomerEntities db = new CustomerEntities();
        // GET: CustomerList
        public ActionResult Index()
        {
            var list = db.View_CustomerDataNum.ToList();
            return View(list);
        }
    }
}