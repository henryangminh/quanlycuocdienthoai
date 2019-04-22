using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.DAL
{
    public class InvoicePostageDAL
    {
        PostageContext db = new PostageContext();

        public InvoicePostage GetTheLastInvoicePostage(int PhoneNumberFK)
        {
            return db.InvoicePostages.Where(x => x.PhoneNumberFK == PhoneNumberFK).OrderByDescending(x => x.PeriodFKNavigation.PeriodPayment).FirstOrDefault();
        }
    }
}
