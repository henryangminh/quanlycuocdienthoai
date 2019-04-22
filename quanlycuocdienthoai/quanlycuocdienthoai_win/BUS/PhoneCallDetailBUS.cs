using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlycuocdienthoai_win.BUS
{
    public class PhoneCallDetailBUS
    {
        InvoiceRegisterBUS invoiceRegisterBUS = new InvoiceRegisterBUS();
        PhoneNumberBUS phoneNumberBUS = new PhoneNumberBUS();
        InvoicePostageBUS invoicePostageBUS = new InvoicePostageBUS();

        static Random random = new Random();

        private bool CheckExistPhoneCallLog(DateTime date)
        {
            string fileName = date.Month.ToString() + date.Year.ToString() + ".txt";

            string dirRoot = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string dirOfFile = Path.Combine(dirRoot, Const.PhoneCallDirection);
            string dirToFile = Path.Combine(dirOfFile, fileName);

            if (File.Exists(dirToFile))
                return true;
            return false;
        }

        public void GenerateRandomDate(int Year, int Month, int lines)
        {
            DateTime dateStartOfMonth = new DateTime(Year, Month, 1);
            if (CheckExistPhoneCallLog(dateStartOfMonth))
            {
                MessageBox.Show($"Dữ liệu chi tiết cuộc gọi tháng {dateStartOfMonth.Month}/{dateStartOfMonth.Year} đã có");
            }
            else
            {
                List<PhoneNumber> availablePhoneNumber = new List<PhoneNumber>();
                //lấy những đơn đăng ký nào đăng ký hòa mạng trong tháng đó hoặc trước đó
                List<InvoiceRegister> invoiceRegisters = invoiceRegisterBUS.GetTheRegisterInThatMonthOrBefore(dateStartOfMonth);
                //lấy sđt từ các hóa đơn đó
                foreach (var invoiceRegister in invoiceRegisters)
                {
                    PhoneNumber phoneNumber = new PhoneNumber();
                    phoneNumber = phoneNumberBUS.GetById(invoiceRegister.PhoneNumberFK);
                    availablePhoneNumber.Add(phoneNumber);
                }
                //lấy những sđt đã đóng tiền trong tháng gần nhất
                foreach (var phoneNumber in availablePhoneNumber)
                {
                    if (!invoicePostageBUS.CheckPaidInvoice(phoneNumber))
                    {
                        availablePhoneNumber.Remove(phoneNumber);
                    }
                }
                //Tạo ngẫu nhiên và ghi file
                ImportFile(lines, dateStartOfMonth, availablePhoneNumber);
            }
        }

        private void ImportFile(int lines, DateTime dateStart, List<PhoneNumber> phoneNumbers)
        {
            DateTime dateEnd = new DateTime(dateStart.Year, dateStart.Month, DateTime.DaysInMonth(dateStart.Year, dateStart.Month), 23, 59, 59);

            string fileName = dateStart.Month.ToString() + dateStart.Year.ToString() + ".txt";

            string dirRoot = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string dirOfFile = dirRoot + Const.PhoneCallDirection;
            string dirToFile = Path.Combine(dirOfFile, fileName);

            if (!File.Exists(dirToFile))
            {
                File.Create(dirToFile).Dispose();
            }
            List<PhoneCallDetail> phoneCallDetails = RandomPhoneCallDetail(lines, phoneNumbers, dateStart, dateEnd);

            using (StreamWriter outFile = new StreamWriter(dirToFile))
            {
                foreach (var phoneCallDetail in phoneCallDetails)
                {
                    outFile.WriteLine($"{phoneCallDetail.PhoneNumberFK}\t{phoneCallDetail.TimeStart}\t{phoneCallDetail.TimeFinish}");
                }
                outFile.Close();
            }

            MessageBox.Show("Tạo thành công");
        }

        private List<PhoneCallDetail> RandomPhoneCallDetail(int lines, List<PhoneNumber> availableNumbers, DateTime dateStart, DateTime dateEnd)
        {
            List<PhoneCallDetail> phoneCallDetails = new List<PhoneCallDetail>();

            for (int i = 0; i < lines; i++)
            {
                PhoneCallDetail phoneCallDetail = new PhoneCallDetail();
                PhoneNumber phoneNumber = RandomPhoneNumber(availableNumbers);
                phoneCallDetail.KeyId = 0;
                phoneCallDetail.PhoneNumberFK = phoneNumber.KeyId;
                phoneCallDetail.TimeStart = RandomDateRange(dateStart, dateEnd);
                phoneCallDetail.TimeFinish = RandomDateEnd(phoneCallDetail.TimeStart);
                phoneCallDetail.SubTotal = 0;

                phoneCallDetails.Add(phoneCallDetail);
            }

            return phoneCallDetails.OrderBy(x => x.TimeStart).ToList();
        }

        private DateTime RandomDateRange(DateTime dateStart, DateTime dateEnd)
        {
            var duration = new TimeSpan(0, 0, 0, random.Next(86400));
            int range = ((TimeSpan)(dateEnd - dateStart)).Days;
            return dateStart.AddDays(random.Next(range)).Add(duration);
        }

        private DateTime RandomDateEnd(DateTime dateStart)
        {
            var duration = new TimeSpan(0, 0, 0, random.Next(86400));
            return dateStart.Add(duration);
        }

        private PhoneNumber RandomPhoneNumber(List<PhoneNumber> phoneNumbers)
        {
            int index = random.Next(phoneNumbers.Count);
            return phoneNumbers[index];
        }
    }
}
