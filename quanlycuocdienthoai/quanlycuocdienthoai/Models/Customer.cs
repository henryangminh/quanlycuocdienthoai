using System;
using System.Collections.Generic;

namespace quanlycuocdienthoai.Models
{
    public class Customer : DomainEntity<int>
    {
        public string CustomerName { get; set; }
        public string Indentity { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime DateRegistered { get; set; }

        public virtual ICollection<InvoiceRegister> InvoiceRegisters { get; set; }
    }
}
