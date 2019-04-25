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
    public class SimDAL
    {
        PostageContext db = new PostageContext();
        PhoneNumberDAL phoneNumberDAL = new PhoneNumberDAL();

        public SIM GetTheLastestSIM(string number)
        {
            int idPhoneNo = phoneNumberDAL.GetByPhoneNumber(number).KeyId;
            return db.SIMs.Where(x => x.PhoneNumberFK == idPhoneNo && x.Status == true)
                .Include(x => x.PhoneNumberFKNavigation)
                .FirstOrDefault();
        }

        public List<SIM> GetActiveSim()
        {
            return db.SIMs.Where(x => (x.Status == false && x.PhoneNumberFK == null))
                .Include(x => x.PhoneNumberFKNavigation)
                .ToList();
        }

        public List<SIM> GetAll()
        {
            return db.SIMs
                .Include(x => x.PhoneNumberFKNavigation)
                .ToList();
        }

        public SIM GetById(int id)
        {
            return db.SIMs.Find(id);
        }

        public SIM Add(SIM sIM)
        {
            if (sIM == null) return null;
            db.Entry(sIM).State = System.Data.Entity.EntityState.Added;
            SaveChanges();
            return sIM;
        }

        public SIM Update(SIM sIM)
        {
            if (sIM == null) return null;
            db.Entry(sIM).State = EntityState.Modified;
            SaveChanges();
            return sIM;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}