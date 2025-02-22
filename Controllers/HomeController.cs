﻿using hospi_web_project.Models;
using hospi_web_project.Services;
using hospi_web_project.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginProcess(LoginViewModel model, string ReturnUrl)
        {
            try
            { 
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                MemberService context = new(dbService);
                MemberViewModel member = new MemberViewModel();

                member = context.login(model);

                if (member != null)
                {
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, model.Email));
                    identity.AddClaim(new Claim(ClaimTypes.Name, model.Email));

                    var principal = new ClaimsPrincipal(identity);

                    if (model.Check == "1")
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                        {
                            IsPersistent = true,
                            AllowRefresh = true
                        });
                    }
                    else
                    {
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties
                        {
                            IsPersistent = false,
                            ExpiresUtc = DateTime.UtcNow.AddHours(1),
                            AllowRefresh = true
                        });
                        
                    }

                    if(ReturnUrl != "" && ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }

                    return RedirectToAction("Index", "Home");
                }

                // 로그인에 실패
                return View(model);
            }
            catch(Exception ex)
            {
                // 로그인에 실패
                return View(model);
            }
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
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
