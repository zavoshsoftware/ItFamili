using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class FaqViewModel:BaseViewModel
    {
        public List<FaqItemViewModel> Result { get; set; }
    }

    public class FaqItemViewModel
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}