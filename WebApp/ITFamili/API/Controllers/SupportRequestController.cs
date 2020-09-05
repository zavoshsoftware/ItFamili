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
    public class SupportRequestController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();

        [Route("SupportRequest/post")]
        [HttpPost]
        [Authorize]
        public SuccessPostViewModel PostRequest(SupportRequestInputViewModel request)
        {
            SuccessPostViewModel result = new SuccessPostViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    SupportRequest supportRequest=new SupportRequest()
                    {
                        UserId = user.Id,
                        Subject = request.Subject,
                        Message = request.Message,
                        IsActive = true
                    };

                    UnitOfWork.SupportRequestRepository.Insert(supportRequest);
                    UnitOfWork.Save();


                    result.Result = Resources.Messages.SuccessPost;
                    result.Status = status.ReturnStatus(0, Resources.Messages.SuccessPost, true);
                }
                else
                {
                    result.Result = "خطا در بازیابی کاربر";
                    result.Status = status.ReturnStatus(100, "خطا در بازیابی کاربر. پارامتر هدر را بررسی کنید", false);
                }

            }
            catch (Exception)
            {
                result.Result = Resources.Messages.CatchError;
                result.Status = status.ReturnStatus(100, Resources.Messages.CatchError, false);
            }

            return result;
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
