using quanlycuocdienthoai.Models;
using quanlycuocdienthoai_win.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlycuocdienthoai_win.BUS
{
    public class PostageBUS
    {
        Shared shared = new Shared();
        PostageDAL postageDAL = new PostageDAL();
        PostageDetailDAL postageDetailDAL = new PostageDetailDAL();

        public bool CheckAddPostageDetails(string cost)
        {
            if (cost == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!shared.CheckDigital(cost))
            {
                MessageBox.Show("Cước phí phải là số");
                return false;
            }
            return true;
        }

        public bool CheckAddPostageDetails(string cost, TimeSpan timeSpan, List<PostageDetail> HourMarks)
        {
            if (!CheckAddPostageDetails(cost))
            {
                return false;
            }

            if (HourMarks.Count > 0 && HourMarks.Where(p => p.HourMark == timeSpan).ToList().Count > 0)
            {
                MessageBox.Show("Mốc giờ này đã có");
                return false;
            }
            return true;
        }

        public bool CheckAddPostageDetails(List<PostageDetail> postageDetails)
        {
            if (postageDetails.Count == 0)
            {
                MessageBox.Show("Phải thêm ít nhất 1 mốc giờ");
                return false;
            }
            return true;
        }

        public bool SaveEntities(Postage postage, List<PostageDetail> postageDetail)
        {
            if(postage.KeyId == 0)
            {
                if (postageDAL.Add(postage))
                {
                    if (postageDetailDAL.Add(postageDetail))
                    {
                        postageDAL.SaveChanges();
                        return true;
                    }
                }
            }
            return false;
        }

        public bool SaveEntities(Postage postage)
        {
            if (postage.KeyId > 0)
            {
                if (postageDAL.Update(postage))
                {
                    postageDAL.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public List<Postage> GetAll()
        {
            return postageDAL.GetAll();
        }
    }
}
