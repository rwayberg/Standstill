using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standsill
{
    class TwitchObjects
    {
        #region Video Link
        public class Links
        {
            public string self { get; set; }
            public string next { get; set; }
            public string prev { get; set; }
        }

        public class Fps
        {
            public double audio_only { get; set; }
            public double medium { get; set; }
            public double mobile { get; set; }
            public double high { get; set; }
            public double low { get; set; }
            public double chunked { get; set; }
        }

        public class Resolutions
        {
            public string medium { get; set; }
            public string mobile { get; set; }
            public string high { get; set; }
            public string low { get; set; }
            public string chunked { get; set; }
        }

        public class Links2
        {
            public string self { get; set; }
            public string channel { get; set; }
        }

        public class Channel
        {
            public string name { get; set; }
            public string display_name { get; set; }
        }

        public class Video
        {
            public string title { get; set; }
            public object description { get; set; }
            public object broadcast_id { get; set; }
            public string status { get; set; }
            public string tag_list { get; set; }
            public string _id { get; set; }
            public string recorded_at { get; set; }
            public string game { get; set; }
            public float length { get; set; }
            public string preview { get; set; }
            public string url { get; set; }
            public int views { get; set; }
            public Fps fps { get; set; }
            public Resolutions resolutions { get; set; }
            public string broadcast_type { get; set; }
            public Links2 _links { get; set; }
            public Channel channel { get; set; }
        }

        public class VideoLinkObject
        {
            public int _total { get; set; }
            public Links _links { get; set; }
            public List<Video> videos { get; set; }
        }
        #endregion

        #region Access Token
        public class AccessToken
        {
            public string token
            {
                get;
                //get
                //{
                //    return this.token.Replace("\\\"", "\"");
                //}
                set;
            }
            public string sig { get; set; }
        }
        #endregion
    }
}
