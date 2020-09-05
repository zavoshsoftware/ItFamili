using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class ArAssetsViewModel : BaseViewModel
    {
        public List<ArAssetItemViewModel> Result { get; set; }
    }

    public class ArAssetItemViewModel
    {
        public string Id { get; set; }
        public string InputImageUrl { get; set; }
        public string InputImageSize { get; set; }
        public string OutPutFileUrl { get; set; }
        public string OutPutType { get; set; }
    }
}