using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
using Helper;

namespace ViewModels
{
    public class MagzineListViewModel : BaseViewModelHelper
    {
        public List<Magzine> Magzines { get; set; }
    }
}