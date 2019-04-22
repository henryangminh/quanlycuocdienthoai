using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.DAL
{
    public class InvoiceRegisterDAL
    {
        PostageContext db = new PostageContext();

        public List<InvoiceRegister> GetAll()
        {
            return db.InvoiceRegisters.ToList();
        }

        public InvoiceRegister GetById(int id)
        {
            return db.InvoiceRegisters.Find(id);
        }

        public bool Add(InvoiceRegister invoiceRegister)
        {
            if (invoiceRegister != null) return false;
            db.InvoiceRegisters.Add(invoiceRegister);
            return true;
        }

        public bool Update(InvoiceRegister invoiceRegister)
        {
            if (invoiceRegister != null) return false;
            db.Entry(invoiceRegister).State = EntityState.Modified;
            return true;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public List<InvoiceRegister> GetByCustomer(List<InvoiceRegister> invoiceRegisters, Customer customer)
        {
            return invoiceRegisters.Where(x => x.CustomerFK == customer.KeyId).ToList();
        }

        public List<InvoiceRegister> GetByPhoneNumber(List<InvoiceRegister> invoiceRegisters, PhoneNumber phoneNumber)
        {
            return invoiceRegisters.Where(x => x.PhoneNumberFK == phoneNumber.KeyId).ToList();
        }

        /// <summary>
        /// lấy những đơn đăng ký nào đăng ký hòa mạng trong tháng đó hoặc trước đó
        /// </summary>
        /// <returns></returns>
        public List<InvoiceRegister> GetTheRegisterInThatMonthOrBefore(DateTime date)
        {
            return db.InvoiceRegisters.Where(x => (x.DateRegisted.CompareTo(date) >= 0 && x.Status)).ToList();
        }
    }
}
