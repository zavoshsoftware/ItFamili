using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContentLike:BaseEntity
    {
        public Guid ContentId { get; set; }
        public virtual Content Content { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public bool IsLike { get; set; }

        internal class configuration : EntityTypeConfiguration<ContentLike>
        {
            public configuration()
            {
                HasRequired(p => p.Content).WithMany(t => t.ContentLikes).HasForeignKey(p => p.ContentId);

                HasRequired(p => p.User).WithMany(j => j.ContentLikes).HasForeignKey(p => p.UserId);
            }
        }
    }
}
