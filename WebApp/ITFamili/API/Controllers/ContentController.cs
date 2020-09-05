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
    public class ContentController : Infrastructure.BaseControllerWithUnitOfWork
    {
        StatusManagement status = new StatusManagement();

        Guid _videoContentTypeId = new Guid("6D995A1E-0943-4761-8579-EC8878EA4F8B");
        Guid _podcastContentTypeId = new Guid("13AD9A72-B049-4A7F-8516-D4F4EA9BA5C9");
        Guid _blogContentTypeId = new Guid("428AB728-6C2F-4D9E-AFAB-C5D69C264D5E");



        [Route("Content/GetContentByGroup")]
        [HttpPost]
        [Authorize]
        public ContentListViewModel PostContentList(ContentGroupInputViewModel input)
        {
            ContentListViewModel result = new ContentListViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetContentListByGroupId(new Guid(input.ContentGroupId), user.Id);
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


        [Route("Content/Getvideos")]
        [HttpPost]
        [Authorize]
        public ContentListViewModel PostVideoList()
        {
            ContentListViewModel result = new ContentListViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetVideoList(_videoContentTypeId, user.Id);
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

        [Route("Content/GetPodcasts")]
        [HttpPost]
        [Authorize]
        public ContentListViewModel PostPodcastList()
        {
            ContentListViewModel result = new ContentListViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetVideoList(_podcastContentTypeId, user.Id);
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

        [Route("Content/GetBlogContents")]
        [HttpPost]
        [Authorize]
        public ContentListViewModel PostBlogList()
        {
            ContentListViewModel result = new ContentListViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetVideoList(_blogContentTypeId, user.Id);
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
        public List<ContentListItemViewModel> GetVideoList(Guid contentTypeId, Guid userId)
        {
            List<ContentListItemViewModel> videos = new List<ContentListItemViewModel>();

            List<Content> videoContents = UnitOfWork.ContentRepository
                .Get(current => current.ContentTypeId == contentTypeId && current.IsActive)
                .OrderByDescending(c => c.CreationDate).ToList();

            foreach (Content content in videoContents)
            {
                videos.Add(new ContentListItemViewModel()
                {
                    Image = _filesBaseUrl + content.ImageUrl,
                    Summery = content.Summery,
                    Title = content.Title,
                    LinkeCount = content.LikeCount.ToString(),
                    Id = content.Id.ToString(),
                    Body = content.Body,
                    LinkAddress = _filesBaseUrl + content.FileUrl,
                    CommentCount = content.CommentCount.ToString(),
                    ContentSource = "مجله تخصصی آی تی",
                    PublishDate = "10/12/1398",
                    IsLike = IsLikeContent(content.Id, userId),
                    Type = GetTypeById(content.ContentTypeId)
                });
            }

            return videos;
        }



        public List<ContentListItemViewModel> GetContentListByGroupId(Guid contentGroupId, Guid userId)
        {
            List<ContentListItemViewModel> videos = new List<ContentListItemViewModel>();

            List<Content> videoContents = UnitOfWork.ContentRepository
                .Get(current => current.ContentGroupId == contentGroupId && current.IsActive)
                .OrderByDescending(c => c.CreationDate).ToList();

            foreach (Content content in videoContents)
            {
                videos.Add(new ContentListItemViewModel()
                {
                    Image = _filesBaseUrl + content.ImageUrl,
                    Summery = content.Summery,
                    Title = content.Title,
                    LinkeCount = content.LikeCount.ToString(),
                    Id = content.Id.ToString(),
                    Body = content.Body,
                    LinkAddress = _filesBaseUrl + content.FileUrl,
                    CommentCount = content.CommentCount.ToString(),
                    ContentSource = "مجله تخصصی آی تی",
                    PublishDate = "10/12/1398",
                    IsLike = IsLikeContent(content.Id, userId),
                    Type = GetTypeById(content.ContentTypeId)

                });
            }

            return videos;
        }

        public string GetTypeById(Guid id)
        {
            string type = "video";

            if (id == _blogContentTypeId)
                type = "blog";

            else if (id == _podcastContentTypeId)
                type = "podcast";

            else if (id == _videoContentTypeId)
                type = "video";

            return type;
        }

        public bool IsLikeContent(Guid contentId, Guid userId)
        {
            ContentLike contentLike = UnitOfWork.ContentLikeRepository
                .Get(current => current.UserId == userId && current.ContentId == contentId).FirstOrDefault();

            if (contentLike == null)
                return false;

            return contentLike.IsLike;
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


        [Route("Content/PostLike")]
        [HttpPost]
        [Authorize]
        public SuccessPostViewModel PostLike(PostLikeInputViewModel likeInputViewModel)
        {
            SuccessPostViewModel result = new SuccessPostViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    if (!LikeAndDislike(likeInputViewModel, user.Id))
                    {
                        result.Result = null;
                        result.Status = status.ReturnStatus(100, Resources.Messages.InvalidContent, false);
                    }

                    result.Result = Resources.Messages.SuccessPost;
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

        public bool LikeAndDislike(PostLikeInputViewModel likeInputViewModel, Guid userId)
        {
            Guid contentId = new Guid(likeInputViewModel.ContentId);
            Content content = UnitOfWork.ContentRepository.GetById(contentId);

            if (content == null)
                return false;

            ContentLike contentLike = UnitOfWork.ContentLikeRepository
                .Get(current => current.ContentId == contentId && current.UserId == userId).FirstOrDefault();

            if (contentLike != null)
            {
                if (contentLike.IsLike != likeInputViewModel.IsLike)
                {
                    contentLike.IsLike = likeInputViewModel.IsLike;

                    UnitOfWork.ContentLikeRepository.Update(contentLike);
                    UnitOfWork.Save();
                }
            }
            else
            {
                ContentLike oContentLike = new ContentLike()
                {
                    UserId = userId,
                    ContentId = contentId,
                    IsLike = likeInputViewModel.IsLike,
                    IsActive = true
                };
                UnitOfWork.ContentLikeRepository.Insert(oContentLike);
                UnitOfWork.Save();
            }

            return true;
        }




        [Route("Content/PostComment")]
        [HttpPost]
        [Authorize]
        public SuccessPostViewModel PostComment(PostCommentInputViewModel commentInputViewModel)
        {
            SuccessPostViewModel result = new SuccessPostViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    if (!SubmitComment(commentInputViewModel, user.Id))
                    {
                        result.Result = null;
                        result.Status = status.ReturnStatus(100, Resources.Messages.InvalidContent, false);
                    }

                    result.Result = Resources.Messages.SuccessPost;
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



        public bool SubmitComment(PostCommentInputViewModel commentInputViewModel, Guid userId)
        {
            Guid contentId = new Guid(commentInputViewModel.ContentId);
            Content content = UnitOfWork.ContentRepository.GetById(contentId);

            if (content == null)
                return false;


            ContentComment oContentComment = new ContentComment()
            {
                UserId = userId,
                ContentId = contentId,
                Comment = commentInputViewModel.Comment,
                IsActive = true
            };
            UnitOfWork.ContentCommentRepository.Insert(oContentComment);

            content.CommentCount = content.CommentCount + 1;

            UnitOfWork.ContentRepository.Update(content);

            UnitOfWork.Save();

            return true;
        }


        [Route("Content/GetComments")]
        [HttpPost]
        [Authorize]
        public ConmmentsViewModel PostCommentList(GetCommentInputViewModel commentInput)
        {
            ConmmentsViewModel result = new ConmmentsViewModel();
            try
            {
                string token = GetRequestHeader();
                User user = UnitOfWork.UserRepository.GetByToken(token);

                if (user != null)
                {
                    result.Result = GetCommentResult(commentInput);
                    result.Status = status.ReturnStatus(0, Resources.Messages.SuccessGet, true);
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

        public List<CommentItemViewModel> GetCommentResult(GetCommentInputViewModel commentInput)
        {
            Guid contentId = new Guid(commentInput.ContentId);
            Content content = UnitOfWork.ContentRepository.GetById(contentId);

            if (content == null)
                return null;

            int skip = (commentInput.PageId - 1) * 10;
            List<ContentComment> contentComments = UnitOfWork.ContentCommentRepository
                .Get(current => current.ContentId == contentId).OrderByDescending(current => current.CreationDate).Skip(skip).Take(10)
                .ToList();

            List<CommentItemViewModel> comments = new List<CommentItemViewModel>();

            foreach (ContentComment contentComment in contentComments)
            {
                comments.Add(new CommentItemViewModel()
                {
                    Comment = contentComment.Comment,
                    CommentDate = contentComment.CreationDateStr,
                    UserFullName = contentComment.User.FullName
                });
            }

            return comments;
        }

    }
}

