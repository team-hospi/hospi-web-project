using hospi_web_project.Models;
using hospi_web_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    
    public class UserController : Controller
    {
        [Authorize]
        public IActionResult MyPage()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult PasswordChange(MemberViewModel model)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            MemberService context = new(dbService);

            if (!context.CheckPassword(model.email, model.password))
            {
                return RedirectToAction("PasswordCheckFail", "User");
            }
            else
            {
                context.ChangePassword(model.email, model.newPassword);
                return RedirectToAction("PasswordChangeSuccess", "User");
            }
        }

        [Authorize]
        public IActionResult PasswordChangeSuccess()
        {
            return View();
        }

        [Authorize]
        public IActionResult PasswordCheckFail()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Withdrawal(MemberViewModel model)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            MemberService context = new(dbService);

            if (!context.CheckPassword(model.email, model.password))
            {
                return RedirectToAction("PasswordCheckFail", "User");
            }
            else
            {
                context.Withdrawal(model.email);
                return RedirectToAction("WithdrawalSuccess", "User");
            }
        }

        [Authorize]
        public IActionResult WithdrawalSuccess()
        {
            return View();
        }
    }
}
