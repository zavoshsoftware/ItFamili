using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Models
{
    public class Content : BaseEntity
    {
        public Content()
        {
            ContentLikes=new List<ContentLike>();
            ContentComments = new List<ContentComment>();
        }

        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Content))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "UrlParam", ResourceType = typeof(Resources.Models.Content))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UrlParam { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Content))]
        public string ImageUrl { get; set; }

        [Display(Name = "Summery", ResourceType = typeof(Resources.Models.Content))]
        [DataType(DataType.MultilineText)]
        public string Summery { get; set; }

        [Display(Name = "Body", ResourceType = typeof(Resources.Models.Content))]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        public string Body { get; set; }


        [Display(Name = "BodySite", ResourceType = typeof(Resources.Models.Content))]
        [AllowHtml]
        [DataType(DataType.Html)]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string BodySite { get; set; }

        [Display(Name = "FileUrl", ResourceType = typeof(Resources.Models.Content))]
        public string FileUrl { get; set; }

        [Display(Name = "PageTitleTag", ResourceType = typeof(Resources.Models.Content))]
        public string PageTitleTag { get; set; }

        [Display(Name = "PageMetaDescription", ResourceType = typeof(Resources.Models.Content))]
        public string PageMetaDescription { get; set; }

        [Display(Name = "ContentTypeId", ResourceType = typeof(Resources.Models.Content))]
        public Guid ContentTypeId { get; set; }
        public virtual ContentType ContentType { get; set; }

        [Display(Name = "IsInSlider", ResourceType = typeof(Resources.Models.Content))]
        public bool IsInSlider { get; set; }
        [Display(Name = "IsInHome", ResourceType = typeof(Resources.Models.Content))]
        public bool? IsInHome { get; set; }

        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        public virtual ICollection<ContentLike> ContentLikes { get; set; }
        public virtual ICollection<ContentComment> ContentComments { get; set; }
        public Guid? ContentGroupId { get; set; }
        public ContentGroup ContentGroup { get; set; }
    }
}
