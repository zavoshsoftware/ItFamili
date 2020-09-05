using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;
using Models;


namespace WebsitePresentation.Controllers
{
    public class ContentGroupController : Infrastructure.BaseControllerWithUnitOfWork
    {
        // GET: ContentGroup
        [Route("contentGroup/{urlParam}")]
        public ActionResult List(string urlParam)
        {
            ContentGroup contentGroup = UnitOfWork.ContentGroupRepository.Get(x => x.UrlParam == urlParam && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();
            if (contentGroup == null)
                return Redirect("/");
            ContentGroupListViewModel viewModel = new ContentGroupListViewModel()
            {
                Contents =UnitOfWork.ContentRepository.Get(x=>x.ContentGroupId == contentGroup.Id).ToList()
            };
            ViewBag.Title = contentGroup.Title;
            return View(viewModel);
        }
    }
}