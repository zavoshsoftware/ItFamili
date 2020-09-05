using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class MagzineListForARViewModel : BaseViewModel
    {
        public List<MagzineListItemForArViewModel> Result { get; set; }
    }

    public class MagzineListItemForArViewModel
    {
        public int Version { get; set; }
        public string Title { get; set; }
        public string PublishDate { get; set; }
    }
}