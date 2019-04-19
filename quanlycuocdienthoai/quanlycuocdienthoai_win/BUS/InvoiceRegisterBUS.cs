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
    public class InvoiceRegisterBUS
    {
        SimDAL simDAL = new SimDAL();
        PhoneNumberDAL phoneNumberDAL = new PhoneNumberDAL();
        InvoiceRegisterDAL invoiceRegisterDAL = new InvoiceRegisterDAL();
        CustomerDAL customerDAL = new CustomerDAL();
        Shared shared = new Shared();

        public List<InvoiceRegister> GetAll()
        {
            return invoiceRegisterDAL.GetAll();
        }

        public InvoiceRegister GetById(int id)
        {
            return invoiceRegisterDAL.GetById(id);
        }

        public List<InvoiceRegister> GetByCustomer(List<InvoiceRegister> invoiceRegisters ,string cmnd)
        {
            Customer customer = customerDAL.GetCustomerByCMND(cmnd);
            return invoiceRegisterDAL.GetByCustomer(invoiceRegisters, customer);
        }

        public List<InvoiceRegister> GetByPhoneNumber(List<InvoiceRegister> invoiceRegisters, string number)
        {
            PhoneNumber phoneNumber = phoneNumberDAL.GetByPhoneNumber(number);
            return invoiceRegisterDAL.GetByCustomer(invoiceRegisters, phoneNumber);
        }

        public bool CheckRegisterInformations(string customerKeyId, string phoneNo, string cost)
        {
            if (customerKeyId == "xxx")
            {
                MessageBox.Show("Chưa nhập hoặc nhập sai CMND");
                return false;
            }

            if (phoneNo == "" || cost == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!shared.CheckDigital(cost))
            {
                MessageBox.Show("Phí hòa mạng phải là số");
                return false;
            }

            if (simDAL.GetTheLastestSIM(phoneNo) == null)
            {
                MessageBox.Show("Số điện thoại này chưa có SIM");
                return false;
            }

            if (phoneNumberDAL.GetByPhoneNumber(phoneNo).Status)
            {
                MessageBox.Show("Số điện thoại này đang được sử dụng");
                return false;
            }
            return true;
        }

        public bool SaveEntities(InvoiceRegister invoiceRegister)
        {
            if (invoiceRegister.KeyId == 0)
            {
                if (invoiceRegisterDAL.Add(invoiceRegister))
                {
                    PhoneNumber phoneNumber = phoneNumberDAL.GetById(invoiceRegister.PhoneNumberFK);
                    phoneNumber.Status = true;
                    if (phoneNumberDAL.Update(phoneNumber))
                    {
                        invoiceRegisterDAL.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            else
            {
                if (invoiceRegisterDAL.Update(invoiceRegister))
                {
                    PhoneNumber phoneNumber = phoneNumberDAL.GetById(invoiceRegister.PhoneNumberFK);
                    phoneNumber.Status = false;

                    SIM sIM = simDAL.GetTheLastestSIM(phoneNumber.PhoneNo);
                    sIM.Status = false;
                    if (invoiceRegisterDAL.Update(invoiceRegister) && phoneNumberDAL.Update(phoneNumber) && simDAL.Update(sIM))
                    {
                        invoiceRegisterDAL.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
