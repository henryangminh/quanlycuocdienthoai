﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class InvoiceRegister : DomainEntity<int>
    {
        public int CustomerFK { get; set; }
        public int PhoneNumberFK { get; set; }
        public int CostRegister { get; set; }
        /// <summary>
        /// true: Active
        /// false: Inactive
        /// </summary>
        public bool Status { get; set; }
        public DateTime DateRegisted { get; set; }

        [ForeignKey("CustomerFK")]
        public virtual Customer CustomerFKNavigation { get; set; }
        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }
    }
}
