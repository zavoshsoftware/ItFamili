using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using API.Models;
using API.Models.Input;
using Models;
using Helper;
using Models.Input;


namespace API.Controllers
{
    public class MagzineController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();

        [Route("Magzine/GetList")]
        [HttpPost]
        [Authorize]
        public MagzineListViewModel PostMagzineList()
        {
            MagzineListViewModel result = new MagzineListViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetMagzineList();
                    result.Status = status.ReturnStatus(0, Resources.Messages.SuccessPost, true);
                }
                else
                {
                    result.Result = null;
                    result.Status = status.ReturnStatus(100, Resources.Messages.InvalidUser, false);
                }

            }
            catch (Exception)
            {
                result.Result = null;
                result.Status = status.ReturnStatus(100, Resources.Messages.CatchError, false);
            }

            return result;
        }


        [Route("ar/Magzine/GetList")]
        [HttpPost]
        public MagzineListForARViewModel PostMagzineListForAr()
        {
            MagzineListForARViewModel result = new MagzineListForARViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetMagzineListForAr();
                    result.Status = status.ReturnStatus(0, Resources.Messages.SuccessPost, true);
                }
                else
                {
                    result.Result = null;
                    result.Status = status.ReturnStatus(100, Resources.Messages.InvalidUser, false);
                }

            }
            catch (Exception)
            {
                result.Result = null;
                result.Status = status.ReturnStatus(100, Resources.Messages.CatchError, false);
            }

            return result;
        }

 
       


        readonly string _filesBaseUrl = System.Web.Configuration.WebConfigurationManager.AppSettings["filesBaseUrl"];
        public List<MagzineListItemViewModel> GetMagzineList()
        {
            List<MagzineListItemViewModel> magzines = new List<MagzineListItemViewModel>();

            List<Magzine> magzineList = UnitOfWork.MagzineRepository
                .Get(current =>  current.IsActive)
                .OrderByDescending(c => c.CreationDate).ToList();

            foreach (Magzine magzine in magzineList)
            {
                magzines.Add(new MagzineListItemViewModel()
                {
                    Image = _filesBaseUrl+ magzine.ImageUrl,
                    Summery = magzine.Summery,
                    Body = magzine.Summery,
                    Title = magzine.Title,
                    LinkeCount = magzine.LikeCount.ToString(),
                    Id = magzine.Id.ToString(),
                    LinkAddress = _filesBaseUrl+ magzine.FileUrl,
                    CommentCount = magzine.CommentCount.ToString(),
                    ContentSource = "مجله تخصصی آی تی",
                     PublishDate = "10/12/1398"
                });
            }

            return magzines;
        }
   public List<MagzineListItemForArViewModel> GetMagzineListForAr()
        {
            List<MagzineListItemForArViewModel> magzines = new List<MagzineListItemForArViewModel>();

            List<Magzine> magzineList = UnitOfWork.MagzineRepository
                .Get(current =>  current.IsActive)
                .OrderByDescending(c => c.CreationDate).ToList();

            foreach (Magzine magzine in magzineList)
            {
                magzines.Add(new MagzineListItemForArViewModel()
                {
                    Version = magzine.Version.Value,
                    Title = magzine.Title,
                     PublishDate = magzine.CreationDateStr
                });
            }

            return magzines;
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
