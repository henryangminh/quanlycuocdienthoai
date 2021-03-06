﻿using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace quanlycuocdienthoai.EF
{
    public class PostageInitializer : DropCreateDatabaseIfModelChanges<PostageContext>
    {
        protected override void Seed(PostageContext context)
        {
            if(context.Customers.Count() == 0)
            {
                context.Customers.AddRange(new List<Customer>()
                {
                    new Customer(){ CustomerName="Bé Yêu", Address="Số 1 Đường Bé Yêu", DateRegistered=new DateTime(2019,4,1), Email="hippore114@gmail.com", CMND="069123412" },
                    new Customer(){ CustomerName="Gấu", Address="Số 3 Đường Bé Yêu", DateRegistered=new DateTime(2019,5,1), Email="hotruongnhatminh@gmail.com", CMND="025861398" },
                    new Customer(){ CustomerName="TL", Address="Đoán xem", DateRegistered=new DateTime(2019,6,1), Email="tltltl@gmail.com", CMND="013929123" }
                });
            }
            context.SaveChanges();

            if (context.PhoneNumbers.Count() == 0)
            {
                context.PhoneNumbers.AddRange(new List<PhoneNumber>()
                {
                    new PhoneNumber() { PhoneNo="0919991167", Status=false},
                    new PhoneNumber() { PhoneNo="0984861168", Status=false},
                    new PhoneNumber() { PhoneNo="0523188169", Status=false},
                    new PhoneNumber() { PhoneNo="0819283912", Status=false},
                    new PhoneNumber() { PhoneNo="0220122221", Status=false},
                    new PhoneNumber() { PhoneNo="0234128399", Status=false},
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
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false},
                    new SIM(){ PhoneNumberFK=null, Status = false}
                });
            }
            context.SaveChanges();
        }
    }
}
