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
    public class ArAssetController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();

        [Route("ar/Arasset/Get")]
        [HttpPost]
        public ArAssetsViewModel PostArAsset(ArAssetInputViewModel input)
        {
            ArAssetsViewModel result = new ArAssetsViewModel();
            try
            {
                int version = Convert.ToInt32(input.MagzineVersion);
                Magzine magzine = UnitOfWork.MagzineRepository.Get(c => c.Version == version).FirstOrDefault();

                if (magzine == null)
                {
                    result.Result = null;
                    result.Status = status.ReturnStatus(100, Resources.Messages.InvalidMagzineVersion, false);
                }
                else
                {
                    result.Result = GetArAssets(magzine.Id);
                    result.Status = status.ReturnStatus(0, Resources.Messages.SuccessGet, true);
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

        public List<ArAssetItemViewModel> GetArAssets(Guid magzineId)
        {
            List<ArAssetItemViewModel> list=new List<ArAssetItemViewModel>();

            List<ArAsset> arAssets = UnitOfWork.ArAssetRepository.Get(c => c.MagzineId == magzineId).ToList();

            foreach (ArAsset arAsset in arAssets)
            {
                list.Add(new ArAssetItemViewModel()
                {
                    Id = arAsset.Id.ToString(),
                    InputImageSize = arAsset.InputSize,
                    InputImageUrl = _filesBaseUrl+arAsset.InputImageUrl,
                    OutPutFileUrl = _filesBaseUrl+arAsset.OutputFileUrl,
                    OutPutType = arAsset.OutPutType
                });
            }

            return list;
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

