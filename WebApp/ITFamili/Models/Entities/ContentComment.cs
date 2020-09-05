using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ContentComment : BaseEntity
    {
        public Guid ContentId { get; set; }
        public virtual Content Content { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public string Comment { get; set; }

        internal class configuration : EntityTypeConfiguration<ContentComment>
        {
            public configuration()
            {
                HasRequired(p => p.Content).WithMany(t => t.ContentComments).HasForeignKey(p => p.ContentId);

                HasRequired(p => p.User).WithMany(j => j.ContentComments).HasForeignKey(p => p.UserId);
            }
        }
    }
}
