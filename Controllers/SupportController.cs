using hospi_web_project.Models;
using hospi_web_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            NoticeService context = new(dbService);

            var list = context.GetBoardList().Cast<NoticeViewModel>().ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult NoticeSearch(string query)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            NoticeService context = new(dbService);

            var list = context.SearchBoardList(query).Cast<NoticeViewModel>().ToList();

            return View(list);
        }

        [Authorize]
        public IActionResult NoticeCreate()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult NoticeCreateProcess(NoticeViewModel model)
        {

            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                NoticeService context = new(dbService);

                context.WriteBoard(model);
            }

            return RedirectToAction("Notice", "Support");
        }

        [Authorize]
        [HttpGet]
        public IActionResult NoticeDelete(int no)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            NoticeService context = new(dbService);

            context.DeleteBoard(no);

            return RedirectToAction("Notice", "Support");
        }

        [HttpGet]
        public IActionResult NoticeDetails(int no)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            NoticeService context = new(dbService);

            var model = (NoticeViewModel)context.GetBoardDetail(no);

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public IActionResult NoticeUpdate(int no)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            NoticeService context = new(dbService);

            var model = (NoticeViewModel)context.GetBoardDetail(no);

            return View(model);
        }

        [Authorize]
        public IActionResult NoticeUpdateProcess(NoticeViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                NoticeService context = new(dbService);

                context.UpdateBoard(model);
            }

            return RedirectToAction("NoticeDetails", "Support", new { no = model.No });
        }

        public IActionResult Inquiry()
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            var list = context.GetBoardList().Cast<InquiryBoardViewModel>().ToList();

            return View(list);
        }

        [HttpGet]
        public IActionResult InquirySearch(string query)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            var list = context.SearchBoardList(query).Cast<InquiryBoardViewModel>().ToList();

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

            if (model.IsPrivate == 1)
                if(User.Identity.Name != model.Email)
                {
                    return RedirectToAction("InquiryNoAccess", "Support");
                }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public ActionResult DownloadFile(int no, string fileName)
        {
            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            byte[] rawData = context.GetFileByte(no);

            return File(rawData, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        public IActionResult InquiryNoPermission()
        {
            return View();
        }

        public IActionResult InquiryNoAccess()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public IActionResult InquiryReply(string no)
        {
            ViewData["no"] = no;

            DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
            InquiryBoardService context = new(dbService);

            var model = (InquiryBoardViewModel)context.GetBoardDetail(Convert.ToInt32(no));

            var list = context.GetManagerList();

            for (int i = 0; i < list.Count; i++)
            {
                if (User.Identity.Name == list[i].Email)
                {
                    return View(model);
                }
            }
            return RedirectToAction("InquiryNoPermission", "Support");
            
        }

        [Authorize]
        public IActionResult InquiryReplyProcess(InquiryBoardViewModel model)
        {
            if (ModelState.IsValid)
            {
                DBService dbService = HttpContext.RequestServices.GetService(typeof(DBService)) as DBService;
                InquiryBoardService context = new(dbService);

                context.WriteReply(model);
            }

            return RedirectToAction("InquiryDetails", "Support", new { no = model.No });
        }
    }
}
