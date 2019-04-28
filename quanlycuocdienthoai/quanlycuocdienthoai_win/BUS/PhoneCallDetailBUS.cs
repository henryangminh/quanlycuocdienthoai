using quanlycuocdienthoai.Models;
using quanlycuocdienthoai_win.DAL;
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
        PhoneCallDetailDAL phoneCallDetailDAL = new PhoneCallDetailDAL();
        InvoiceRegisterBUS invoiceRegisterBUS = new InvoiceRegisterBUS();
        PhoneNumberBUS phoneNumberBUS = new PhoneNumberBUS();
        InvoicePostageBUS invoicePostageBUS = new InvoicePostageBUS();
        PostageBUS postageBUS = new PostageBUS();
        PostageDetailBUS postageDetailBUS = new PostageDetailBUS();
        PeriodBUS periodBUS = new PeriodBUS();

        static Random random = new Random();

        public bool CheckExistPhoneCallLog(DateTime date)
        {
            string fileName = date.Month.ToString() + date.Year.ToString() + ".txt";

            string dirRoot = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            string dirOfFile = dirRoot + Const.PhoneCallDirection;
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
                List<PhoneNumber> availablePhoneNumber = phoneNumberBUS.GetAvailablePhoneNumber(dateStartOfMonth);
                //Tạo ngẫu nhiên và ghi file
                ImportFile(lines, dateStartOfMonth, availablePhoneNumber);
            }
        }

        public void ImportFile(int lines, DateTime dateStart, List<PhoneNumber> phoneNumbers)
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

        public List<PhoneCallDetail> RandomPhoneCallDetail(int lines, List<PhoneNumber> availableNumbers, DateTime dateStart, DateTime dateEnd)
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

        public DateTime RandomDateRange(DateTime dateStart, DateTime dateEnd)
        {
            var duration = new TimeSpan(0, 0, 0, random.Next(86400));
            int range = ((TimeSpan)(dateEnd - dateStart)).Days;
            return dateStart.AddDays(random.Next(range)).Add(duration);
        }

        public DateTime RandomDateEnd(DateTime dateStart)
        {
            var duration = new TimeSpan(0, 0, 0, random.Next(86400));
            return dateStart.Add(duration);
        }

        public PhoneNumber RandomPhoneNumber(List<PhoneNumber> phoneNumbers)
        {
            int index = random.Next(phoneNumbers.Count);
            return phoneNumbers[index];
        }

        public PostageDetail GetHourMark(List<PostageDetail> postageDetails, TimeSpan time)
        {
            //if (time < postageDetails[0].HourMark) return postageDetails[0];
            for (int i = 0; i < postageDetails.Count - 1; i++)
            {
                if (time > postageDetails[i].HourMark && time < postageDetails[i + 1].HourMark)
                {
                    return postageDetails[i];
                }
            }

            return postageDetails[postageDetails.Count - 1];
        }

        public double ChargeTimeRange(TimeSpan start, TimeSpan end, int cost)
        {
            if (start < end)
            {
                return (end.Subtract(start).TotalSeconds / 60) * cost;
            }
            return (new TimeSpan(23, 59, 59).Subtract(start).TotalSeconds / 60) * cost + ((end.Subtract(new TimeSpan(0, 0, 0)).TotalSeconds) / 60) * cost;
        }


        public double ChargeTimeRange(TimeSpan start, TimeSpan end)
        {
            List<PostageDetail> postageDetails = postageDetailBUS.GetByPostageId(postageBUS.GetTheLastPostage().KeyId);
            PostageDetail HourMarkStart = GetHourMark(postageDetails, start);
            PostageDetail HourMarkEnd = GetHourMark(postageDetails, end);

            if (start < end)
            {
                if (HourMarkStart.KeyId == HourMarkEnd.KeyId)
                {
                    return ChargeTimeRange(start, end, HourMarkStart.Cost);
                }
                double charge = 0;
                int index;
                PostageDetail NextMarkStart = ((index = postageDetails.IndexOf(HourMarkStart) + 1) < postageDetails.Count) ? postageDetails[index] : postageDetails[0];
                charge += ChargeTimeRange(start, NextMarkStart.HourMark, HourMarkStart.Cost);
                for (int i = NextMarkStart.KeyId; i < HourMarkEnd.KeyId; i++)
                {
                    charge += ChargeTimeRange(postageDetails[i].HourMark, postageDetails[i + 1].HourMark, postageDetails[i].Cost);
                }
                charge += ChargeTimeRange(HourMarkEnd.HourMark, end, HourMarkEnd.Cost);
                return charge;
            }
            return ChargeFullDay(postageDetails) - ChargeTimeRange(end, start);
        }

        public double ChargeFullDay(List<PostageDetail> postageDetails)
        {
            double charge = 0;
            for (int i = 0; i < postageDetails.Count - 1; i++)
            {
                charge += ChargeTimeRange(postageDetails[i].HourMark, postageDetails[i + 1].HourMark, postageDetails[i].Cost);
            }
            charge += ChargeTimeRange(postageDetails[postageDetails.Count - 1].HourMark, postageDetails[0].HourMark, postageDetails[postageDetails.Count - 1].Cost);
            return charge;
        }

        public double ChargePhoneCallDetail(DateTime start, DateTime end)
        {
            double charge = 0;
            List<PostageDetail> postageDetails = postageDetailBUS.GetByPostageId(postageBUS.GetTheLastPostage().KeyId);
            if (end.Subtract(start).TotalDays > 1)
            {
                charge += end.Subtract(start).TotalDays * ChargeFullDay(postageDetails);
            }
            charge += ChargeTimeRange(start.TimeOfDay, end.TimeOfDay);
            return charge;
        }

        public void CalculatePhoneCallDetails(int Year, int Month)
        {
            DateTime dateStartOfMonth = new DateTime(Year, Month, 1);
            if (postageBUS.GetTheLastPostage() == null)
            {
                MessageBox.Show("Không có dữ liệu tính giá cước");
            }
            if (periodBUS.GetByDate(dateStartOfMonth) != null)
            {
                MessageBox.Show($"Dữ liệu cước tháng {dateStartOfMonth.Month}/{dateStartOfMonth.Year} đã được tính");
            }
            else if (!CheckExistPhoneCallLog(dateStartOfMonth))
            {
                MessageBox.Show($"Dữ liệu chi tiết cuộc gọi tháng {dateStartOfMonth.Month}/{dateStartOfMonth.Year} chưa có");
            }
            else
            {
                string fileName = Month.ToString() + Year.ToString() + ".txt";

                string dirRoot = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
                string dirOfFile = dirRoot + Const.PhoneCallDirection;
                string dirToFile = Path.Combine(dirOfFile, fileName);

                string message="";
                List<PhoneNumber> availablePhoneNumber = phoneNumberBUS.GetAvailablePhoneNumber(dateStartOfMonth);
                List<PhoneCallDetail> phoneCallDetails = new List<PhoneCallDetail>();

                using (StreamReader sr = new StreamReader(dirToFile))
                {
                    string line;
                    int lineNo = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var data = line.Split('\t');
                        DateTime Start = new DateTime();
                        DateTime End = new DateTime();
                        if (!(DateTime.TryParse(data[1], out Start) && DateTime.TryParse(data[2], out End)))
                        {
                            message += $"Dòng thứ {lineNo} có lỗi: Ngày tháng bắt đầu hoặc kết thúc bị sai\n";
                        }
                        else if (Start > End)
                        {
                            message += $"Dòng thứ {lineNo} có lỗi: Ngày bắt đầu sau ngày kết thúc\n";
                        }
                        else if (!availablePhoneNumber.Any(x => x.KeyId == Convert.ToInt32(data[0])))
                        {
                            message += $"Dòng thứ {lineNo} có lỗi: SĐT chưa đóng tiền hoặc điện thoại này không có hiệu lực\n";
                        }
                        else
                        {
                            PhoneCallDetail phoneCallDetail = new PhoneCallDetail();
                            phoneCallDetail.PhoneNumberFK = Convert.ToInt32(data[0]);
                            phoneCallDetail.TimeStart = Start;
                            phoneCallDetail.TimeFinish = End;
                            phoneCallDetail.SubTotal = ChargePhoneCallDetail(Start, End);
                            phoneCallDetails.Add(phoneCallDetail);

                            lineNo++;
                        }
                    }
                }

                Period period = new Period();
                period.PeriodPayment = dateStartOfMonth;
                Period periodRs = periodBUS.SaveEntities(period);

                phoneCallDetailDAL.Add(phoneCallDetails);

                List<InvoicePostage> result = phoneCallDetails
                    .GroupBy(c => c.PhoneNumberFK)
                    .Select(cl => new InvoicePostage
                    {
                        PeriodFK = periodRs.KeyId,
                        PhoneNumberFK = cl.First().PhoneNumberFK,
                        Total = cl.Sum(c => c.SubTotal),
                        PaidPostage = false,
                    }).ToList();

                invoicePostageBUS.SaveEntities(result);

                MessageBox.Show($"Tính cước tháng {dateStartOfMonth.Month}/{dateStartOfMonth.Year} thành công");
            }
        }
    }
}
