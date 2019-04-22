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

        public bool Add(PostageDetail postageDetail)
        {
            if (postageDetail == null) return false;
            db.PostageDetails.Add(postageDetail);
            return true;
        }

        public bool Add(List<PostageDetail> postageDetails)
        {
            db.PostageDetails.AddRange(postageDetails);
            return true;
        }
    }
}
