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
    public class ContentGroupController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();

        [Route("ContentGroup/Get")]
        [HttpPost]
        public ContentGroupListViewModel PostContentGroupList()
        {
            ContentGroupListViewModel result = new ContentGroupListViewModel();
            try
            {

                result.Result = GetContentGroupList();
                result.Status = status.ReturnStatus(0, Resources.Messages.SuccessGet, true);

            }
            catch (Exception)
            {
                result.Result = null;
                result.Status = status.ReturnStatus(100, Resources.Messages.CatchError, false);
            }

            return result;
        }


        public List<ContentGroupItemListViewModel> GetContentGroupList()
        {
            List<ContentGroupItemListViewModel> contentGroups = new List<ContentGroupItemListViewModel>();

            List<ContentGroup> groups = UnitOfWork.ContentGroupRepository
                .Get(current => current.IsActive)
                .OrderByDescending(c => c.CreationDate).ToList();

            foreach (ContentGroup contentGroup in groups)
            {
                contentGroups.Add(new ContentGroupItemListViewModel()
                {
                    Title = contentGroup.Title,
                    Id = contentGroup.Id.ToString(),
                });
            }

            return contentGroups;
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

