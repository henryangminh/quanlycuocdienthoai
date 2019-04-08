using System.Collections.Generic;
using System.ComponentModel;

namespace quanlycuocdienthoai.Models
{
    public class PhoneNumber: DomainEntity<int>
    {
        public string PhoneNo { get; set; }
        /// <summary>
        /// true: Active (cannot use because this phone number is used)
        /// false: Inactive
        /// </summary>
        [DefaultValue(false)]
        public bool Status { get; set; }

        public virtual ICollection<SIM> SIMs { get; set; }
    }
}
