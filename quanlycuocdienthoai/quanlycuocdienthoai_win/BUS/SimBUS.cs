using quanlycuocdienthoai.EF;
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
    public class SimBUS
    {
        PostageContext db = new PostageContext();
        SimDAL simDAL = new SimDAL();
        PhoneNumberDAL phoneNumberDAL = new PhoneNumberDAL();

        /// <summary>
        /// Lấy sim của 1 SĐT. Kiểm tra xem SĐT đã có SIM hay chưa
        /// </summary>
        /// <param name="number">SĐT</param>
        /// <returns>Trả về SIM đã có hoặc null</returns>
        public SIM GetTheLastestSIM(string number)
        {
            return simDAL.GetTheLastestSIM(number);
        }

        public List<SIM> GetAll()
        {
            return simDAL.GetAll();
        }

        public SIM GetById(int id)
        {
            return simDAL.GetById(id);
        }

        public List<SIM> GetActiveSim()
        {
            return simDAL.GetActiveSim();
        }

        public List<SIM> GetSimByPhoneNumber(string number)
        {
            return db.SIMs.Where(p => p.PhoneNumberFKNavigation.PhoneNo == number).ToList();
        }

        public bool CheckAssignSIM(string sim, string phoneNumber)
        {
            if (sim == "" || phoneNumber == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (simDAL.GetById(Convert.ToInt32(sim)) == null || phoneNumberDAL.GetByPhoneNumber(phoneNumber) == null)
            {
                MessageBox.Show("Sim hoặc SĐT đã nhập không có trong CSDL");
                return false;
            }

            if (GetTheLastestSIM(phoneNumber) != null)
            {
                MessageBox.Show("SĐT này đã có SIM. Muốn chuyển SIM, hãy khóa SIM hiện tại của SĐT này");
                return false;
            }
            return true;
        }

        public bool SaveEntities(SIM sIM)
        {
            if (sIM.KeyId == 0)
            {
                if (simDAL.Add(sIM))
                {
                    simDAL.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                if (simDAL.Update(sIM))
                {
                    simDAL.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
