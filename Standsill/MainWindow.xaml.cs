using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Standsill
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Process.TwitchObject TwitchObj = null;
        private const string QUALITY = "high"; //"medium"
        public MainWindow()
        {
            InitializeComponent();
            HideVideos();
        }

        private void HideVideos()
        {   //1
            gbxVideo1.Visibility = System.Windows.Visibility.Hidden;
            imgVideo1.Visibility = System.Windows.Visibility.Hidden;
            lblVideo1Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo1Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo1Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo1Dwn.Visibility = System.Windows.Visibility.Hidden;
            //2
            gbxVideo2.Visibility = System.Windows.Visibility.Hidden;
            imgVideo2.Visibility = System.Windows.Visibility.Hidden;
            lblVideo2Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo2Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo2Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo2Dwn.Visibility = System.Windows.Visibility.Hidden;
            //3
            gbxVideo3.Visibility = System.Windows.Visibility.Hidden;
            imgVideo3.Visibility = System.Windows.Visibility.Hidden;
            lblVideo3Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo3Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo3Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo3Dwn.Visibility = System.Windows.Visibility.Hidden;
            //4
            gbxVideo4.Visibility = System.Windows.Visibility.Hidden;
            imgVideo4.Visibility = System.Windows.Visibility.Hidden;
            lblVideo4Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo4Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo4Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo4Dwn.Visibility = System.Windows.Visibility.Hidden;
            //5
            gbxVideo5.Visibility = System.Windows.Visibility.Hidden;
            imgVideo5.Visibility = System.Windows.Visibility.Hidden;
            lblVideo5Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo5Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo5Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo5Dwn.Visibility = System.Windows.Visibility.Hidden;
            //6
            gbxVideo6.Visibility = System.Windows.Visibility.Hidden;
            imgVideo6.Visibility = System.Windows.Visibility.Hidden;
            lblVideo6Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo6Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo6Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo6Dwn.Visibility = System.Windows.Visibility.Hidden;
            //7
            gbxVideo7.Visibility = System.Windows.Visibility.Hidden;
            imgVideo7.Visibility = System.Windows.Visibility.Hidden;
            lblVideo7Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo7Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo7Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo7Dwn.Visibility = System.Windows.Visibility.Hidden;
            //8
            gbxVideo8.Visibility = System.Windows.Visibility.Hidden;
            imgVideo8.Visibility = System.Windows.Visibility.Hidden;
            lblVideo8Date.Visibility = System.Windows.Visibility.Hidden;
            lblVideo8Time.Visibility = System.Windows.Visibility.Hidden;
            lblVideo8Title.Visibility = System.Windows.Visibility.Hidden;
            btnVideo8Dwn.Visibility = System.Windows.Visibility.Hidden;
        }

        private void btnLoadChannel_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbxChannel.Text))
                tbxChannel.Text = "Enter Channel Name";
            else
            {
                Process.TwitchObject twObj = new Process.TwitchObject(tbxChannel.Text);
                this.TwitchObj = twObj;
                twObj.RequestVideoList();
                btnChannelNext.IsEnabled = true;
                SetVideos(ref twObj);
            }
        }

        private void tbxChannel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                btnLoadChannel_Click(sender, null);
        }

        private void SetVideos(ref Process.TwitchObject TwitchObj)
        {
            int videoCnt = TwitchObj.VideoLinks.videos.Count();
            TimeSpan runtime;
            if(videoCnt >= 1)
            {
                gbxVideo1.Visibility = System.Windows.Visibility.Visible;
                imgVideo1.Visibility = System.Windows.Visibility.Visible;
                lblVideo1Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo1Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo1Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo1Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo1.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[0].preview, UriKind.RelativeOrAbsolute));
                lblVideo1Title.Content = TwitchObj.VideoLinks.videos[0].title;
                lblVideo1Date.Content = TwitchObj.VideoLinks.videos[0].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[0].length);
                lblVideo1Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 2)
            {
                gbxVideo2.Visibility = System.Windows.Visibility.Visible;
                imgVideo2.Visibility = System.Windows.Visibility.Visible;
                lblVideo2Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo2Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo2Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo2Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo2.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[1].preview, UriKind.RelativeOrAbsolute));
                lblVideo2Title.Content = TwitchObj.VideoLinks.videos[1].title;
                lblVideo2Date.Content = TwitchObj.VideoLinks.videos[1].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[1].length);
                lblVideo2Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 3)
            {
                gbxVideo3.Visibility = System.Windows.Visibility.Visible;
                imgVideo3.Visibility = System.Windows.Visibility.Visible;
                lblVideo3Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo3Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo3Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo3Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo3.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[2].preview, UriKind.RelativeOrAbsolute));
                lblVideo3Title.Content = TwitchObj.VideoLinks.videos[2].title;
                lblVideo3Date.Content = TwitchObj.VideoLinks.videos[2].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[2].length);
                lblVideo3Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 4)
            {
                gbxVideo4.Visibility = System.Windows.Visibility.Visible;
                imgVideo4.Visibility = System.Windows.Visibility.Visible;
                lblVideo4Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo4Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo4Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo4Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo4.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[3].preview, UriKind.RelativeOrAbsolute));
                lblVideo4Title.Content = TwitchObj.VideoLinks.videos[3].title;
                lblVideo4Date.Content = TwitchObj.VideoLinks.videos[3].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[3].length);
                lblVideo4Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 5)
            {
                gbxVideo5.Visibility = System.Windows.Visibility.Visible;
                imgVideo5.Visibility = System.Windows.Visibility.Visible;
                lblVideo5Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo5Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo5Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo5Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo5.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[4].preview, UriKind.RelativeOrAbsolute));
                lblVideo5Title.Content = TwitchObj.VideoLinks.videos[4].title;
                lblVideo5Date.Content = TwitchObj.VideoLinks.videos[4].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[4].length);
                lblVideo5Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 6)
            {
                gbxVideo6.Visibility = System.Windows.Visibility.Visible;
                imgVideo6.Visibility = System.Windows.Visibility.Visible;
                lblVideo6Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo6Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo6Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo6Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo6.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[5].preview, UriKind.RelativeOrAbsolute));
                lblVideo6Title.Content = TwitchObj.VideoLinks.videos[5].title;
                lblVideo6Date.Content = TwitchObj.VideoLinks.videos[5].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[5].length);
                lblVideo6Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 7)
            {
                gbxVideo7.Visibility = System.Windows.Visibility.Visible;
                imgVideo7.Visibility = System.Windows.Visibility.Visible;
                lblVideo7Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo7Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo7Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo7Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo7.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[6].preview, UriKind.RelativeOrAbsolute));
                lblVideo7Title.Content = TwitchObj.VideoLinks.videos[6].title;
                lblVideo7Date.Content = TwitchObj.VideoLinks.videos[6].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[6].length);
                lblVideo7Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
            if(videoCnt >= 8)
            {
                gbxVideo8.Visibility = System.Windows.Visibility.Visible;
                imgVideo8.Visibility = System.Windows.Visibility.Visible;
                lblVideo8Date.Visibility = System.Windows.Visibility.Visible;
                lblVideo8Time.Visibility = System.Windows.Visibility.Visible;
                lblVideo8Title.Visibility = System.Windows.Visibility.Visible;
                btnVideo8Dwn.Visibility = System.Windows.Visibility.Visible;
                imgVideo8.Source = new BitmapImage(new Uri(TwitchObj.VideoLinks.videos[7].preview, UriKind.RelativeOrAbsolute));
                lblVideo8Title.Content = TwitchObj.VideoLinks.videos[7].title;
                lblVideo8Date.Content = TwitchObj.VideoLinks.videos[7].recorded_at;
                runtime = TimeSpan.FromSeconds(TwitchObj.VideoLinks.videos[7].length);
                lblVideo8Time.Content = runtime.ToString(@"hh\:mm\:ss");
            }
        }

        private void btnChannelNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.TwitchObj == null)
                return;
            if (String.IsNullOrEmpty(this.TwitchObj.VideoLinks._links.next))
                return;
            this.TwitchObj.NextVideoList();
            if (!String.IsNullOrEmpty(this.TwitchObj.VideoLinks._links.prev))
                btnChannelPrev.IsEnabled = true;
            SetVideos(ref this.TwitchObj);
        }

        private void btnChannelPrev_Click(object sender, RoutedEventArgs e)
        {
            if (this.TwitchObj == null)
                return;
            if (String.IsNullOrEmpty(this.TwitchObj.VideoLinks._links.prev))
            {
                btnChannelPrev.IsEnabled = false;
                return;
            }
            this.TwitchObj.PrevVideoList();
            if (String.IsNullOrEmpty(this.TwitchObj.VideoLinks._links.prev))
                btnChannelPrev.IsEnabled = false;
            SetVideos(ref this.TwitchObj);
        }

        #region Download buttons
        private void btnVideo1Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[0]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[0]._id, QUALITY);
        }

        private void btnVideo2Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[1]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[1]._id, QUALITY);
        }

        private void btnVideo3Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[2]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[2]._id, QUALITY);
        }

        private void btnVideo4Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[3]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[3]._id, QUALITY);
        }

        private void btnVideo5Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[4]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[4]._id, QUALITY);
        }

        private void btnVideo6Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[5]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[5]._id, QUALITY);
        }

        private void btnVideo7Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[6]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[6]._id, QUALITY);
        }

        private void btnVideo8Dwn_Click(object sender, RoutedEventArgs e)
        {
            if (TwitchObj == null || TwitchObj.VideoLinks.videos.Count <= 0)
                throw new ArgumentException("No video links");
            TwitchObj.RequestVideoAccess(TwitchObj.VideoLinks.videos[7]._id);
            //TODO create enum for video qualities
            TwitchObj.WriteM3U(TwitchObj.VideoLinks.videos[7]._id, QUALITY);
        }
        #endregion

        private void tbxLiveName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                btnWatchLive_Click(sender, null);
        }
        private void btnWatchLive_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(tbxLiveName.Text))
            {
                tbxLiveName.Text = "Enter Channel Name";
            }
            else
            {
                lblLiveOut.Content = tbxLiveName.Text;
                //me_Player.Source = new Uri("http://player.twitch.tv/?channel=" + tbxLiveName.Text);
                //me_Player.Play();
                //wbVideo.Source = 

                //Process.TwitchObject twObj = new Process.TwitchObject(tbxLiveName.Text);
                //this.TwitchObj = twObj;
                //twObj.RequestVideoList();
                ////lblLiveOut.Content = twObj.VideoLinks.videos.Count;
                ////lblLiveOut.Content = twObj.VideoLinks.videos[0].url;
                //int idNum = 0;
                //for (int i = 0; i < twObj.VideoLinks.videos.Count; i++)
                //{
                //    DebugLog.Log(twObj.VideoLinks.videos[i].url + " " + twObj.VideoLinks.videos[i].status);
                //    if (twObj.VideoLinks.videos[i].status == "recording")
                //    {
                //        idNum = i;
                //        break;
                //    }
                //}

                //twObj.RequestVideoAccess(twObj.VideoLinks.videos[idNum]._id);
                //lblLiveOut.Content = twObj.GetLiveURL(tbxLiveName.Text);
            }
        }
    }
}
