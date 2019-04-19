using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class InvoicePostage : DomainEntity<int>
    {
        public int PeriodFK { get; set; }
        public int PhoneNumberFK { get; set; }
        public double Total { get; set; }
        [DefaultValue(false)]
        public bool PaidPostage { get; set; }

        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }

        [ForeignKey("PaymentPeriodFK")]
        public virtual Period PeriodFKNavigation { get; set; }
    }
}
