using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Customer.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }
        public 客戶聯絡人 GetOneCustomerPersonDataByID(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶聯絡人> GetCustomerPersonList(bool showAll = false, string keyword = "", string PersonJob = "", string sortOrder ="")
        {
            IQueryable<客戶聯絡人> all = this.All();
            if (showAll)
            {
                all = base.All();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                all = all.Where(p => p.姓名.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(PersonJob) && PersonJob != "0")
            {
                all = all.Where(p => p.職稱.Contains(PersonJob));
            }
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "job":
                        all = all.OrderBy(p => p.職稱);
                        break;
                    case "job_desc":
                        all = all.OrderByDescending(p => p.職稱);
                        break;
                    case "name":
                        all = all.OrderBy(p => p.姓名);
                        break;
                    case "name_desc":
                        all = all.OrderByDescending(p => p.姓名);
                        break;
                    case "email":
                        all = all.OrderBy(p => p.Email);
                        break;
                    case "email_desc":
                        all = all.OrderByDescending(p => p.Email);
                        break;
                    case "phone":
                        all = all.OrderBy(p => p.手機);
                        break;
                    case "phone_desc":
                        all = all.OrderByDescending(p => p.手機);
                        break;
                    case "tell":
                        all = all.OrderBy(p => p.電話);
                        break;
                    case "tell_desc":
                        all = all.OrderByDescending(p => p.電話);
                        break;
                    case "cus":
                        all = all.OrderBy(p => p.客戶資料);
                        break;
                    case "cus_desc":
                        all = all.OrderByDescending(p => p.客戶資料);
                        break;
                    default:
                        all = all.OrderByDescending(p => p.Id);
                        break;
                }
            }
            return all
                .Take(10);
        }
        public void Update(客戶聯絡人 customerPerson)
        {
            this.UnitOfWork.Context.Entry(customerPerson).State = EntityState.Modified;
        }
        public override void Delete(客戶聯絡人 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.IsDelete = true;

        }
        public IQueryable<string> GetPersonJobTitleList()
        {
            IQueryable<客戶聯絡人> all = this.All();
            var DropList = all.Select(p => p.職稱).Distinct();
            return DropList;
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}