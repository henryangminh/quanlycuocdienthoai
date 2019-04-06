using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class InvoicePostage : DomainEntity<int>
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int PhoneNumberFK { get; set; }
        public int Total { get; set; }
        [DefaultValue(false)]
        public bool PaidPostage { get; set; }

        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }
    }
}
