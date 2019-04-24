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

        public Customer Add(Customer customer)
        {
            if (customer == null) return null;
            db.Entry(customer).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return customer;
        }

        public Customer Update(Customer customer)
        {
            if (customer == null) return null;
            db.Entry(customer).State = EntityState.Modified;
            SaveChanges();
            return customer;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
