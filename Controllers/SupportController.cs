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
    public class SupportController : Controller
    {
        public IActionResult ProductAuthGuide()
        {
            return View();
        }

        public IActionResult ProductSupport()
        {
            return View();
        }

        public IActionResult Notice()
        {
            return View();
        }

        public IActionResult Inquiry()
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            var list = context.GetBoardList().Cast<InquiryBoardViewModel>().ToList();

            return View(list);
        }

        [Authorize]
        public IActionResult InquiryCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult InquiryCreateProcess(InquiryBoardViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                InquiryBoardService context = new(dbService);

                context.WriteBoard(model);
            }

            return RedirectToAction("Inquiry", "Support");
        }

        [Authorize]
        [HttpGet]
        public IActionResult InquiryDelete(int no)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            context.DeleteBoard(no);

            return RedirectToAction("Inquiry", "Support");
        }

        [HttpGet]
        public IActionResult InquiryDetails(int no)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            var model = (InquiryBoardViewModel)context.GetBoardDetail(no);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult InquiryUpdate(int no)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            var model = (InquiryBoardViewModel)context.GetBoardDetail(no);

            return View(model);
        }

        [Authorize]
        public IActionResult InquiryUpdateProcess(InquiryBoardViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                InquiryBoardService context = new(dbService);

                context.UpdateBoard(model);
            }

            return RedirectToAction("InquiryDetails", "Support", new { no = model.No });
        }
    }
}
