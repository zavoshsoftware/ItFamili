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
    public class Magzine : BaseEntity
    {
        public Magzine()
        {
            ArAssets = new List<ArAsset>();
        }
        [Display(Name = "Title", ResourceType = typeof(Resources.Models.Magzine))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string Title { get; set; }

        [Display(Name = "PublishDate", ResourceType = typeof(Resources.Models.Magzine))]
        public DateTime? PublishDate { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources.Models.Magzine))]
        public decimal? Amount { get; set; }

        [Display(Name = "FileUrl", ResourceType = typeof(Resources.Models.Magzine))]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string FileUrl { get; set; }

        [Display(Name = "ImageUrl", ResourceType = typeof(Resources.Models.Magzine))]
        [StringLength(500, ErrorMessage = "طول {0} نباید بیشتر از {1} باشد")]
        public string ImageUrl { get; set; }

        [Display(Name = "UrlParam", ResourceType = typeof(Resources.Models.Magzine))]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید.")]
        public string UrlParam { get; set; }

        public int LikeCount { get; set; }
        public int CommentCount { get; set; }

        [DataType(DataType.MultilineText)]
        [Column(TypeName = "ntext")]
        public string Summery { get; set; }

        [Display(Name = "BodySite", ResourceType = typeof(Resources.Models.Content))]
        [AllowHtml]
        [DataType(DataType.Html)]
        [Column(TypeName = "ntext")]
        [UIHint("RichText")]
        public string SummerySite { get; set; }

        public int? Version { get; set; }

        public virtual ICollection<ArAsset> ArAssets { get; set; }
    }
}
