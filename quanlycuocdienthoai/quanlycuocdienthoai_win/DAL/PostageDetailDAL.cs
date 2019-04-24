using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.DAL
{
    public class PostageDetailDAL
    {
        PostageContext db = new PostageContext();

        public List<PostageDetail> GetByPostageId(int postageId)
        {
            return db.PostageDetails.Where(x => x.PostageFK == postageId).ToList();
        }

        public PostageDetail Add(PostageDetail postageDetail)
        {
            if (postageDetail == null) return null;
            db.Entry(postageDetail).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            return postageDetail;
        }

        public bool Add(List<PostageDetail> postageDetails)
        {
            db.PostageDetails.AddRange(postageDetails);
            db.SaveChanges();
            return true;
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
