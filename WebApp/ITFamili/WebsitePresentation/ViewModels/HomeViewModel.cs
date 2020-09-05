using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helper;

namespace ViewModels
{
    public class HomeViewModel: BaseViewModelHelper
    {
        public Magzine LatestMagzine { get; set; }
        public List<Content> TopContents { get; set; }
        public List<Content> LatestBlogs { get; set; }
        public List<Magzine> LateMagzines { get; set; }

    }
}