using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Customer.Models.ValidationAttributes
{
    public class CheckContactPersonEmailRepeat : DataTypeAttribute
    {
        public CheckContactPersonEmailRepeat() : base(DataType.Text)
        {

        }
        public override bool IsValid(object email )
        {
            CustomerEntities db = new CustomerEntities();
            var emailC = (string)email;
            var chkemail = db.客戶聯絡人.Where(c => c.IsDelete == false && c.Email == emailC).Any();
            if (chkemail) { chkemail = false; } else { chkemail = true; }
            return chkemail;
        }
    }
}