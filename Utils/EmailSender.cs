using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace hospi_web_project.Utils
{
    public class EmailSender
    {
        public static void SendMail(string title, string content, IFormFile file)
        {
            string senderEmail = "hospi@hopipi.kro.kr";
            string recipientEmail = "tnfy10@naver.com";

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(senderEmail);
            mail.To.Add(new MailAddress(recipientEmail));
            // 메일 제목
            mail.Subject = title;
            // 본문 내용
            mail.Body = content;

            if(file != null)
            {
                mail.Attachments.Add(new Attachment(file.OpenReadStream(), file.FileName));
            }

            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient("127.0.0.1");
            client.Send(mail);
        }
    }
}