using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using API.Models;
using Models;
using Helper;


namespace API.Controllers
{
    public class FaqController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();

        [Route("faq/get")]
        [HttpPost]
        public FaqViewModel PostFaq()
        {
            FaqViewModel faqViewModel = new FaqViewModel();

            try
            {
                List<Faq> faqs = UnitOfWork.FaqRepository.Get().OrderBy(c => c.Order).ToList();

                List<FaqItemViewModel> faqItems = new List<FaqItemViewModel>();

                foreach (Faq faq in faqs)
                {
                    faqItems.Add(new FaqItemViewModel()
                    {
                        Question = faq.Question,
                        Answer = faq.Answer
                    });
                }

                faqViewModel.Result = faqItems;
                faqViewModel.Status = status.ReturnStatus(0, Resources.Messages.SuccessGet, true);
            }
            catch (Exception)
            {
                return new FaqViewModel()
                {
                    Result = null,
                    Status = status.ReturnStatus(100, Resources.Messages.CatchError, false)
                };
            }

            return faqViewModel;
        }
    }
}
