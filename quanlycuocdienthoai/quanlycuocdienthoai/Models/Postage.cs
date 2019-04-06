using System;
using System.Collections.Generic;

namespace quanlycuocdienthoai.Models
{
    public class Postage : DomainEntity<int>
    {
        public DateTime DateApplied { get; set; }

        public virtual ICollection<PostageDetail> PostageDetails { get; set; }
    }
}
