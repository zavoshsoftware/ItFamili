using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helper;

namespace ViewModels
{
    public class ContentListViewModel : BaseViewModelHelper
    {
        public List<ContentItemViewModel> Contents { get; set; }
    }

    public class ContentItemViewModel
    {
        public Content Content  { get; set; }
        public int CommentCount { get; set; }
        public int LikeCount { get; set; }
    }
}