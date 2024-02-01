using Stage03Library.Interface;
using Stage03Library.Repository;
using Stage03Library.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Stage03Library.Models;
using Question01MVCwithServerSidePagination.Models;

namespace Question01MVCwithServerSidePagination.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDatabaseRepository _repo;
        public AccountController()
        {
            _repo = new DatabaseRepository(new DefaultDbContext());
        }

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var isAuthenticated = _repo.LoginUser(model.Email, model.Password);

                if (isAuthenticated)
                {
                    var userId = _repo.GetUserIdByEmail(model.Email);
                    Session["UserId"] = userId;

                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = new UserModel
                    {
                        Name = model.Email,
                        Email = model.Email,
                        Password = model.Password,
                        SecurityCode = 1,
                        SecurityAnswer = model.SecurityAnswer,
                        IsActive = true,
                    };

                    _repo.UserRegister(user);

                    return RedirectToAction("Login");
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            return View(model);
        }

        [HttpGet]
        public JsonResult GetSecurityQuestions()
        {
            try
            {
                var securityQuestions = _repo.GetAllSecurityQuestions().ToList();
                return Json(securityQuestions, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = "Error retrieving security questions." });
            }
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecoverPassword(RecoverPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var recoverySuccess = _repo.ChangePassword(model.Email, model.SecurityCode, model.SecurityAnswer, model.NewPassword);

                if (recoverySuccess)
                {
                    // Password recovery successful, you can redirect to login page or display a success message
                    TempData["RecoveryMessage"] = "Password recovery successful!";
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("", "Password recovery failed. Please check your information and try again.");
                }
            }
            return View(model);
        }
    }
}
