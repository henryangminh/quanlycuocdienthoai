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
    public class PostageDAL
    {
        PostageContext db = new PostageContext();

        public List<Postage> GetAll()
        {
            return db.Postages.ToList();
        }

        public Postage GetById(int id)
        {
            return db.Postages.Find(id);
        }

        public bool Add(Postage postage)
        {
            if (postage != null) return false;
            db.Postages.Add(postage);
            return true;
        }

        public bool Update(Postage postage)
        {
            if (postage != null) return false;
            db.Entry(postage).State = EntityState.Modified;
            return true;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
