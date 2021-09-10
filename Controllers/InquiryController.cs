using hospi_web_project.Models;
using hospi_web_project.Utils;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Controllers
{
    public class InquiryController : Controller
    {
        public IActionResult GeneralInquiry()
        {
            return View();
        }

        public IActionResult GenralInquiryProcess(InquiryViewModel model)
        {
            string title = "[일반문의] " + model.Title;
            string content = "문의자: " + model.Name
                + "\n문의자 이메일: " + model.Email
                + "\n문의 내용\n"
                + model.Content;
            
            EmailSender.SendMail(title, content, model.File);

            return RedirectToAction("InquirySuccess", "Inquiry");
        }

        public IActionResult ProductInquiry()
        {
            return View();
        }

        public IActionResult ProductInquiryProcess(ProductInquiryViewModel model)
        {
            string title = "[제품문의] " + model.Title;
            string content = "문의자: " + model.Name
                + "\n문의자 이메일: " + model.Email
                + "\n문의 종류: " + model.Kind
                + "\n문의 내용\n"
                + model.Content;

            EmailSender.SendMail(title, content, model.File);

            return RedirectToAction("InquirySuccess", "Inquiry");
        }

        public IActionResult InquirySuccess()
        {
            return View();
        }
    }
}