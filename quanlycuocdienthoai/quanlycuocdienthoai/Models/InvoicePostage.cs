using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class InvoicePostage : DomainEntity<int>
    {
        /// <summary>
        /// Kỳ thanh toán, chỉ lấy tháng và năm. Ngày set bằng 1
        /// </summary>
        public DateTime PaymentPeriod { get; set; }
        public int PhoneNumberFK { get; set; }
        public int Total { get; set; }
        [DefaultValue(false)]
        public bool PaidPostage { get; set; }

        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }
    }
}
