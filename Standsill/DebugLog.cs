using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Standsill
{
    class DebugLog
    {
        const bool DEBUG = true;
        static string debugPath = Directory.GetCurrentDirectory() + "\\DebugLog.txt";
        public static void Log(String Info)
        {
            try
            {
                if (DEBUG)
                    File.AppendAllText(debugPath, String.Format("{0}\r\n", Info));
            }
            catch(System.IO.IOException)
            {
                //TODO
            }
        }
    }
}
