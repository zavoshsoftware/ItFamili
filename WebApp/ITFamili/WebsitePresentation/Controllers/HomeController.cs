using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels;

namespace WebsitePresentation.Controllers
{
    public class HomeController : Infrastructure.BaseControllerWithUnitOfWork
    {
        readonly Guid _blogTypeId=new Guid("428AB728-6C2F-4D9E-AFAB-C5D69C264D5E");
            public ActionResult Index()
        {
          HomeViewModel home=new HomeViewModel()
          {
              LatestMagzine = UnitOfWork.MagzineRepository.Get().OrderByDescending(c=>c.CreationDate).FirstOrDefault(),
              LateMagzines = UnitOfWork.MagzineRepository.Get().OrderByDescending(c => c.CreationDate).Take(8).ToList(),
              LatestBlogs = UnitOfWork.ContentRepository.Get(c=>c.ContentTypeId== _blogTypeId && c.IsActive).OrderByDescending(c => c.CreationDate).Take(8).ToList(),
              TopContents = UnitOfWork.ContentRepository.Get(c => c.IsActive && c.IsInHome == true).Take(8).ToList(),
          };
            return View(home);
        }
    }
}