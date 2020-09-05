using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helper;

namespace ViewModels
{
    public class ContentDetailViewModel : BaseViewModelHelper
    {
        public Content Content { get; set; }
        public List<ContentComment> ContentComments { get; set; }
    }
}