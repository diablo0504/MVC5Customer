using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace MVC5Customer.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(p => p.IsDelete == false);
        }
        public 客戶銀行資訊 GetOneCustomerDataByID(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
        public IQueryable<客戶銀行資訊> GetCustomerList(bool showAll = false)
        {
            IQueryable<客戶銀行資訊> all = this.All();
            if (showAll)
            {
                all = base.All();
            }
            return all
                .OrderByDescending(p => p.Id).Take(10);
        }
        public void Update(客戶銀行資訊 customer)
        {
            this.UnitOfWork.Context.Entry(customer).State = EntityState.Modified;
        }
        public override void Delete(客戶銀行資訊 entity)
        {
            this.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;
            entity.IsDelete = true;

        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}