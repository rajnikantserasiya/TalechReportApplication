using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchDataFromTalechPOS_BLL
{
    public static class LogHelper
    {
        public static void Log(string message)
        {
            //StreamWriter sw = null;
            using (FileStream aFile = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(aFile))
                {
                    sw.WriteLine(message);
                }
            }
        }

        public static string GetEndTextForPost()
        {
            string endText;
            using (var streamReader = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "\\EndTextForPost.txt", Encoding.UTF8))
            {
                endText = streamReader.ReadToEnd();
            }

            return @"" + endText;
        }
    }
}
