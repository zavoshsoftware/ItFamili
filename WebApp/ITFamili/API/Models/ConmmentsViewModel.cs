using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ConmmentsViewModel : BaseViewModel
    {
        public List<CommentItemViewModel> Result { get; set; }
    }

    public class CommentItemViewModel
    {
        public string UserFullName { get; set; }
        public string Comment { get; set; }
        public string CommentDate { get; set; }
    }
}