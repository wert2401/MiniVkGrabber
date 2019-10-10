using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSideMiniGraber.VKLogic
{
    static class FileManager
    {
        static public string GetToken()
        {
            string token = "";
            if (File.Exists("token.txt"))
            {
                using (StreamReader sr = new StreamReader("token.txt"))
                {
                    token = sr.ReadLine();
                }
            }
            else
            {
                return "Error";
            }
            return token;
        }

        static public void SetToken(string token)
        {
            using (StreamWriter sw = new StreamWriter("token.exe"))
            {
                sw.WriteLine(token);
            }
        }
    }
}
