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
            grvRegister.CellClick += new DataGridViewCellEventHandler(BlockRegister);
            grvSim.CellClick += new DataGridViewCellEventHandler(BlockSim);
            grvAddHourMark.CellClick += new DataGridViewCellEventHandler(DeleteHourMark);
            grvAddHourMark.CellEndEdit += new DataGridViewCellEventHandler(ChangePostageCost);
            tabPostageProject.SelectedIndexChanged += new EventHandler(LoadAll);

            LoadAll();
        }

        PostageContext db = new PostageContext();

        private void LoadAll()
        {
            LoadCustomer(db.Customers.ToList());
            LoadRegister(db.InvoiceRegisters.ToList());
            LoadPhoneNumber(db.PhoneNumbers.ToList());
            LoadSim(db.SIMs.ToList());

            GetComboboxPhoneNumber(cbxRegisterPhoneNumber, true);
            GetComboboxPhoneNumber(cbxRegisterPhoneNumberSearch, false);
            GetComboboxPhoneNumber(cbxViewPhoneNumberPhoneNoSearch, false);
            GetComboboxPhoneNumber(cbxSimPhoneNumberSearch, false);
            GetComboboxPhoneNumber(cbxSimPhoneNumber, false);
            GetComboboxActiveSim(cbxSimActive, GetActiveSim());
        }

        private void LoadAll(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void ClearDataGridView(DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
        }

        private void Update(Object T)
        {
            db.Entry(T).State = EntityState.Modified;
        }

        private void TimeSpanPickerChangeValue(NumericUpDown numericUpDown)
        {
            if (numericUpDown.Value < numericUpDown.Minimum)
            {
                numericUpDown.Value = numericUpDown.Minimum;
            }

            if (numericUpDown.Value > numericUpDown.Maximum)
            {
                numericUpDown.Value = numericUpDown.Maximum;
            }
        }

        /// <summary>
        /// Kiểm tra toàn bộ ký tự trong chuỗi chỉ chứa số
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool CheckDigital(string text)
        {
            return text.All(Char.IsDigit);
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

            if (!CheckDigital(identity))
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
                var query = GetCustomerByCMND(identity);
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
            return db.Customers.Where(p => p.CMND == cmnd).FirstOrDefault();
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

                LoadCustomer(db.Customers.ToList());
            }
        }

        private void LoadCustomer(Customer customer)
        {
            if(customer!=null)
            {
                int n = grvCustomer.Rows.Add();
                grvCustomer.Rows[n].Cells[0].Value = customer.KeyId;
                grvCustomer.Rows[n].Cells[1].Value = customer.CustomerName;
                grvCustomer.Rows[n].Cells[2].Value = customer.CMND;
                grvCustomer.Rows[n].Cells[3].Value = customer.Address;
                grvCustomer.Rows[n].Cells[4].Value = customer.Email;
                grvCustomer.Rows[n].Cells[5].Value = customer.DateRegistered;
            }
        }

        private void LoadCustomer(List<Customer> customers)
        {
            ClearDataGridView(grvCustomer);

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
            if (CheckCustomerInfomations(txtEditCustomerName.Text, txtEditCustomerIdentity.Text, txtEditCustomerAddress.Text, txtEditCustomerEmail.Text, false))
            {
                Customer customer = db.Customers.Find(Convert.ToInt32(lblCustomerId.Text));

                customer.CustomerName = txtEditCustomerName.Text;
                customer.CMND = txtEditCustomerIdentity.Text;
                customer.Address = txtEditCustomerAddress.Text;
                customer.Email = txtEditCustomerEmail.Text;

                Update(customer);
                db.SaveChanges();

                MessageBox.Show("Sửa thành công");

                txtEditCustomerName.Text = "";
                txtEditCustomerIdentity.Text = "";
                txtEditCustomerAddress.Text = "";
                txtEditCustomerEmail.Text = "";
                txtEditCustomerDateRegister.Text = "";

                LoadCustomer(db.Customers.ToList());
            }
        }
        #endregion

        #region SIM
        /// <summary>
        /// Lấy sim của 1 SĐT. Kiểm tra xem SĐT đã có SIM hay chưa
        /// </summary>
        /// <param name="number">SĐT</param>
        /// <returns>Trả về SIM đã có hoặc null</returns>
        private SIM GetTheLastestSIM(string number)
        {
            int idPhoneNo = GetKeyIdOfPhoneNumber(number);
            return db.SIMs.Where(x => x.PhoneNumberFK == idPhoneNo && x.Status == true).FirstOrDefault();
        }

        private void btnAddSim_Click(object sender, EventArgs e)
        {
            decimal quantity = nudViewSimQuantity.Value;

            for (decimal i = 0; i < quantity; i++)
            {
                SIM sim = new SIM();
                sim.KeyId = 0;
                sim.PhoneNumberFK = null;

                db.SIMs.Add(sim);
            }

            db.SaveChanges();

            LoadSim(db.SIMs.ToList());
            GetComboboxActiveSim(cbxSimActive, GetActiveSim());
        }

        private void LoadSim(SIM sim)
        {
            if (sim != null)
            {
                int n = grvSim.Rows.Add();
                grvSim.Rows[n].Cells[0].Value = sim.KeyId;
                grvSim.Rows[n].Cells[1].Value = (sim.PhoneNumberFKNavigation != null) ? sim.PhoneNumberFKNavigation.PhoneNo : "";
                grvSim.Rows[n].Cells[2].Value = (sim.Status) ? "Đang sử dụng" : (sim.PhoneNumberFK != null) ? "Đã khóa" : "Chưa được sử dụng";
            }
        }

        private void LoadSim(List<SIM> sims)
        {
            ClearDataGridView(grvSim);

            foreach (var sim in sims)
            {
                LoadSim(sim);
            }
        }

        private List<SIM> GetActiveSim()
        {
            return db.SIMs.Where(x => (x.Status == false && x.PhoneNumberFK == null)).ToList();
        }

        private void GetComboboxActiveSim(ComboBox comboBox, List<SIM> sims)
        {
            comboBox.Items.Clear();

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();

            foreach (var sim in sims)
            {
                comboBox.Items.Add(sim.KeyId);
                collection.Add(sim.KeyId.ToString());
            }

            comboBox.AutoCompleteCustomSource = collection;
        }

        private bool CheckAssignSIM(string sim, string phoneNumber)
        {
            if (sim == "" || phoneNumber == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (db.SIMs.Find(Convert.ToInt32(sim)) == null || db.PhoneNumbers.Find(GetKeyIdOfPhoneNumber(phoneNumber)) == null)
            {
                MessageBox.Show("Sim hoặc SĐT đã nhập không có trong CSDL");
                return false;
            }

            if(GetTheLastestSIM(phoneNumber) != null)
            {
                MessageBox.Show("SĐT này đã có SIM. Muốn chuyển SIM, hãy khóa SIM hiện tại của SĐT này");
                return false;
            }
            return true;
        }

        private void btnViewSimAssign_Click(object sender, EventArgs e)
        {
            if (CheckAssignSIM(cbxSimActive.Text, cbxSimPhoneNumberSearch.Text))
            {
                SIM sim = db.SIMs.Find(Convert.ToInt32(cbxSimActive.Text));
                sim.PhoneNumberFK = GetKeyIdOfPhoneNumber(cbxSimPhoneNumberSearch.Text);
                sim.Status = true;

                Update(sim);
                db.SaveChanges();

                LoadSim(db.SIMs.ToList());

                cbxSimActive.SelectedIndex = -1;
                cbxSimPhoneNumberSearch.SelectedIndex = -1;

                cbxSimActive.Text = "";
                cbxSimPhoneNumberSearch.Text = "";

                GetComboboxPhoneNumber(cbxSimPhoneNumberSearch, false);
                GetComboboxActiveSim(cbxSimActive, GetActiveSim());
            }
        }

        private void BlockSim(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView != null)
            {
                if (e.ColumnIndex == 3)
                {
                    int rowIndex = e.RowIndex;

                    if (!db.SIMs.Find(Convert.ToInt32(grvSim.Rows[rowIndex].Cells[0].Value.ToString())).Status)
                    {
                        MessageBox.Show("SIM này đã được khóa hoặc chưa có SĐT");
                        return;
                    }

                    if (DialogResult.Yes == MessageBox.Show("Bạn có muốn khóa SIM này", "", MessageBoxButtons.YesNo))
                    {
                        SIM sim = db.SIMs.Find(Convert.ToInt32(grvSim.Rows[rowIndex].Cells[0].Value.ToString()));
                        sim.Status = false;

                        Update(sim);
                        db.SaveChanges();

                        LoadSim(db.SIMs.ToList());
                    }
                }
            }
        }

        private List<SIM> GetSimByPhoneNumber(string number)
        {
            return db.SIMs.Where(p => p.PhoneNumberFKNavigation.PhoneNo == number).ToList();
        }

        private void btnSimSearch_Click(object sender, EventArgs e)
        {
            var sims = GetSimByPhoneNumber(cbxSimPhoneNumber.Text);

            LoadSim(sims);
        }
        #endregion

        #region Phone Number
        private int GetKeyIdOfPhoneNumber(string number)
        {
            return GetByPhoneNumber(number).KeyId;
        }

        private PhoneNumber GetByPhoneNumber(string number)
        {
            return db.PhoneNumbers.Where(p => p.PhoneNo  == number).FirstOrDefault();
        }

        private bool CheckAddPhoneNumber(string phoneNumber)
        {
            if(phoneNumber=="")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!CheckDigital(phoneNumber))
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

        private void LoadPhoneNumber(PhoneNumber number)
        {
            if (number != null)
            {
                int n = grvPhoneNumber.Rows.Add();
                grvPhoneNumber.Rows[n].Cells[0].Value = number.KeyId;
                grvPhoneNumber.Rows[n].Cells[1].Value = number.PhoneNo;
                grvPhoneNumber.Rows[n].Cells[2].Value = (number.Status) ? "Đang được sử dụng" : "Đang trống";
            }
        }

        private void LoadPhoneNumber(List<PhoneNumber> numbers)
        {
            ClearDataGridView(grvPhoneNumber);

            foreach (var number in numbers)
            {
                LoadPhoneNumber(number);
            }
        }

        private void btnAddPhoneNumber_Click(object sender, EventArgs e)
        {
            if (CheckAddPhoneNumber(txtPhoneNumberPhoneNo.Text))
            {
                PhoneNumber phoneNumber = new PhoneNumber();
                phoneNumber.KeyId = 0;
                phoneNumber.PhoneNo = txtPhoneNumberPhoneNo.Text;
                phoneNumber.Status = false;

                db.PhoneNumbers.Add(phoneNumber);
                db.SaveChanges();

                MessageBox.Show("Thêm thành công");

                LoadPhoneNumber(db.PhoneNumbers.ToList());
                txtPhoneNumberPhoneNo.Text = "";
            }
        }

        private void btnViewPhoneNumberSearch_Click(object sender, EventArgs e)
        {
            var phoneNumber = GetByPhoneNumber(cbxViewPhoneNumberPhoneNoSearch.Text);

            ClearDataGridView(grvPhoneNumber);
            LoadPhoneNumber(phoneNumber);
        }
        #endregion

        #region Register
        /// <summary>
        /// Lấy các giá trị vào combobox
        /// </summary>
        /// <param name="comboBox">Combobox nào cần thêm giá trị</param>
        /// <param name="inactive">true: những SĐT nào chưa có ai SD, false: tất cả SĐT</param>
        private void GetComboboxPhoneNumber(ComboBox comboBox, bool inactive)
        {
            comboBox.Items.Clear();

            IQueryable<PhoneNumber> numbers = db.PhoneNumbers;

            if (inactive)
            {
                numbers = numbers.Where(x => x.Status == false);
            }

            List<PhoneNumber> phoneNumbers = numbers.ToList();

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
            if (txtRegisterCustomer.Text != "")
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

                    lblRegisterCustomerKeyId.Text = "xxx";
                    lblRegisterCustomerName.Text = "xxx";
                    lblRegisterIdentity.Text = "xxx";
                    lblRegisterAddress.Text = "xxx";
                    lblRegisterEmail.Text = "xxx";
                }
            }
        }

        private bool CheckRegisterInformations(string customerKeyId, string phoneNo, string cost)
        {
            if(customerKeyId == "xxx")
            {
                MessageBox.Show("Chưa nhập hoặc nhập sai CMND");
                return false;
            }

            if (phoneNo == "" || cost == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if(!CheckDigital(cost))
            {
                MessageBox.Show("Phí hòa mạng phải là số");
                return false;
            }

            if (GetTheLastestSIM(phoneNo) == null)
            {
                MessageBox.Show("Số điện thoại này chưa có SIM");
                return false;
            }

            if (GetByPhoneNumber(phoneNo).Status)
            {
                MessageBox.Show("Số điện thoại này đang được sử dụng");
                return false;
            }
            return true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            InvoiceRegister register = new InvoiceRegister();

            if(CheckRegisterInformations(lblRegisterCustomerKeyId.Text, cbxRegisterPhoneNumber.Text, txtRegisterCost.Text))
            {
                register.CustomerFK = Convert.ToInt32(lblRegisterCustomerKeyId.Text);
                register.PhoneNumberFK = GetKeyIdOfPhoneNumber(cbxRegisterPhoneNumber.Text);
                register.CostRegister = Convert.ToInt32(txtRegisterCost.Text);
                register.Status = true;
                register.DateRegisted = DateTime.Now;

                db.InvoiceRegisters.Add(register);

                PhoneNumber phoneNumber = db.PhoneNumbers.Find(register.PhoneNumberFK);
                phoneNumber.Status = true;

                Update(phoneNumber);
                db.SaveChanges();

                MessageBox.Show("Đăng ký thành công");
                GetComboboxPhoneNumber(cbxRegisterPhoneNumber, true);

                cbxRegisterPhoneNumber.SelectedIndex = -1;
                cbxRegisterPhoneNumber.Text = "";
                txtRegisterCost.Text = "";
                txtRegisterCustomer.Text = "";
                lblRegisterCustomerKeyId.Text = "xxx";
                lblRegisterCustomerName.Text = "xxx";
                lblRegisterIdentity.Text = "xxx";
                lblRegisterAddress.Text = "xxx";
                lblRegisterEmail.Text = "xxx";

                LoadRegister(db.InvoiceRegisters.ToList());
            }
        }

        private void LoadRegister(InvoiceRegister register)
        {
            if (register != null)
            {
                int n = grvRegister.Rows.Add();
                grvRegister.Rows[n].Cells[0].Value = register.KeyId;
                grvRegister.Rows[n].Cells[1].Value = register.CustomerFKNavigation.CustomerName;
                grvRegister.Rows[n].Cells[2].Value = register.CustomerFKNavigation.CMND;
                grvRegister.Rows[n].Cells[3].Value = register.PhoneNumberFKNavigation.PhoneNo;
                grvRegister.Rows[n].Cells[4].Value = register.DateRegisted;
                grvRegister.Rows[n].Cells[5].Value = register.CostRegister;
                grvRegister.Rows[n].Cells[6].Value = (register.Status) ? "Bình thường" : "Khóa";
            }
        }

        private void LoadRegister(List<InvoiceRegister> registers)
        {
            ClearDataGridView(grvRegister);

            foreach (var register in registers)
            {
                LoadRegister(register);
            }
        }
        
        private void BlockRegister(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = sender as DataGridView;

            if (dataGridView != null)
            {
                if (e.ColumnIndex == 7)
                {
                    int rowIndex = e.RowIndex;
                    var register = db.InvoiceRegisters.Find(Convert.ToInt32(grvRegister.Rows[rowIndex].Cells[0].Value.ToString()));

                    if(register.Status)
                    {
                        if (DialogResult.Yes == MessageBox.Show("Bạn có muốn khóa SĐT này", "", MessageBoxButtons.YesNo))
                        {
                            register.Status = false;
                            var phoneNumber = db.PhoneNumbers.Find(register.PhoneNumberFK);
                            phoneNumber.Status = false;
                            var sim = GetTheLastestSIM(phoneNumber.PhoneNo);
                            sim.Status = false;
                            Update(phoneNumber);
                            Update(sim);
                            Update(register);
                            db.SaveChanges();

                            GetComboboxPhoneNumber(cbxRegisterPhoneNumber, true);

                            LoadRegister(db.InvoiceRegisters.ToList());
                        }
                    }
                    else
                    {
                        MessageBox.Show("SĐT của khách hàng này đã khóa");
                    }
                }
            }
        }

        private void btnRegisterCustomerSearch_Click(object sender, EventArgs e)
        {
            IQueryable<InvoiceRegister> registers = db.InvoiceRegisters;

            if (txtRegisterCustomerSearch.Text != "")
            {
                int customerId = GetCustomerByCMND(txtRegisterCustomerSearch.Text).KeyId;
                registers = registers.Where(x => x.CustomerFK == customerId);
            }
            if (cbxRegisterPhoneNumberSearch.Text != "")
            {
                int numberId = GetByPhoneNumber(cbxRegisterPhoneNumberSearch.Text).KeyId;
                registers = registers.Where(x => x.PhoneNumberFK == numberId);
            }

            LoadRegister(registers.ToList());
        }
        #endregion

        #region Postage

        public static List<PostageDetail> HourMarks = new List<PostageDetail>();

        private void DeleteHourMark(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                int rowIndex = e.RowIndex;
                HourMarks.RemoveAt(rowIndex);

                LoadAddHourMarks();
            }
        }

        private void nudHour_ValueChanged(object sender, EventArgs e)
        {
            TimeSpanPickerChangeValue(nudHour);
        }

        private void nudMinute_ValueChanged(object sender, EventArgs e)
        {
            TimeSpanPickerChangeValue(nudMinute);
        }

        private void nudSecond_ValueChanged(object sender, EventArgs e)
        {
            TimeSpanPickerChangeValue(nudSecond);
        }

        private void LoadAddHourMarks()
        {
            ClearDataGridView(grvAddHourMark);

            if (HourMarks.Count() > 1)
            {
                for (int i = 0; i < HourMarks.Count - 1; i++)
                {
                    int n = grvAddHourMark.Rows.Add();
                    grvAddHourMark.Rows[n].Cells[0].Value = HourMarks[i].HourMark.ToString();
                    grvAddHourMark.Rows[n].Cells[1].Value = HourMarks[i + 1].HourMark.ToString();
                    grvAddHourMark.Rows[n].Cells[2].Value = HourMarks[i].Cost.ToString();
                }
            }
            int m = grvAddHourMark.Rows.Add();
            grvAddHourMark.Rows[m].Cells[0].Value = HourMarks[HourMarks.Count - 1].HourMark.ToString();
            grvAddHourMark.Rows[m].Cells[1].Value = HourMarks[0].HourMark.ToString();
            grvAddHourMark.Rows[m].Cells[2].Value = HourMarks[HourMarks.Count - 1].Cost.ToString();
        }

        private bool CheckAddPostageDetails(string cost)
        {
            if (cost == "")
            {
                MessageBox.Show("Không được để trống");
                return false;
            }

            if (!CheckDigital(cost))
            {
                MessageBox.Show("Cước phí phải là số");
                return false;
            }
            return true;
        }

        private bool CheckAddPostageDetails(string cost, TimeSpan timeSpan)
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

        private void ChangePostageCost(object sender, DataGridViewCellEventArgs e)
        {
            ChangePostageCost(grvAddHourMark, e.RowIndex);
        }

        /// <summary>
        /// Đổi giá cước khi đang thêm
        /// </summary>
        /// <param name="dataGridView">DataGridView</param>
        /// <param name="n">phần tử thứ n</param>
        private void ChangePostageCost(DataGridView dataGridView, int n)
        {
            if (CheckAddPostageDetails(dataGridView.Rows[n].Cells[2].Value.ToString()))
            {
                HourMarks[n].Cost = Convert.ToInt32(dataGridView.Rows[n].Cells[2].Value.ToString());
            }
        }

        private void btnAddHourMark_Click(object sender, EventArgs e)
        {
            TimeSpan HourMark = new TimeSpan((int)nudHour.Value, (int)nudMinute.Value, (int)nudSecond.Value);
            if (CheckAddPostageDetails(txtPostageCost.Text, HourMark))
            {
                int n = db.Postages.ToList().Count + 1;
                PostageDetail postageDetail = new PostageDetail();
                postageDetail.PostageFK = n;
                postageDetail.HourMark = HourMark;
                postageDetail.Cost = Convert.ToInt32(txtPostageCost.Text);

                HourMarks.Add(postageDetail);
                HourMarks = HourMarks.OrderBy(p => p.HourMark).ToList();

                nudHour.Value = nudHour.Minimum;
                nudMinute.Value = nudMinute.Minimum;
                nudSecond.Value = nudSecond.Minimum;
                txtPostageCost.Text = "";

                LoadAddHourMarks();
            }
        }
        #endregion
    }
}
