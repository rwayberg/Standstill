using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace Standsill
{
    class DownloadProcess
    {
        const int DOWNLOAD_THREAD_COUNT = 10;

        private List<BackgroundWorker> WorkList;
        private BackgroundWorker videoWorker;
        private BackgroundWorker convertWorker;
        private List<String> FileList;
        private List<String> downloadedFiles;
        private string downloadPath;
        private int fileCount;
        private string CatFile;
        private Object lockObj;
        private string catVideoName;
        private string finalVideoName;

        public DownloadProcess(List<String> ListOfFiles)
        {
            if (ListOfFiles.Count == 0)
                throw new ArgumentException("No files for download");
            FileList = ListOfFiles;
            downloadPath = @"T:\Videos\twitch"; //Directory.GetCurrentDirectory() + "\\Videos\\Download";
            CatFile = downloadPath + "\\" + GetVodName(FileList.First()) + ".txt";
            catVideoName = downloadPath + "\\" + GetVodName(FileList.First()) + ".ts";
            finalVideoName = downloadPath + "\\" + GetVodName(FileList.First()) + ".mp4";
            DebugLog.Log("Cat file " + CatFile);
            InitializeWorkers();
            fileCount = 0;
            lockObj = new Object();
            downloadedFiles = new List<string>();
        }

        public void StartDownload()
        {
            CreateConcatFile();
            foreach (BackgroundWorker worker in WorkList)
            {
                DebugLog.Log("Spawn worker");
                worker.RunWorkerAsync(GetNextFile());
                System.Threading.Thread.Sleep(50);
            }
        }

        private void InitializeWorkers()
        {
            WorkList = new List<BackgroundWorker>(DOWNLOAD_THREAD_COUNT);
            BackgroundWorker worker;
            for (int i = 1; i <= DOWNLOAD_THREAD_COUNT; i++)
            {
                worker = new BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                WorkList.Add(worker);
            }
            videoWorker = new BackgroundWorker();
            videoWorker.DoWork += videoWorker_DoWork;
            videoWorker.RunWorkerCompleted += videoWorker_RunWorkerCompleted;
            convertWorker = new BackgroundWorker();
            convertWorker.DoWork += convertWorker_DoWork;
            convertWorker.RunWorkerCompleted += convertWorker_RunWorkerCompleted;
        }

        private void CreateConcatFile()
        {
            foreach (string line in FileList)
            {
                string file = downloadPath + "\\" + GetFileNameFromURL(line);
                File.AppendAllText(CatFile, String.Format("file '{0}'\r\n", file));
            }
        }

        void convertWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //TODO need to have the update of the UI but we are done
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.Dispose();
            foreach(String file in downloadedFiles)
            {
                File.Delete(file);
            }
            File.Delete(CatFile);
            File.Delete(catVideoName);
        }

        void convertWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            VideoProcess.ConvertFile(catVideoName, finalVideoName);
        }

        void videoWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.Dispose();
            convertWorker.RunWorkerAsync();
        }

        void videoWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            DebugLog.Log(String.Format("Start video process {0} {1}", catVideoName, finalVideoName));
            VideoProcess.ConcatFiles(CatFile, catVideoName);
        }

        private String GetVodName(String URL)
        {
            if (String.IsNullOrEmpty(URL))
                throw new ArgumentException("Invalid URL ");
            String[] split = URL.Split('/');
            for(int i = 0; i < split.Count(); i++)
            {
                if (split[i].Contains("vods_"))
                    return split[i];
            }
            return "";
            //int vi = URL.IndexOf("vods_");
            //int endi = URL.IndexOf("/", vi + 1);
            //return URL.Substring(vi, endi - 1);
        }

        private String GetFileNameFromURL(String URL)
        {
            if (String.IsNullOrEmpty(URL))
                throw new ArgumentException("Cannot get file name");
            int firstIndex = URL.LastIndexOf("/");
            int lastIndex = URL.IndexOf("?");
            DebugLog.Log(URL.Substring(firstIndex + 1, (lastIndex - (firstIndex + 1))));
            return URL.Substring(firstIndex + 1, (lastIndex - (firstIndex + 1)));
        }

        private void DownloadFile(String FileURL, String FileName)
        {
            try
            {
                //lock (lockObj)
                //{
                    using (WebClient client = new WebClient())
                    {
                        fileCount++;
                        //string downPath = downloadPath + "\\" + fileCount.ToString() + "." + FileName;
                        string downPath = downloadPath + "\\" + FileName;
                        //File.AppendAllText(CatFile, String.Format("file '{0}'\r\n", downPath));
                        downloadedFiles.Add(downPath);
                        client.DownloadFile(FileURL, downPath);
                    }
                //}
            }
            catch(Exception ex)
            {
                DebugLog.Log(String.Format("Download error on {0}\r\n{1}", FileURL, ex.ToString()));
            }
        }

        private String GetNextFile()
        {
            lock (lockObj)
            {
                if (FileList.Count > 0)
                {
                    string next = FileList.First();
                    FileList.Remove(next);
                    return next;
                }
                else
                    return "";
            }
        }

        private void ProcessFile(BackgroundWorker worker, RunWorkerCompletedEventArgs e)
        {
            String next = GetNextFile();
            if (!String.IsNullOrEmpty(next))
                worker.RunWorkerAsync(next);
            else
            {
                DebugLog.Log("Worker done -" + System.DateTime.Now.ToString());
                worker.Dispose();
                WorkList.Remove(worker);
                if (WorkList.Count == 0)
                {
                    //File.AppendAllLines(CatFile, downloadedFiles);
                    DebugLog.Log("All workders done");
                    videoWorker.RunWorkerAsync();
                }
            }
            //if(FileList.Count > 0)
            //{
            //    //DebugLog.Log("Download file");
            //    //DownloadFile(FileList.First(), GetFileNameFromURL(FileList.First()));
            //    worker.RunWorkerAsync(FileList.First());
            //    FileList.RemoveAt(0); //TODO this will probably not work since the threads will collide on accessing the list
            //}
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            ProcessFile(worker, e);
            //throw new NotImplementedException();
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        { //argument should be the file url
            //BackgroundWorker worker = sender as BackgroundWorker;
            //ProcessFile(worker, e);
            DownloadFile(e.Argument.ToString(), GetFileNameFromURL(e.Argument.ToString()));
        }
    }
}
