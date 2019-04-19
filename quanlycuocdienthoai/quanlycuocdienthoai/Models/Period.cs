using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace quanlycuocdienthoai.Models
{
    public class Period : DomainEntity<int>
    {
        public DateTime PeriodPayment { get; set; }

        public virtual ICollection<InvoicePostage> InvoicePostages { get; set; }
    }
}