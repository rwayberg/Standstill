using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace Standsill
{
    class Process
    {
        public static String APIRequest(String URL)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(URL);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream);
                //TODO Error handling
                return sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                DebugLog.Log(String.Format("Request error for {0}\r\n{1}", URL, ex.ToString()));
                return null;
            }
        }

        public class TwitchObject
        {
            //TODO settings to replace const
            const int VIDEO_LIMIT = 8;
            const bool BROADCAST = true;
            private String m_channel;
            private TwitchObjects.AccessToken m_access;
            private TwitchObjects.VideoLinkObject m_videoLink;

            public TwitchObjects.AccessToken Access
            {
                get { return m_access; }
                private set { m_access = value; } 
            }

            public TwitchObjects.VideoLinkObject VideoLinks
            {
                get { return m_videoLink; }
                private set { m_videoLink = value; }
            }

            public TwitchObject(String ChannelName)
            {
                this.m_channel = ChannelName.ToLower();
                this.m_access = null;
                this.m_videoLink = null;
            }

            public void RequestVideoList()
            {
                if (String.IsNullOrEmpty(this.m_channel))
                    throw new ArgumentException("Channel name missing");

                String videoURL = "https://api.twitch.tv/kraken/channels/" + this.m_channel + "/videos?limit=" + VIDEO_LIMIT.ToString() + "&broadcasts=" + BROADCAST.ToString().ToLower();
                String videoResp = APIRequest(videoURL);
                this.m_videoLink = JsonConvert.DeserializeObject<TwitchObjects.VideoLinkObject>(videoResp);
            }

            public void NextVideoList()
            {
                if (this.m_videoLink == null)
                    throw new ArgumentException("No video link object found");
                if (String.IsNullOrEmpty(this.m_videoLink._links.next))
                    return;
                String nextResp = APIRequest(this.m_videoLink._links.next);
                this.m_videoLink = JsonConvert.DeserializeObject<TwitchObjects.VideoLinkObject>(nextResp);
            }

            public void PrevVideoList()
            {
                if (this.m_videoLink == null)
                    throw new ArgumentException("No video link object found");
                if (String.IsNullOrEmpty(this.m_videoLink._links.prev))
                    return;

                String prevResp = APIRequest(this.m_videoLink._links.prev);
                this.m_videoLink = JsonConvert.DeserializeObject<TwitchObjects.VideoLinkObject>(prevResp);
            }

            public void RequestVideoAccess(String VideoID)
            {
                if (String.IsNullOrEmpty(this.m_channel))
                    throw new ArgumentException("Channel name missing");
                if (String.IsNullOrEmpty(VideoID))
                    throw new ArgumentException("Invalid Video ID: " + VideoID);
                VideoID = VideoID.Substring(1); //remove the v or a at the start of the id
                String accessURL = "https://api.twitch.tv/api/vods/" + VideoID + "/access_token";
                String accessResponse = APIRequest(accessURL);
                //TODO handle errors
                this.m_access = JsonConvert.DeserializeObject<TwitchObjects.AccessToken>(accessResponse);
            }

            public void WriteM3U(String ID, String Quality)
            {
                if (String.IsNullOrEmpty(this.m_access.token) || String.IsNullOrEmpty(this.m_access.sig))
                    throw new ArgumentException("Missing token or sig");
                ID = ID.Substring(1); //remove the v or a at the start of the id
                String m3utURL = "http://usher.twitch.tv/vod/" + ID + "?nauth=" + System.Net.WebUtility.UrlEncode(this.m_access.token) + "&nauthsig=" + this.m_access.sig;
                String resp = APIRequest(m3utURL);
                //TODO setup quality  
                String urlFile = ParseM3U8(Quality, resp);
                string filepath = Directory.GetCurrentDirectory() + "\\listfile.m3u8";
                String indexLink = ParseStreamLink(urlFile);
                //urlFile = urlFile.Replace(",\n", "," + indexLink);
                String m3 = APIRequest(urlFile);
                m3 = m3.Replace(",\n", "," + indexLink);
                File.WriteAllText(filepath, m3);
                //TODO remove call
                CreateM3List(filepath);
            }

            public String GetLiveURL(string Channel)
            {
                if (String.IsNullOrEmpty(this.m_access.token) || String.IsNullOrEmpty(this.m_access.sig))
                    throw new ArgumentException("Missing token or sig");
                string m3URL = String.Format(@"http://usher.twitch.tv/api/channel/hls/{0}.m3u8?player=twitchweb&token={1}&sig={2}&allow_audio_only=true&allow_source=true&type=any&p=44818889922",
                    Channel, System.Net.WebUtility.UrlEncode(this.m_access.token), this.m_access.sig);
                String resp = APIRequest(m3URL);
                return resp;
            }

            private void CreateM3List(string ListFilePath)
            {
                if (!File.Exists(ListFilePath))
                    throw new ArgumentException(String.Format("File at {0} does not exist", ListFilePath));
                List<string> lines = File.ReadLines(ListFilePath).ToList<string>();
                List<string> links = new List<string>();
                bool startedLink = false;
                string conLink = "";
                string endtime = "";
                foreach(string line in lines)
                {
                    if(line.Contains("start_offset=0"))
                    {
                        if(!String.IsNullOrEmpty(conLink))
                        {
                            links.Add(String.Format("{0}?start_offset=0&end_offset={1}", conLink, endtime));
                        }
                        conLink = line.Substring(0, line.IndexOf("?start_offset"));
                        //startedLink = true;
                    }
                    else if (line.Contains("end_offset="))
                    {
                        endtime = line.Substring(line.IndexOf("end_offset=") + 11); //offset for length of phrase
                    }
                }
                File.WriteAllLines(Directory.GetCurrentDirectory() + "\\linklist.txt", links.ToArray());
                DownloadProcess dp = new DownloadProcess(links);
                DebugLog.Log("Download process start -" + System.DateTime.Now.ToString());
                dp.StartDownload();
            }

            private string ParseM3U8(string Quality, string ListText)
            {
                string strQuality = "VIDEO=\"" + Quality.ToLower() + "\"";
                string urlSub = ListText.Substring(ListText.IndexOf(strQuality) + strQuality.Length);
                urlSub = urlSub.Substring(0, urlSub.IndexOf('#'));
                return urlSub;
            }

            private string ParseStreamLink(string Link)
            {
                int index = Link.LastIndexOf('/'); //Link.IndexOf("/index");
                if (index <= 0)
                {
                    DebugLog.Log("Cannot find m3u8: " + Link);
                    throw new Exception("Unable to parse stream link");
                }
                string sub = Link.Substring(0, index + 1);
                //string sub = Link.Substring(0, Link.IndexOf("index-dvr.m3u8"));
                if (sub.Length <= 0)
                    return "";
                return sub;
            }
        }

    }
}
