using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ContentGroupListViewModel : BaseViewModel
    {
        public List<ContentGroupItemListViewModel> Result { get; set; }
    }

    public class ContentGroupItemListViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }
}