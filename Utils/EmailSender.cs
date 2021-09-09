using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace hospi_web_project.Utils
{
    public class EmailSender
    {
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

        public void sendAuthNumMail(string email)
        {
            string senderEmail = "hospi@hopipi.kro.kr";
            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(senderEmail);
            mail.To.Add(new MailAddress(email));
            // 메일 제목
            mail.Subject = "Hospi 이메일 인증입니다.";
            // 본문 내용
            mail.Body = "Hospi 이메일 인증번호는 ["
                + getAuthNum()
                + "] 입니다.\n본인이 요청하신게 아니라면 이 메일은 무시하셔도 됩니다.";

            mail.SubjectEncoding = Encoding.UTF8;
            mail.BodyEncoding = Encoding.UTF8;

            SmtpClient client = new SmtpClient("katep.iptime.org");
            client.Send(mail);
        }
    }
}
