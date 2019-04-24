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

        public Postage GetTheLastPostage()
        {
            return db.Postages.OrderByDescending(x => x.KeyId).FirstOrDefault();
        }

        public Postage Add(Postage postage)
        {
            if (postage == null) return null;
            db.Entry(postage).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return postage;
        }

        public Postage Update(Postage postage)
        {
            if (postage == null) return null;
            db.Entry(postage).State = EntityState.Modified;
            SaveChanges();
            return postage;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
