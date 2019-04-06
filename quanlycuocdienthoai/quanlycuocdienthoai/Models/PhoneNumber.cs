using System.Collections.Generic;
using System.ComponentModel;

namespace quanlycuocdienthoai.Models
{
    public class PhoneNumber: DomainEntity<int>
    {
        public string PhoneNo { get; set; }
        [DefaultValue(false)]
        public bool Status { get; set; }

        public virtual ICollection<SIM> SIMs { get; set; }
    }
}
