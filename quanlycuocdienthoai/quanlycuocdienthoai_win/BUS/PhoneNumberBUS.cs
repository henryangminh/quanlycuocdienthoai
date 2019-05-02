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
    public class PhoneNumberBUS
    {
        InvoiceRegisterBUS invoiceRegisterBUS = new InvoiceRegisterBUS();
        InvoicePostageBUS invoicePostageBUS = new InvoicePostageBUS();
        PhoneNumberDAL phoneNumberDAL = new PhoneNumberDAL();
        SimDAL simDAL = new SimDAL();
        Shared shared = new Shared();
        /*
        public int GetKeyIdOfPhoneNumber(string number)
        {
            return GetByPhoneNumber(number).KeyId;
        }
        */
        
        public List<PhoneNumber> GetAll()
        {
            return phoneNumberDAL.GetAll();
        }

        public PhoneNumber GetById(int id)
        {
            return phoneNumberDAL.GetById(id);
        }

        public PhoneNumber GetByPhoneNumber(string number)
        {
            return phoneNumberDAL.GetByPhoneNumber(number);
        }

        public List<PhoneNumber> GetActivePhoneNumber()
        {
            return phoneNumberDAL.GetActivePhoneNumber();
        }

        public bool CheckAddPhoneNumber(string phoneNumber)
        {
            if (phoneNumber == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!shared.CheckDigital(phoneNumber))
            {
                MessageBox.Show("SĐT chỉ chứa số");
                return false;
            }

            if (phoneNumber.Length != 10)
            {
                MessageBox.Show("SĐT phải có đúng 10 số");
                return false;
            }

            if (phoneNumber[0] != '0')
            {
                MessageBox.Show("SĐT phải có số 0 đầu tiên");
                return false;
            }

            return true;
        }

        public bool SaveEntities(PhoneNumber phoneNumber)
        {
            if (phoneNumber.KeyId == 0)
            {
                if (phoneNumberDAL.Add(phoneNumber) != null)
                {
                    return true;
                }
                return false;
            }
            else
            {
                if (phoneNumberDAL.Update(phoneNumber) != null)
                {
                    return true;
                }
                return false;
            }
        }

        public List<PhoneNumber> GetAvailablePhoneNumber(DateTime dateTime)
        {
            List<PhoneNumber> availablePhoneNumber = new List<PhoneNumber>();
            //lấy những đơn đăng ký nào đăng ký hòa mạng trong tháng đó hoặc trước đó
            List<InvoiceRegister> invoiceRegisters = invoiceRegisterBUS.GetTheRegisterInThatMonthOrBefore(dateTime);
            //lấy sđt từ các hóa đơn đó
            foreach (var invoiceRegister in invoiceRegisters)
            {
                PhoneNumber phoneNumber = new PhoneNumber();
                phoneNumber = GetById(invoiceRegister.PhoneNumberFK);
                availablePhoneNumber.Add(phoneNumber);
            }
            //lấy những sđt đã đóng tiền trong tháng gần nhất
            foreach (var phoneNumber in availablePhoneNumber.ToList())
            {
                if (!invoicePostageBUS.CheckPaidInvoice(phoneNumber))
                {
                    availablePhoneNumber.Remove(phoneNumber);
                }
            }
            return availablePhoneNumber;
        }
    }
}
