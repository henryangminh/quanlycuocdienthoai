using quanlycuocdienthoai.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace quanlycuocdienthoai.DAL
{
    public class PostageContext : DbContext
    {
        public PostageContext() : base("PostageContext")
        { }

        #region Create DbSet
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<InvoicePostage> InvoicePostages { get; set; }
        public virtual DbSet<InvoiceRegister> InvoiceRegisters { get; set; }
        public virtual DbSet<PhoneCallDetail> PhoneCallDetails { get; set; }
        public virtual DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public virtual DbSet<Postage> Postages { get; set; }
        public virtual DbSet<PostageDetail> PostageDetails { get; set; }
        public virtual DbSet<SIM> SIMs { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
