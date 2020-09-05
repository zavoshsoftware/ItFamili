using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ArAsset:BaseEntity
    {
        [Display(Name = "InputImageUrl", ResourceType = typeof(Resources.Models.ArAsset))]
        public string InputImageUrl { get; set; }
        [Display(Name = "InputSize", ResourceType = typeof(Resources.Models.ArAsset))]
        public string InputSize { get; set; }
        [Display(Name = "OutPutType", ResourceType = typeof(Resources.Models.ArAsset))]
        public string OutPutType { get; set; }
        [Display(Name = "OutputFileUrl", ResourceType = typeof(Resources.Models.ArAsset))]
        public string OutputFileUrl { get; set; }
        [Display(Name = "MagzineId", ResourceType = typeof(Resources.Models.ArAsset))]
        public Guid MagzineId { get; set; }
        public virtual Magzine Magzine { get; set; }

    }
}
