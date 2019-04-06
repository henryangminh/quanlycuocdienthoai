using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class PhoneCallDetail : DomainEntity<int>
    {
        public int PhoneNumberFK { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeFinish { get; set; }
        public int SubTotal { get; set; }

        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }
    }
}
