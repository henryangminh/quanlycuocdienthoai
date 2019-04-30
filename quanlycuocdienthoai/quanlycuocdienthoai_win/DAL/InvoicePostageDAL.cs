using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

        public List<InvoicePostage> GetByPeriod(Period period)
        {
            return db.InvoicePostages
                .Include(x => x.PhoneNumberFKNavigation)
                .Include(x => x.PeriodFKNavigation)
                .Where(x => x.PeriodFK == period.KeyId)
                .ToList();
        }

        public List<InvoicePostage> Add(List<InvoicePostage> invoicePostages)
        {
            var model = db.InvoicePostages.AddRange(invoicePostages).ToList();
            db.SaveChanges();
            return model;
        }
    }
}
