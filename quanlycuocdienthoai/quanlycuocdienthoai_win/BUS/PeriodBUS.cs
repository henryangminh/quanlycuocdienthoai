using quanlycuocdienthoai.Models;
using quanlycuocdienthoai_win.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.BUS
{
    public class PeriodBUS
    {
        PeriodDAL periodDAL = new PeriodDAL();

        public Period SaveEntities(Period period)
        {
            return periodDAL.Add(period);
        }

        public Period GetByDate(DateTime dateTime)
        {
            return periodDAL.GetByDate(dateTime);
        }
    }
}
