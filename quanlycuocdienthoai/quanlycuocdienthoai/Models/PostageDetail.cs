using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class PostageDetail : DomainEntity<int>
    {
        public int PostageFK { get; set; }
        public TimeSpan HourMark { get; set; }
        public int Cost { get; set; }

        [ForeignKey("PostageFK")]
        public virtual Postage PostageFKNavigation { get; set; }
    }
}
