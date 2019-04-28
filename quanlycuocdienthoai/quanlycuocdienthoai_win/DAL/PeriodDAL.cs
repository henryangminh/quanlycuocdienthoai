using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.DAL
{
    public class PeriodDAL
    {
        PostageContext db = new PostageContext();
        public List<Period> GetAll()
        {
            return db.Periods.OrderBy(x => x.PeriodPayment).ToList();
        }

        public Period GetByDate(DateTime dateTime)
        {
            return db.Periods.Where(x => x.PeriodPayment == dateTime).FirstOrDefault();
        }

        public Period Add(Period period)
        {
            if (period == null) return null;
            db.Entry(period).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return period;
        }
    }
}
