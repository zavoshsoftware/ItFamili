using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace WebsitePresentation.Controllers
{
    public class MagzinesController : Infrastructure.BaseControllerWithUnitOfWork
    {
        [Route("magzine")]
        public ActionResult List()
        {
            MagzineListViewModel magzines=new MagzineListViewModel()
            {
                Magzines = UnitOfWork.MagzineRepository.Get(c=>c.IsActive).OrderByDescending(c=>c.CreationDate).ToList()
            };
            return View(magzines);
        }

        [Route("magzine/{urlParam}")]
        public ActionResult Details(string urlParam)
        {
            MagzineDetailViewModel magzine=new MagzineDetailViewModel()
            {
                Magzine = UnitOfWork.MagzineRepository.Get(c=>c.UrlParam==urlParam&& c.IsActive).FirstOrDefault()
            };
            return View(magzine);
        }
    }
}