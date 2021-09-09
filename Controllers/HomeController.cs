using hospi_web_project.Models;
using hospi_web_project.Service;
using hospi_web_project.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace hospi_web_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginProcess(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                LoginService context = new(dbService);

                MemberViewModel member = context.login(model);

                // TODO: 로그인 세션 관련해서 코드 추가 필요

                if (member != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            // 로그인에 실패
            ModelState.AddModelError(string.Empty, "사용자 ID 혹은 비밀번호가 올바르지 않습니다.");

            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult SignUpSuccess()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignupProcess(MemberViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                SignUpService context = new(dbService);

                context.Signup(model);


                return RedirectToAction("SignUpSuccess", "Home");
            }

            return View();
        }


        [HttpPost]
        public IActionResult AuthNumSend(MemberViewModel model)
        {
            EmailSender sender = new EmailSender();
            sender.sendAuthNumMail(model.email);

            return RedirectToAction("SignUp", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
