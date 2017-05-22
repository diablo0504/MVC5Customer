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

        public IQueryable<客戶資料> GetCustomerList(bool showAll = false, string keyword = "" ,string drop ="" ,string sortOrder ="")
        {
            IQueryable<客戶資料> all = this.All();
            int test = 0;
            if (showAll)
            {
                all = base.All();
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                all = all.Where(p => p.客戶名稱.Contains(keyword));
            }
            if (!string.IsNullOrEmpty(drop) && int.TryParse(drop , out test))
            {
                int chose = int.Parse(drop);
                if(chose != 0) { all = all.Where(p => p.客戶分類 == chose); }
            }
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "name":
                        all = all.OrderBy(p => p.客戶名稱);
                        break;
                    case "name_desc":
                        all = all.OrderByDescending(p => p.客戶名稱);
                        break;
                    case "num":
                        all = all.OrderBy(p => p.統一編號);
                        break;
                    case "num_desc":
                        all = all.OrderByDescending(p => p.統一編號);
                        break;
                    case "tell":
                        all = all.OrderBy(p => p.電話);
                        break;
                    case "tell_desc":
                        all = all.OrderByDescending(p => p.電話);
                        break;
                    case "fax":
                        all = all.OrderBy(p => p.傳真);
                        break;
                    case "fax_desc":
                        all = all.OrderByDescending(p => p.傳真);
                        break;
                    case "address":
                        all = all.OrderBy(p => p.地址);
                        break;
                    case "address_desc":
                        all = all.OrderByDescending(p => p.地址);
                        break;
                    case "email":
                        all = all.OrderBy(p => p.Email);
                        break;
                    case "email_desc":
                        all = all.OrderByDescending(p => p.Email);
                        break;

                    default:
                        all = all.OrderByDescending(p => p.Id);
                        break;
                }
            }
            return all
                ;
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