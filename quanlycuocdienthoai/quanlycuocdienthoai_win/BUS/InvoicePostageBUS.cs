﻿using quanlycuocdienthoai.Models;
using quanlycuocdienthoai_win.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.BUS
{
    public class InvoicePostageBUS
    {
        InvoicePostageDAL invoicePostageDAL = new InvoicePostageDAL();
        PeriodDAL periodDAL = new PeriodDAL();

        public InvoicePostage GetTheLastInvoicePostage(int PhoneNumberFK)
        {
            return invoicePostageDAL.GetTheLastInvoicePostage(PhoneNumberFK);
        }

        public bool CheckPaidInvoice(PhoneNumber phoneNumber)
        {
            if (GetTheLastInvoicePostage(phoneNumber.KeyId) == null || GetTheLastInvoicePostage(phoneNumber.KeyId).PaidPostage)
                return true;
            return false;
        }

        public List<InvoicePostage> GetByPeriod(DateTime dateTime)
        {
            Period period = periodDAL.GetByDate(dateTime);
            return invoicePostageDAL.GetByPeriod(period);
        }

        public List<InvoicePostage> SaveEntities(List<InvoicePostage> invoicePostages)
        {
            return invoicePostageDAL.Add(invoicePostages);
        }
    }
}
