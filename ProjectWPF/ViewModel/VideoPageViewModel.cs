using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.ViewModel
{
    class VideoPageViewModel : BaseViewModel
    {
        public VideoPageViewModel ()
        {
            printVideo();
        }

        private string video = AppDomain.CurrentDomain.BaseDirectory + "opwarming.mp4";

        public String Video
        {
            get
            {
                return video;
            }

            set
            {
                video = value;
                NotifyPropertyChanged();
            }
        }

        private void printVideo()
        {
            Debug.WriteLine(video);
        }
    }
}
