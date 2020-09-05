using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helper;

namespace ViewModels
{
    public class MagzineDetailViewModel : BaseViewModelHelper
    {
        public Magzine Magzine { get; set; }
    }
}