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
        public IQueryable<客戶聯絡人> GetCustomerPersonList(bool showAll = false, string keyword = "")
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
            return all
                .OrderByDescending(p => p.Id).Take(10);
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
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}