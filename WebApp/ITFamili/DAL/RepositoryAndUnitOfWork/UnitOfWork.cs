using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class UnitOfWork : System.Object, IUnitOfWork
    {
        public UnitOfWork()
        {
            IsDisposed = false;
        }
        protected bool IsDisposed { get; set; }

        private Models.DatabaseContext _databaseContext;
        protected virtual Models.DatabaseContext DatabaseContext
        {
            get
            {
                if (_databaseContext == null)
                {
                    _databaseContext =
                        new Models.DatabaseContext();
                }
                return (_databaseContext);
            }
        }
        public void Save()
        {
            try
            {
                DatabaseContext.SaveChanges();
            }
            //catch (System.Exception ex)
            catch
            {
                //Hmx.LogHandler.Report(GetType(), null, ex);
                throw;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _databaseContext.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion

        #region Inserting custom Respositories
        //private IRepository _Repository;
        //public IRepository Repository
        //{
        //	get
        //	{
        //		if (_Repository == null)
        //		{
        //			_Repository =
        //				new Repository(DatabaseContext);
        //		}
        //		return (_Repository);
        //	}
        //}

        private IVersionHistoryRepository _versionHistoryRepository;
        public IVersionHistoryRepository VersionHistoryRepository
        {
            get
            {
                if (_versionHistoryRepository == null)
                {
                    _versionHistoryRepository =
                        new VersionHistoryRepository(DatabaseContext);
                }
                return (_versionHistoryRepository);
            }
        }
      
        private IUserRepository _userRepository;
        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository =
                        new UserRepository(DatabaseContext);
                }
                return (_userRepository);
            }
        }

        private IActivationCodeRepository _activationCodeRepository;
        public IActivationCodeRepository ActivationCodeRepository
        {
            get
            {
                if (_activationCodeRepository == null)
                {
                    _activationCodeRepository =
                        new ActivationCodeRepository(DatabaseContext);
                }
                return (_activationCodeRepository);
            }
        }
   
        private IRoleRepository _roleRepository;
        public IRoleRepository  RoleRepository
        {
            get
            {
                if (_roleRepository == null)
                {
                    _roleRepository =
                        new RoleRepository(DatabaseContext);
                }
                return (_roleRepository);
            }
        }

        private ICityRepository _cityRepository;
        public ICityRepository CityRepository
        {
            get
            {
                if (_cityRepository == null)
                {
                    _cityRepository =
                        new CityRepository(DatabaseContext);
                }
                return (_cityRepository);
            }
        }
      
        private IProvinceRepository _provinceRepository;
        public IProvinceRepository ProvinceRepository
        {
            get
            {
                if (_provinceRepository == null)
                {
                    _provinceRepository =
                        new ProvinceRepository(DatabaseContext);
                }
                return (_provinceRepository);
            }
        }

        private IContentRepository _contentRepository;
        public IContentRepository ContentRepository
        {
            get
            {
                if (_contentRepository == null)
                {
                    _contentRepository =
                        new ContentRepository(DatabaseContext);
                }
                return (_contentRepository);
            }
        }

        private IContentTypeRepository _contentTypeRepository;
        public IContentTypeRepository ContentTypeRepository
        {
            get
            {
                if (_contentTypeRepository == null)
                {
                    _contentTypeRepository =
                        new ContentTypeRepository(DatabaseContext);
                }
                return (_contentTypeRepository);
            }
        }

        private IMagzineRepository _magzineRepository;
        public IMagzineRepository MagzineRepository
        {
            get
            {
                if (_magzineRepository == null)
                {
                    _magzineRepository =
                        new MagzineRepository(DatabaseContext);
                }
                return (_magzineRepository);
            }
        }

        private IFaqRepository _faqRepository;
        public IFaqRepository FaqRepository
        {
            get
            {
                if (_faqRepository == null)
                {
                    _faqRepository =
                        new FaqRepository(DatabaseContext);
                }
                return (_faqRepository);
            }
        }

        private ISupportRequestRepository _supportRequestRepository;
        public ISupportRequestRepository SupportRequestRepository
        {
            get
            {
                if (_supportRequestRepository == null)
                {
                    _supportRequestRepository =
                        new SupportRequestRepository(DatabaseContext);
                }
                return (_supportRequestRepository);
            }
        }

        private IContentCommentRepository _contentCommentRepository;
        public IContentCommentRepository ContentCommentRepository
        {
            get
            {
                if (_contentCommentRepository == null)
                {
                    _contentCommentRepository =
                        new ContentCommentRepository(DatabaseContext);
                }
                return (_contentCommentRepository);
            }
        }

        private IContentLikeRepository _contentLikeRepository;
        public IContentLikeRepository ContentLikeRepository
        {
            get
            {
                if (_contentLikeRepository == null)
                {
                    _contentLikeRepository =
                        new ContentLikeRepository(DatabaseContext);
                }
                return (_contentLikeRepository);
            }
        }
        private IContentGroupRepository _contentGroupRepository;
        public IContentGroupRepository ContentGroupRepository
        {
            get
            {
                if (_contentGroupRepository == null)
                {
                    _contentGroupRepository =
                        new ContentGroupRepository(DatabaseContext);
                }
                return (_contentGroupRepository);
            }
        }
        private IArAssetRepository _arAssetRepository;
        public IArAssetRepository ArAssetRepository
        {
            get
            {
                if (_arAssetRepository == null)
                {
                    _arAssetRepository =
                        new ArAssetRepository(DatabaseContext);
                }
                return (_arAssetRepository);
            }
        }

        #endregion Inserting custom Respositories

    }
}
