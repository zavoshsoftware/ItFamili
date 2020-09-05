using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork : System.IDisposable
    {
        IActivationCodeRepository ActivationCodeRepository { get; }
        IRoleRepository RoleRepository { get; }
        IVersionHistoryRepository VersionHistoryRepository { get; }
        ICityRepository CityRepository { get; }
        IProvinceRepository ProvinceRepository { get; }
        IUserRepository UserRepository { get; }
        IContentRepository ContentRepository { get; }
        IContentTypeRepository ContentTypeRepository { get; }
        IMagzineRepository MagzineRepository { get; }
        IFaqRepository FaqRepository { get; }
        ISupportRequestRepository SupportRequestRepository { get; }
        IContentCommentRepository ContentCommentRepository { get; }
        IContentLikeRepository ContentLikeRepository { get; }
        IContentGroupRepository ContentGroupRepository { get; }
        IArAssetRepository ArAssetRepository { get; }

        void Save();
    }
}
