﻿using Helper;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ContentGroupListViewModel : BaseViewModelHelper
    {
        public List<Content> Contents { get; set; }
    }
}