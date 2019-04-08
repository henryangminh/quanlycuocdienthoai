using quanlycuocdienthoai.DAL;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlycuocdienthoai_win
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            grvCustomer.CellClick += new DataGridViewCellEventHandler(EditCustomer);

            LoadCustomer(db.Customers.ToList());

            List<PhoneNumber> phoneNumbers = GetAllActivePhoneNumbers();
            GetComboboxPhoneNumber(cbxRegisterPhoneNumber, phoneNumbers);
            GetComboboxPhoneNumber(cbxRegisterPhoneNumberSearch, phoneNumbers);
        }

        PostageContext db = new PostageContext();

        private void ClearDataGridView(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }

        #region Customer
        /// <summary>
        /// Hàm kiểm tra các trường đã nhập có thỏa mãn không bị bể database hay không
        /// </summary>
        /// <param name="name"></param>
        /// <param name="identity"></param>
        /// <param name="address"></param>
        /// <param name="email"></param>
        /// <param name="checkCMNDexists">Nếu cần check CMND đã có hay chưa, check khi gọi hàm add</param>
        /// <returns></returns>
        private bool CheckCustomerInfomations(string name, string identity, string address, string email, bool checkCMNDexists)
        {
            if (name == "" || identity == "" || address == "" || email == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (identity.Length != 9 && identity.Length != 12)
            {
                MessageBox.Show("CMND phải có 9 hoặc 12 số");
                return false;
            }
            if (checkCMNDexists)
            {
                var query = db.Customers.Where(p => p.CMND.Contains(identity)).FirstOrDefault();
                if (query != null)
                {
                    MessageBox.Show("CMND đã có");
                    return false;
                }
            }
            return true;
        }

        private Customer GetCustomerByCMND(string cmnd)
        {
            return db.Customers.Where(p => p.CMND.Contains(cmnd)).FirstOrDefault();
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.KeyId = 0;
            customer.CustomerName = txtCustomerName.Text;
            customer.CMND = txtCustomerIdentity.Text;
            customer.Address = txtCustomerAddress.Text;
            customer.Email = txtCustomerEmail.Text;
            customer.DateRegistered = DateTime.Now;

            if (CheckCustomerInfomations(txtCustomerName.Text, txtCustomerIdentity.Text, txtCustomerAddress.Text, txtCustomerEmail.Text, true))
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                MessageBox.Show("Thêm thành công");

                txtCustomerName.Text = "";
                txtCustomerIdentity.Text = "";
                txtCustomerAddress.Text = "";
                txtCustomerEmail.Text = "";
                txtCustomerDateRegister.Text = "";

                ClearDataGridView(grvCustomer);
                LoadCustomer(db.Customers.ToList());
            }
        }

        private void LoadCustomer(Customer customer)
        {
            int n = grvCustomer.Rows.Add();
            grvCustomer.Rows[n].Cells[0].Value = customer.KeyId;
            grvCustomer.Rows[n].Cells[1].Value = customer.CustomerName;
            grvCustomer.Rows[n].Cells[2].Value = customer.CMND;
            grvCustomer.Rows[n].Cells[3].Value = customer.Address;
            grvCustomer.Rows[n].Cells[4].Value = customer.Email;
            grvCustomer.Rows[n].Cells[5].Value = customer.DateRegistered;
        }

        private void LoadCustomer(List<Customer> customers)
        {
            foreach (var customer in customers)
            {
                LoadCustomer(customer);
            }
        }

        private void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            Customer customer = GetCustomerByCMND(txtCustomerSearch.Text);
            ClearDataGridView(grvCustomer);

            if (customer != null)
                LoadCustomer(customer);

            else
            {
                MessageBox.Show("Không có khách hàng có CMND này");
            }
        }

        private void EditCustomer(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;
            if (dataGridView != null)
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    lblCustomerId.Text = grvCustomer.Rows[rowIndex].Cells[0].Value.ToString();
                    txtEditCustomerName.Text = grvCustomer.Rows[rowIndex].Cells[1].Value.ToString();
                    txtEditCustomerIdentity.Text = grvCustomer.Rows[rowIndex].Cells[2].Value.ToString();
                    txtEditCustomerAddress.Text = grvCustomer.Rows[rowIndex].Cells[3].Value.ToString();
                    txtEditCustomerEmail.Text = grvCustomer.Rows[rowIndex].Cells[4].Value.ToString();
                    txtEditCustomerDateRegister.Text = grvCustomer.Rows[rowIndex].Cells[5].Value.ToString();
                }
            }
        }

        private void btnCustomerEdit_Click(object sender, EventArgs e)
        {
            Customer customer = db.Customers.Find(Convert.ToInt32(lblCustomerId.Text));

            customer.CustomerName = txtEditCustomerName.Text;
            customer.CMND = txtEditCustomerIdentity.Text;
            customer.Address = txtEditCustomerAddress.Text;
            customer.Email = txtEditCustomerEmail.Text;

            if (CheckCustomerInfomations(txtEditCustomerName.Text, txtEditCustomerIdentity.Text, txtEditCustomerAddress.Text, txtEditCustomerEmail.Text, false))
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();

                MessageBox.Show("Sửa thành công");

                txtEditCustomerName.Text = "";
                txtEditCustomerIdentity.Text = "";
                txtEditCustomerAddress.Text = "";
                txtEditCustomerEmail.Text = "";
                txtEditCustomerDateRegister.Text = "";

                ClearDataGridView(grvCustomer);
                LoadCustomer(db.Customers.ToList());
            }
        }
        #endregion

        #region Phone Number
        private List<PhoneNumber> GetAllActivePhoneNumbers()
        {
            return db.PhoneNumbers.Where(p => p.Status == true).ToList();
        }

        private int GetKeyIdOfPhoneNumber(string number)
        {
            return db.PhoneNumbers.Where(p => p.PhoneNo.Contains(number)).FirstOrDefault().KeyId;
        }
        #endregion

        #region Register
        private void GetComboboxPhoneNumber(ComboBox comboBox, List<PhoneNumber> phoneNumbers)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (var phoneNumber in phoneNumbers)
            {
                comboBox.Items.Add(phoneNumber.PhoneNo);
                collection.Add(phoneNumber.PhoneNo);
            }

            comboBox.AutoCompleteCustomSource = collection;
        }

        private void txtRegisterCustomer_Change(object sender, EventArgs e)
        {
            Customer customer = GetCustomerByCMND(txtRegisterCustomer.Text);

            if (customer != null)
            {
                lblRegisterCustomerKeyId.Text = customer.KeyId.ToString();
                lblRegisterCustomerName.Text = customer.CustomerName;
                lblRegisterIdentity.Text = customer.CMND;
                lblRegisterAddress.Text = customer.Address;
                lblRegisterEmail.Text = customer.Email;
            }
            else
            {
                MessageBox.Show("Không có khách hàng có CMND này");
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            InvoiceRegister register = new InvoiceRegister();

            register.CustomerFK = Convert.ToInt32(lblRegisterCustomerKeyId.Text);
            register.PhoneNumberFK = GetKeyIdOfPhoneNumber(cbxRegisterPhoneNumber.Text);
            register.CostRegister = Convert.ToInt32(txtRegisterCost.Text);
            register.Status = true;
            register.DateRegisted = DateTime.Now;

            db.InvoiceRegisters.Add(register);

            PhoneNumber phoneNumber = new PhoneNumber();
            phoneNumber.KeyId = register.PhoneNumberFK;
            phoneNumber.Status = false;

            db.Entry(phoneNumber).State = EntityState.Modified;
            db.SaveChanges();

            MessageBox.Show("Thêm thành công");
        }
        #endregion
    }
}
