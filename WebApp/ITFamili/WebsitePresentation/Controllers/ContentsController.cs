using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace WebsitePresentation.Controllers
{
    public class ContentsController : Infrastructure.BaseControllerWithUnitOfWork
    {
        [Route("content/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            ContentType contentType = UnitOfWork.ContentTypeRepository
                .Get(c => c.UrlParam == urlParam && c.IsDeleted == false).FirstOrDefault();

            if (contentType == null)
                return Redirect("/");

            ViewBag.Title = contentType.Title;
            ContentListViewModel contentList = new ContentListViewModel()
            {
                Contents = GetContents(urlParam)
            };
            return View(contentList);
        }

        public List<ContentItemViewModel> GetContents(string urlParam)
        {
            List<ContentItemViewModel> contentItems = new List<ContentItemViewModel>();

            List<Content> contents = UnitOfWork.ContentRepository
                .Get(c => c.ContentType.UrlParam == urlParam && c.IsActive).ToList();

            foreach (Content content in contents)
            {
                contentItems.Add(new ContentItemViewModel()
                {
                    Content = content,
                    CommentCount = UnitOfWork.ContentCommentRepository.Get(c => c.ContentId == content.Id && c.IsActive && c.IsDeleted == false).Count(),
                    LikeCount = content.LikeCount
                });
            }

            return contentItems;
        }

        [Route("content/{contentTypeUrlParam}/{urlParam}")]
        public ActionResult Details(string contentTypeUrlParam, string urlParam)
        {
            Content contentObject = UnitOfWork.ContentRepository.Get(c => c.UrlParam == urlParam && c.IsActive)
                .FirstOrDefault();
            ViewBag.Title = contentObject.Title;
            ContentDetailViewModel content = new ContentDetailViewModel()
            {
                Content = contentObject,
                ContentComments = UnitOfWork.ContentCommentRepository.Get(c =>
                        c.ContentId == contentObject.Id && c.IsActive == true && c.IsDeleted == false)
                    .OrderByDescending(c => c.CreationDate).ToList()
            };

            if (User.Identity.IsAuthenticated)
            {
                ViewBag.IsAuthenticate = "true";

                var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;
                Guid userId = new Guid(identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);

                Models.User user = UnitOfWork.UserRepository.GetById(userId);
                if (user != null)
                    if (user.FullName != null)
                        ViewBag.Name = user.FullName;

                    else
                        ViewBag.Name = "کاربر";

                else
                    ViewBag.Name = "کاربر";

            }
            else
                ViewBag.IsAuthenticate = "false";

            return View(content);
        }


        [AllowAnonymous]
        public ActionResult SubmitComment(string message, string urlParam)
        {
            try
            {
                if (!User.Identity.IsAuthenticated)
                    return Json("invalidUse", JsonRequestBehavior.AllowGet);

                var identity = (System.Security.Claims.ClaimsIdentity)User.Identity;

                Guid userId = new Guid(identity.FindFirst(System.Security.Claims.ClaimTypes.Name).Value);

                User user = UnitOfWork.UserRepository.GetById(userId);

                if (user == null)
                    return Json("invalidUser", JsonRequestBehavior.AllowGet);

                Content content = UnitOfWork.ContentRepository.Get(current => current.UrlParam == urlParam).FirstOrDefault();

                if (content != null)
                {
                    ContentComment comment = new ContentComment()
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        Comment = message,
                        IsDeleted = false,
                        IsActive = false,
                        CreationDate = DateTime.Now,
                        ContentId = content.Id
                    };

                    UnitOfWork.ContentCommentRepository.Insert(comment);
                    UnitOfWork.Save();

                    return Json("true", JsonRequestBehavior.AllowGet);
                }

                return Json("false", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
    }
}