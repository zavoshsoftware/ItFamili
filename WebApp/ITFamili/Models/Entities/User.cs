
using System.Data.Entity.ModelConfiguration;
using Resources;

namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : BaseEntity
    {
        public User()
        {
            Users = new List<User>();
            ActivationCodes = new List<ActivationCode>();
            ContentComments = new List<ContentComment>();
            ContentLikes=new List<ContentLike>();
            SupportRequests =new List<SupportRequest>();
        }


        [Display(Name = "Password", ResourceType = typeof(Resources.Models.User))]
        [StringLength(150, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Password { get; set; }

        [Display(Name = "CellNum", ResourceType = typeof(Resources.Models.User))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(20, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        [RegularExpression(@"(^(09|9)[0123456789][0123456789]\d{7}$)|(^(09|9)[0123456789][0123456789]\d{7}$)", ErrorMessageResourceName = "MobilExpersionValidation", ErrorMessageResourceType = typeof(Messages))]
        public string CellNum { get; set; }

        [Display(Name = "FullName", ResourceType = typeof(Resources.Models.User))]
        [StringLength(250, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string FullName { get; set; }

        [Display(Name = "Code", ResourceType = typeof(Resources.Models.User))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public int Code { get; set; }

        public bool? IsMale { get; set; }

        public Guid RoleId { get; set; }

        [Display(Name = "Introducer", ResourceType = typeof(Resources.Models.User))]
        public Guid? ParentId { get; set; }

        public string Token { get; set; }

        [Display(Name = "RemainCredit", ResourceType = typeof(Resources.Models.User))]
        public decimal? RemainCredit { get; set; }

        [Display(Name = "DeviceType", ResourceType = typeof(Resources.Models.ActivationCode))]
        [StringLength(50, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string OsType { get; set; }

        [Display(Name = "VersionNumber", ResourceType = typeof(Resources.Models.VersionHistory))]
        public string VersionNumber { get; set; }

        public string Email { get; set; }
        public virtual User Parent { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<ActivationCode> ActivationCodes { get; set; }
        public virtual ICollection<SupportRequest> SupportRequests { get; set; }
        public virtual ICollection<ContentLike> ContentLikes { get; set; }
        public virtual ICollection<ContentComment> ContentComments { get; set; }

        internal class configuration : EntityTypeConfiguration<User>
        {
            public configuration()
            {
                HasRequired(p => p.Parent).WithMany(t => t.Users).HasForeignKey(p => p.ParentId);

                HasRequired(p => p.Role).WithMany(j => j.Users).HasForeignKey(p => p.RoleId);


            }
        }
    }
}

