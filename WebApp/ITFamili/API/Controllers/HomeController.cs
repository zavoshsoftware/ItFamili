using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Configuration;
using System.Web.Http;
using Helper;
using Models;
using Models.Input;

namespace API.Controllers
{
    public class HomeController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();



        [Authorize]
        [Route("home/get")]
        [HttpPost]
        public HomeViewModel Get(HomeInputViewModel input)
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);
                if (user.IsActive == false)
                {
                    homeViewModel.Result = null;
                    homeViewModel.Status = status.ReturnStatus(16, Resources.Messages.InvalidUser, false);
                    return homeViewModel;
                }

                if (!(String.IsNullOrEmpty(input.Version))) 
                {
                    user.VersionNumber = input.Version;
                }
                if (!(String.IsNullOrEmpty(input.OsType)))
                {
                    user.OsType = input.OsType;
                }

                if (!(String.IsNullOrEmpty(input.Version)) || !(String.IsNullOrEmpty(input.OsType)))
                {
                    UnitOfWork.UserRepository.Update(user);
                    UnitOfWork.Save();
                }

                homeViewModel = GetHomeViewModel(user.Id);
                //UpdateCommentCounts();
            }
            catch (Exception e)
            {
                return new HomeViewModel()
                {
                    Result = null,
                    Status = status.ReturnStatus(100, "خطا در بازیابی اطلاعات", false)
                };
            }
            return homeViewModel;
        }

      
        public HomeViewModel GetHomeViewModel(Guid userId)
        {
            return new HomeViewModel()
            {
                Result = GetHomeResult(userId),
                Status = status.ReturnStatus(0, Resources.Messages.SuccessGet, true)
            };

        }

        public HomeViewModelItem GetHomeResult(Guid userId)
        {
            HomeViewModelItem result = new HomeViewModelItem()
            {
                Magzines = GetHomeMagzine(),
                SliderContents = GetSliderContent("blog", userId),
                Video = GetHomeContent("video", userId).FirstOrDefault(),
                Podcasts = GetHomeContent("podcast", userId).Take(2).ToList(),
                BlogContent = GetHomeContent("blog", userId).FirstOrDefault(),

            };

            return result;
        }

        readonly string _filesBaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["filesBaseUrl"];

        public List<MagzineInHome> GetHomeMagzine()
        {

            List<Magzine> magzines = UnitOfWork.MagzineRepository.Get()
                .OrderByDescending(current => current.CreationDate).Take(4).ToList();

            List<MagzineInHome> homeMagzines = new List<MagzineInHome>();

            foreach (Magzine magzine in magzines)
            {
                homeMagzines.Add(new MagzineInHome()
                {
                    Id=magzine.Id.ToString(),
                    Image = _filesBaseUrl+ magzine.ImageUrl,
                    Title = magzine.Title,
                    Summery = magzine.Summery,
                    Body = magzine.Summery,
                    CommentCount = magzine.CommentCount.ToString(),
                    LinkeCount = magzine.LikeCount.ToString(),
                    LinkAddress = _filesBaseUrl + magzine.FileUrl,
                    ContentSource = "مجله تخصصی آی تی",
                    PublishDate = "10/12/1398"
                });
            }
            return homeMagzines;
        }

        public List<ContentInHome> GetHomeContent(string type,Guid userId)
        {
            List<ContentInHome> homeContents = new List<ContentInHome>();

            List<Content> contents = UnitOfWork.ContentRepository
                .Get(current => current.ContentType.UrlParam == type).ToList();

            foreach (Content content in contents)
            {
                homeContents.Add(new ContentInHome()
                {
                    Id = content.Id.ToString(),
                    Image = _filesBaseUrl+ content.ImageUrl,
                    Title = content.Title,
                    LinkeCount = content.LikeCount.ToString(),
                    Summery = content.Summery,
                    Body = content.Body,
                    CommentCount = content.CommentCount.ToString(),
                    LinkAddress = _filesBaseUrl + content.FileUrl,
                    ContentSource = "مجله تخصصی آی تی",
                    PublishDate = "10/12/1398",
                    IsLike = IsLikeContent(content.Id,userId)
                });
            }

            return homeContents;
        }

        public bool IsLikeContent(Guid contentId, Guid userId)
        {
            ContentLike contentLike = UnitOfWork.ContentLikeRepository
                .Get(current => current.UserId == userId && current.ContentId == contentId).FirstOrDefault();

            if (contentLike == null)
                return false;

            return contentLike.IsLike;
        }
        public List<ContentInHome> GetSliderContent(string type,Guid userId)
        {
            List<ContentInHome> homeContents = new List<ContentInHome>();

            List<Content> contents = UnitOfWork.ContentRepository
                .Get(current => current.ContentType.UrlParam == type&&current.IsInSlider).ToList();

            foreach (Content content in contents)
            {
                homeContents.Add(new ContentInHome()
                {
                    Id = content.Id.ToString(),
                    Image = _filesBaseUrl+content.ImageUrl,
                    Title = content.Title,
                    Summery = content.Summery,
                    LinkeCount = content.LikeCount.ToString(),
                    Body = content.Body,
                    LinkAddress = _filesBaseUrl + content.FileUrl,
                    CommentCount = content.CommentCount.ToString(),
                    ContentSource = "مجله تخصصی آی تی",
                    PublishDate = "10/12/1398", 
                    IsLike = IsLikeContent(content.Id, userId)
                });
            }

            return homeContents;
        }
        public string GetRequestHeader()
        {
            var re = Request;
            var headers = re.Headers;

            if (headers.Contains("Authorization"))
            {
                string token = headers.GetValues("Authorization").First();
                return token;
            }
            return String.Empty;
        }
    }
}
