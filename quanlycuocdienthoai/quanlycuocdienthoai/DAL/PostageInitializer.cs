using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace quanlycuocdienthoai.DAL
{
    public class PostageInitializer : DropCreateDatabaseIfModelChanges<PostageContext>
    {
        protected override void Seed(PostageContext context)
        {
            if(context.Customers.Count() == 0)
            {
                context.Customers.AddRange(new List<Customer>()
                {
                    new Customer(){ CustomerName="Bé Yêu", Address="Số 1 Đường Bé Yêu", DateRegistered=new DateTime(2019,4,1), Email="hippore114@gmail.com", CMND="069123412" }
                });
            }
            context.SaveChanges();

            if (context.PhoneNumbers.Count() == 0)
            {
                context.PhoneNumbers.AddRange(new List<PhoneNumber>()
                {
                    new PhoneNumber() { PhoneNo="0919991167", Status=true},
                });
            }
            context.SaveChanges();

            if (context.SIMs.Count() == 0)
            {
                context.SIMs.AddRange(new List<SIM>()
                {
                    new SIM(){ PhoneNumberFK=1, Status = true},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false}
                });
            }
            context.SaveChanges();
        }
    }
}
