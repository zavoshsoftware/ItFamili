using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Input
{
    public class GetCommentInputViewModel
    {
        public string ContentId { get; set; }
        public int PageId { get; set; }
    }
}