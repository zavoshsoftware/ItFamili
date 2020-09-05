using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Input
{
    public class PostCommentInputViewModel
    {
        public string ContentId { get; set; }
        public string Comment { get; set; }
    }
}