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
            return db.SIMs.Where(x => x.PhoneNumberFK == idPhoneNo && x.Status == true).FirstOrDefault();
        }

        public List<SIM> GetActiveSim()
        {
            return db.SIMs.Where(x => (x.Status == false && x.PhoneNumberFK == null)).ToList();
        }

        public List<SIM> GetAll()
        {
            return db.SIMs.ToList();
        }

        public SIM GetById(int id)
        {
            return db.SIMs.Find(id);
        }

        public bool Add(SIM sIM)
        {
            if (sIM == null) return false;
            db.SIMs.Add(sIM);
            return true;
        }

        public bool Update(SIM sIM)
        {
            if (sIM != null) return false;
            db.Entry(sIM).State = EntityState.Modified;
            return true;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}