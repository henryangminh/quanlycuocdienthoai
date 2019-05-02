using quanlycuocdienthoai.EF;
using quanlycuocdienthoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace quanlycuocdienthoai_win.DAL
{
    public class PhoneCallDetailDAL
    {
        PostageContext db = new PostageContext();
        PhoneNumberDAL phoneNumberDAL = new PhoneNumberDAL();

        public List<PhoneCallDetail> GetAll()
        {
            return db.PhoneCallDetails
                .Include(x => x.PhoneNumberFKNavigation)
                .ToList();
        }

        public List<PhoneCallDetail> GetPhoneCallDetailByPhoneNumber(string phoneNumber)
        {
            PhoneNumber number = phoneNumberDAL.GetByPhoneNumber(phoneNumber);
            return db.PhoneCallDetails
                .Include(x => x.PhoneNumberFKNavigation)
                .Where(x => x.PhoneNumberFK == number.KeyId)
                .ToList();
        }

        public List<PhoneCallDetail> Add(List<PhoneCallDetail> phoneCallDetails)
        {
            var model = db.PhoneCallDetails.AddRange(phoneCallDetails).ToList();
            db.SaveChanges();
            return model;
        }
    }
}
