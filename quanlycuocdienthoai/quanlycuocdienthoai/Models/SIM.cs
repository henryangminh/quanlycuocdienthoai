using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace quanlycuocdienthoai.Models
{
    public class SIM : DomainEntity<int>
    {
        public int? PhoneNumberFK { get; set; }
        /// <summary>
        /// Tình trạng đang sử dụng của SIM
        /// true: đang sử dụng
        /// false: đã khóa hoặc đang không có SĐT
        /// </summary>
        [DefaultValue(false)]
        public bool Status { get; set; }

        [ForeignKey("PhoneNumberFK")]
        public virtual PhoneNumber PhoneNumberFKNavigation { get; set; }
    }
}
