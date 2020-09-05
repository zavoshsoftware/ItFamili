using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Faq:BaseEntity
    {
        [Display(Name = "Order", ResourceType = typeof(Resources.Models.Faq))]
        public int Order { get; set; }
        [Display(Name = "Question", ResourceType = typeof(Resources.Models.Faq))]
        public string Question { get; set; }
        [Display(Name = "Answer", ResourceType = typeof(Resources.Models.Faq))]
        public string Answer { get; set; }
    }
}
