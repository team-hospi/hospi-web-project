using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace hospi_web_project.Utils
{
    public class AuthNumEmailSender
    {
        private MailMessage mail = new MailMessage();

        private string getAuthNum()
        {
            string[] numArr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string authNum = "";

            HashSet<int> numbers = new HashSet<int>();
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                int temp;
                do
                {
                    temp = r.Next(0, 9);
                } while (numbers.Add(temp) == false);
            }
            foreach (int num in numbers)
            {
                authNum += numArr[num];
            }

            return authNum;
        }

        public void sendAuthNum(string email)
        {
            string senderEmail = "";
            string senderPassword = "";

            try
            {
                // 보내는 사람 메일, 이름, 인코딩(UTF-8)
                mail.From = new MailAddress(senderEmail, "Hospi", System.Text.Encoding.UTF8);
                // 받는 사람 메일
                mail.To.Add(email);
                // 메일 제목
                mail.Subject = "Hospi 이메일 인증입니다.";
                // 본문 내용
                mail.Body = "<html><body>"
                    + "Hospi 이메일 인증번호는 ["
                    + getAuthNum()
                    + "] 입니다."
                    + "<br />"
                    + "본인이 요청하신게 아니라면 이 메일은 무시하셔도 됩니다."
                    + "</body></html>";
                // 본문 내용 포멧의 타입 (true의 경우 Html 포멧으로)
                mail.IsBodyHtml = true;
                // 메일 제목과 본문의 인코딩 타입(UTF-8)
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                // smtp 서버 주소
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                // smtp 포트
                SmtpServer.Port = 587;
                // smtp 인증
                SmtpServer.Credentials = new System.Net.NetworkCredential(senderEmail, senderPassword);
                // SSL 사용 여부
                SmtpServer.EnableSsl = true;
                // 발송
                SmtpServer.Send(mail);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
