﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class MagzineListViewModel : BaseViewModel
    {
        public List<MagzineListItemViewModel> Result { get; set; }
    }

    public class MagzineListItemViewModel
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string PublishDate { get; set; }
        public string Body { get; set; }
        public string Summery { get; set; }
        public string LinkeCount { get; set; }
        public string LinkAddress { get; set; }
        public string ContentSource { get; set; }
        public string CommentCount { get; set; }
    }


}