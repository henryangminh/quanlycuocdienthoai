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
    public class CustomerBUS
    {
        CustomerDAL customerDAL = new CustomerDAL();
        Shared shared = new Shared();

        /// <summary>
        /// Hàm kiểm tra các trường đã nhập có thỏa mãn không bị bể database hay không
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identity"></param>
        /// <param name="address"></param>
        /// <param name="email"></param>
        /// <param name="checkCMNDexists">Nếu cần check CMND đã có hay chưa, check khi gọi hàm add</param>
        /// <returns></returns>
        public bool CheckCustomerInfomations(string name, string identity, string address, string email, bool checkCMNDexists)
        {
            if (name == "" || identity == "" || address == "" || email == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!shared.CheckDigital(identity))
            {
                MessageBox.Show("CMND phải là số");
                return false;
            }

            if (identity.Length != 9 && identity.Length != 12)
            {
                MessageBox.Show("CMND phải có 9 hoặc 12 số");
                return false;
            }
            if (checkCMNDexists)
            {
                var query = customerDAL.GetCustomerByCMND(identity);
                if (query != null)
                {
                    MessageBox.Show("CMND đã có");
                    return false;
                }
            }
            return true;
        }

        public List<Customer> GetAll()
        {
            return customerDAL.GetAll();
        }

        public bool SaveEntities(Customer customer)
        {
            if (customer.KeyId == 0)
            {
                if (customerDAL.Add(customer))
                {
                    customerDAL.SaveChanges();
                    return true;
                }
                return false;
            }
            else
            {
                if (customerDAL.Update(customer))
                {
                    customerDAL.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public Customer GetCustomerByCMND(string cmnd)
        {
            return customerDAL.GetCustomerByCMND(cmnd);
        }

        public Customer GetById(int id)
        {
            return customerDAL.GetById(id);
        }
    }
}
