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
    public class CustomerDAL
    {
        PostageContext db = new PostageContext();

        public Customer GetCustomerByCMND(string cmnd)
        {
            return db.Customers.Where(p => p.CMND == cmnd).FirstOrDefault();
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return db.Customers.Find(id);
        }

        public bool Add(Customer customer)
        {
            if (customer != null) return false;
            db.Customers.Add(customer);
            return true;
        }

        public bool Update(Customer customer)
        {
            if (customer != null) return false;
            db.Entry(customer).State = EntityState.Modified;
            return true;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
