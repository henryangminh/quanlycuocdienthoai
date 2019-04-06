using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class SIM : DomainEntity<int>
    {
        public int? PhoneNumberFK { get; set; }
        [DefaultValue(false)]
        public bool Status { get; set; }

        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }
    }
}
