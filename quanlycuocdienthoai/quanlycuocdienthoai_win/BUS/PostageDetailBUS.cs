using quanlycuocdienthoai.Models;
using quanlycuocdienthoai_win.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.BUS
{
    public class PostageDetailBUS
    {
        PostageDetailDAL postageDetailDAL = new PostageDetailDAL();

        public List<PostageDetail> GetByPostageId(int postageId)
        {
            return postageDetailDAL.GetByPostageId(postageId);
        }
        /*
        public bool Add(PostageDetail postageDetail)
        {
            return postageDetailDAL.Add(postageDetail);
        }

        public bool Add(List<PostageDetail> postageDetails)
        {
            foreach (var postageDetail in postageDetails)
            {
                if (!Add(postageDetail))
                    return false;
            }
            return true;
        }
        */
        public bool SaveEntities(List<PostageDetail> postageDetails)
        {
            postageDetailDAL.Add(postageDetails);
            return true;
        }
    }
}
