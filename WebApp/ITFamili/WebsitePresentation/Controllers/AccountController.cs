using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using Helper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Models;

namespace WebsitePresentation.Controllers
{
    public class AccountController : Infrastructure.BaseControllerWithUnitOfWork
    {
        readonly IrSms sms = new IrSms();

        public ActionResult RegisterUser(string fullName, string cellNumber)
        {
            try
            {


                User existUser = IsUserExist(cellNumber);
                int code = 12345;
                if (existUser == null)
                {
                    User user = CreateUserObject(cellNumber, fullName);

                    UnitOfWork.UserRepository.Insert(user);

                    UnitOfWork.Save();

                    code = CreateActivationCode(user.Id);

                    UnitOfWork.Save();

                    sms.SendActivationCode(cellNumber, code);

                    return Json("true|" + user.Id, JsonRequestBehavior.AllowGet);

                }

                code = CreateActivationCode(existUser.Id);

                UnitOfWork.Save();

                sms.SendActivationCode(cellNumber, code);

                return Json("true|" + existUser.Id, JsonRequestBehavior.AllowGet);


            }
            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);

            }
        }

        #region RegisterUser

        public User IsUserExist(string cellNumber)
        {
            Guid userRoleId = new Guid("b999eb27-7330-4062-b81f-62b3d1935885");
            User user = UnitOfWork.UserRepository
                .Get(current => current.RoleId == userRoleId && current.CellNum == cellNumber)
                .FirstOrDefault();

            return user;
        }

        public User CreateUserObject(string cellNumber, string fullName)
        {
            User user = new User();

            user.FullName = fullName;
            user.CellNum = cellNumber;
            user.IsActive = false;
            user.CreationDate = DateTime.Now;
            user.IsDeleted = false;
            user.Code = ReturnCode();
            user.RoleId = new Guid("b999eb27-7330-4062-b81f-62b3d1935885");

            return user;
        }

        public int ReturnCode()
        {
            Guid userRoleId = new Guid("b999eb27-7330-4062-b81f-62b3d1935885");

            User user = UnitOfWork.UserRepository.Get(current => current.RoleId == userRoleId).OrderByDescending(current => current.Code).FirstOrDefault();
            if (user != null)
            {
                return user.Code + 1;
            }
            else
            {
                return 300001;
            }
        }

        public int CreateActivationCode(Guid userId)
        {
            DeactiveOtherActivationCode(userId);

            int code = RandomCode();
            ActivationCode activationCode = new ActivationCode();
            activationCode.UserId = userId;
            activationCode.Code = code;
            activationCode.ExpireDate = DateTime.Now.AddDays(2);
            activationCode.IsUsed = false;
            activationCode.IsActive = true;
            activationCode.CreationDate = DateTime.Now;
            activationCode.IsDeleted = false;

            UnitOfWork.ActivationCodeRepository.Insert(activationCode);
            return code;
        }
        public void DeactiveOtherActivationCode(Guid userId)
        {
            List<ActivationCode> activationCodes = UnitOfWork.ActivationCodeRepository
                .Get(current => current.UserId == userId && current.IsActive == true).ToList();

            foreach (ActivationCode activationCode in activationCodes)
            {
                activationCode.IsActive = false;
                activationCode.LastModifiedDate = DateTime.Now;

                UnitOfWork.ActivationCodeRepository.Update(activationCode);
            }

        }

        private Random random = new Random();
        public int RandomCode()
        {
            Random generator = new Random();
            String r = generator.Next(0, 100000).ToString("D5");
            return Convert.ToInt32(r);
        }
        #endregion


        public ActionResult Activate(string code, string activationCode)
        {
            try
            {
                ActivationCode activation = IsValidActivationCode(code, activationCode);

                if (activation != null)
                {
                    ActivateUser(code, activationCode);
                    UpdateActivationCode(activation);
                    UnitOfWork.Save();

                    return Json("true", JsonRequestBehavior.AllowGet);
                }

                return Json("false", JsonRequestBehavior.AllowGet);

            }
            catch (Exception e)
            {
                return Json("error", JsonRequestBehavior.AllowGet);
            }
        }


        #region ActivateHelper
        public void ActivateUser(string tokenId, string activationCode)
        {
            Guid userId = new Guid(tokenId);
            User user = UnitOfWork.UserRepository.GetById(userId);
            user.IsActive = true;
            user.LastModifiedDate = DateTime.Now;
            user.Password = activationCode;
        }

        public void UpdateActivationCode(ActivationCode activationCode)
        {
            activationCode.IsUsed = true;
            activationCode.UsingDate = DateTime.Now;
            activationCode.IsActive = false;
            activationCode.LastModifiedDate = DateTime.Now;

            UnitOfWork.ActivationCodeRepository.Update(activationCode);
        }
        public ActivationCode IsValidActivationCode(string tokenId, string activationCode)
        {
            Guid userId = new Guid(tokenId);
            int code = Convert.ToInt32(activationCode);

            ActivationCode oActivationCode = UnitOfWork.ActivationCodeRepository
                .Get(current => current.UserId == userId && current.Code == code && current.IsUsed == false
                                && current.IsActive == true && current.ExpireDate > DateTime.Now).FirstOrDefault();

            if (oActivationCode != null)
                return oActivationCode;
            else
                return null;
        }
        #endregion

        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        public ActionResult SendOtp(string cellNumber)
        {
            try
            {
                cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");
                bool isValidMobile = Regex.IsMatch(cellNumber, @"(^(09|9)[0-9][0-9]\d{7}$)|(^(09|9)[3][12456]\d{7}$)", RegexOptions.IgnoreCase);

                if (isValidMobile)
                {
                    User user = UnitOfWork.UserRepository.Get(current => current.CellNum == cellNumber).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.RoleId == new Guid("B999EB27-7330-4062-B81F-62B3D1935885"))
                        {
                            int code = CreateActivationCode(user.Id);

                            UnitOfWork.Save();

                            sms.SendActivationCode(cellNumber, code);
                        }

                        return Json("true", JsonRequestBehavior.AllowGet);
                    }


                    return Json("invalidUser", JsonRequestBehavior.AllowGet);

                }
                return Json("invalidCellNumber", JsonRequestBehavior.AllowGet);

                //else
                //{
                //    Guid userId = CreateUser(fullName, cellNumber, email, employeeType);
                //    int codeInt = CreateActivationCode(userId);
                //    code = codeInt.ToString();
                //}


                //UnitOfWork.Save();

            }

            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult CompleteRegister(string cellNumber, string fullName)
        {
            try
            {
                cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");

                User user = UnitOfWork.UserRepository.Get(current => current.CellNum == cellNumber).FirstOrDefault();

                if (user == null)
                {
                    User oUser = CreateUserObject(cellNumber, fullName);

                    UnitOfWork.UserRepository.Insert(oUser);

                    UnitOfWork.Save();

                    int code = CreateActivationCode(oUser.Id);

                    UnitOfWork.Save();

                    sms.SendActivationCode(cellNumber, code);

                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                return Json("false", JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }


        [AllowAnonymous]
        public ActionResult CheckOtp(string cellNumber, string activationCode)
        {
            try
            {
                cellNumber = cellNumber.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");
                activationCode = activationCode.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");

                User user = UnitOfWork.UserRepository.Get(current => current.CellNum == cellNumber).FirstOrDefault();

                if (user != null)
                {
                    ActivationCode activation = IsValidActivationCode(user.Id.ToString(), activationCode);

                    if (activation != null)
                    {
                        ActivateUser(user.Id.ToString(), activationCode);

                        UpdateActivationCode(activation);

                        UnitOfWork.Save();

                        LoginWithOtp(user);

                        return Json("true", JsonRequestBehavior.AllowGet);
                    }

                    return Json("invalid", JsonRequestBehavior.AllowGet);
                }
                return Json("invalid", JsonRequestBehavior.AllowGet);
            }

            catch (Exception e)
            {
                return Json("false", JsonRequestBehavior.AllowGet);
            }
        }
        public void LoginWithOtp(User oUser)
        {
            var ident = new ClaimsIdentity(
                new[] { 
                    // adding following 2 claim just for supporting default antiforgery provider
                    new Claim(ClaimTypes.NameIdentifier, oUser.CellNum),
                    new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity", "http://www.w3.org/2001/XMLSchema#string"),

                    new Claim(ClaimTypes.Name,oUser.Id.ToString()),

                    // optionally you could add roles if any
                    new Claim(ClaimTypes.Role, oUser.Role.Name),
                  //  new Claim(ClaimTypes.Surname, oUser.FullName),

                },
                DefaultAuthenticationTypes.ApplicationCookie);

            HttpContext.GetOwinContext().Authentication.SignIn(
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(600),

                },
                ident);

        }
    }
}