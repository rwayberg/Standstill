using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Diagnostics;

namespace Standsill
{
    class VideoProcess
    {
        //ffmpeg.exe -f concat -i vods_8adb.txt -c copy dw1.ts
        public static string ffmpegDir = @"T:\Coding\FFMPEG\bin\ffmpeg.exe";

        public static void ConcatFiles(string ConcatList, string DestFilePath)
        {
            //TODO add error catching for the ffmpeg process erroring.
            if(!File.Exists(ConcatList))
                throw new ArgumentException(String.Format("{0} does not exist", ConcatList));
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.Arguments = "/c " + ffmpegDir + " -f concat -i \"" + ConcatList + "\" -c copy \"" + DestFilePath + "\"";
                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                process.StartInfo = startInfo;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                DebugLog.Log(String.Format("Concat of file {0}", ConcatList));
                process.WaitForExit();
                DebugLog.Log("Concat done");
            }
            catch(IOException ioex)
            {
                throw new IOException("Unable to write/access concat files", ioex);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static void ConvertFile(String InputFilePath, String OutputFilePath)
        {
            //TODO add error catching for the ffmpeg process erroring.
            //TODO should probably check or add mp4 to the end of the outfile
            if (!File.Exists(InputFilePath))
                throw new ArgumentException(String.Format("{0} does not exist", InputFilePath));
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.Arguments = "/c " + ffmpegDir + " -i \"" + InputFilePath + "\" -bsf:a aac_adtstoasc -c copy \"" + OutputFilePath + "\"";
                startInfo.FileName = "cmd.exe";
                startInfo.UseShellExecute = false;
                process.StartInfo = startInfo;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                DebugLog.Log(String.Format("Convert of file {0}", InputFilePath));
                process.WaitForExit();
                DebugLog.Log("Convert done");
            }
            catch (IOException ioex)
            {
                throw new IOException("Unable to write/access concat files", ioex);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private string catfile;
        //private string destfilepath;

        //public VideoProcess(String DownloadFile, String DestinaitionFilePath)
        //{
        //    catfile = DownloadFile;
        //    destfilepath = DestinaitionFilePath;
        //}
    }


}
