using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;

namespace API.Models
{
    public class HomeViewModel:BaseViewModel
    {
        public HomeViewModelItem Result { get; set; }
    }

    public class HomeViewModelItem
    {
        public List<ContentInHome> SliderContents { get; set; }
        public ContentInHome Video { get; set; }
        public List<ContentInHome> Podcasts { get; set; }
        public ContentInHome BlogContent { get; set; }
        public List<MagzineInHome> Magzines { get; set; }
    }

    public class ContentInHome
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }

        public string Summery { get; set; }
        public string LinkeCount { get; set; }
        public string Body { get; set; }
        public string LinkAddress { get; set; }
        public string PublishDate { get; set; }
        public string ContentSource { get; set; }
        public string CommentCount { get; set; }
        public bool IsLike { get; set; }
    }

    public class MagzineInHome
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