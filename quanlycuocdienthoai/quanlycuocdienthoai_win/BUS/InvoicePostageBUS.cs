using quanlycuocdienthoai.Models;
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
    }
}
