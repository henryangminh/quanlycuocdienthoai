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
    public class PhoneNumberDAL
    {
        PostageContext db = new PostageContext();

        public PhoneNumber GetByPhoneNumber(string number)
        {
            return db.PhoneNumbers.Where(p => p.PhoneNo == number).FirstOrDefault();
        }

        public List<PhoneNumber> GetAll()
        {
            return db.PhoneNumbers.ToList();
        }

        public List<PhoneNumber> GetActivePhoneNumber()
        {
            return db.PhoneNumbers.Where(x => x.Status == false).ToList();
        }

        public PhoneNumber GetById(int id)
        {
            return db.PhoneNumbers.Find(id);
        }

        public PhoneNumber Add(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null) return null;
            db.Entry(phoneNumber).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return phoneNumber;
        }

        public PhoneNumber Update(PhoneNumber phoneNumber)
        {
            if (phoneNumber == null) return null;
            db.Entry(phoneNumber).State = EntityState.Modified;
            SaveChanges();
            return phoneNumber;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
