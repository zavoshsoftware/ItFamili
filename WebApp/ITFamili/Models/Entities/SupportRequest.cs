using System;
using System.Data.Entity.ModelConfiguration;

namespace Models
{
    public class SupportRequest:BaseEntity
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        internal class configuration : EntityTypeConfiguration<SupportRequest>
        {
            public configuration()
            {
                HasRequired(p => p.User).WithMany(t => t.SupportRequests).HasForeignKey(p => p.UserId);
            }
        }
    }
}
