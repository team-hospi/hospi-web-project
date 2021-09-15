using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hospi_web_project.Utils
{
    public class GenerateTool
    {
        // 정품키 생성 함수
        public static string GenerateProductKey()
        {
            string key = "";
            string[] ch = { "A", "B", "C", "D", "E", "F", "G",
                            "H", "I", "J", "K", "L", "M", "N",
                            "O", "P", "Q", "R", "S", "T", "U",
                            "V", "W", "X", "Y", "Z", "0", "1",
                            "2", "3", "4", "5", "6", "7", "8",
                            "9" };

            Random rand = new Random();

            for (int i = 0; i < 4; i++)
            {
                string temp = "";
                for (int j = 0; j < 5; j++)
                {
                    int idx = rand.Next(0, ch.Length);
                    temp += ch[idx];
                }

                key += temp;

                if (i != 3) key += "-";
            }

            return key;
        }
    }
}
