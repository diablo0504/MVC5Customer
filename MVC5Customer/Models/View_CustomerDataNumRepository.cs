using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Customer.Models
{   
	public  class View_CustomerDataNumRepository : EFRepository<View_CustomerDataNum>, IView_CustomerDataNumRepository
	{

	}

	public  interface IView_CustomerDataNumRepository : IRepository<View_CustomerDataNum>
	{

	}
}