using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Customer.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }

        public 客戶資料 GetOneCustomerDataByID(int id)
        {
            return this.All().FirstOrDefault( p => p.Id ==id);
        }

        public IQueryable<客戶資料> GetCustomerList(bool showAll = false, string keyword = "")
        {
            IQueryable<客戶資料> all = this.All();
            if (showAll)
            {
                all = base.All();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                all = all.Where(p => p.客戶名稱.Contains(keyword));
            }
            return all
                .OrderByDescending(p => p.Id).Take(10);
        }
        public void Update(客戶資料 customer)
        {
            this.UnitOfWork.Context.Entry(customer).State = EntityState.Modified;
        }
        public override void Delete(客戶資料 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.IsDelete = true;
        }

    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}