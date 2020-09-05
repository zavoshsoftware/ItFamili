using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Input
{
    public class PostLikeInputViewModel
    {
        public string ContentId { get; set; }
        public bool IsLike { get; set; }
    }
}