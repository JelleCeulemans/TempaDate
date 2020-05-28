using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWPF.ViewModel;

namespace ProjectWPF
{
    class ViewModelLocator
    {
        private static HomePageViewModel homePageViewModel = new HomePageViewModel();
        private static ChartPageViewModel chartPageViewModel = new ChartPageViewModel();
        private static InfoPageViewModel infoPageViewModel = new InfoPageViewModel();
        private static AllChartPageViewModel allChartPageViewModel = new AllChartPageViewModel();
        private static VideoPageViewModel videoPageViewModel = new VideoPageViewModel();
        private static MaandenDetailPageViewModel maandenDetailPageViewModel = new MaandenDetailPageViewModel();
        private static TemperatuurDetailPageViewModel temperatuurDetailPageViewModel = new TemperatuurDetailPageViewModel();
        private static WindkrachtenDetailPageViewModel windkrachtenDetailPageViewModel = new WindkrachtenDetailPageViewModel();
        private static MaandenEditeerWindowViewModel maandenEditeerWindowViewModel = new MaandenEditeerWindowViewModel();
        private static WindkrachtenEditeerWindowViewModel windkrachtenEditeerWindowViewModel = new WindkrachtenEditeerWindowViewModel();
        private static TemperaturenEditeerWindowViewModel temperaturenEditeerWindowViewModel = new TemperaturenEditeerWindowViewModel();

        public static HomePageViewModel HomePageViewModel
        {
            get
            {
                return homePageViewModel;
            }
        }

        public static ChartPageViewModel ChartPageViewModel
        {
            get
            {
                return chartPageViewModel;
            }
        }

        public static InfoPageViewModel InfoPageViewModel
        {
            get
            {
                return infoPageViewModel;
            }
        }

        public static AllChartPageViewModel AllChartPageViewModel
        {
            get
            {
                return allChartPageViewModel;
            }
        }

        public static VideoPageViewModel VideoPageViewModel
        {
            get
            {
                return videoPageViewModel;
            }
        }

        public static MaandenDetailPageViewModel MaandenDetailPageViewModel
        {
            get
            {
                return maandenDetailPageViewModel;
            }
        }

        public static TemperatuurDetailPageViewModel TemperatuurDetailPageViewModel
        {
            get
            {
                return temperatuurDetailPageViewModel;
            }
        }

        public static WindkrachtenDetailPageViewModel WindkrachtenDetailPageViewModel
        {
            get
            {
                return windkrachtenDetailPageViewModel;
            }
        }

        public static MaandenEditeerWindowViewModel MaandenEditeerWindowViewModel
        {
            get
            {
                return maandenEditeerWindowViewModel;
            }
        }

        public static WindkrachtenEditeerWindowViewModel WindkrachtenEditeerWindowViewModel
        {
            get
            {
                return windkrachtenEditeerWindowViewModel;
            }
        }

        public static TemperaturenEditeerWindowViewModel TemperaturenEditeerWindowViewModel
        {
            get
            {
                return temperaturenEditeerWindowViewModel;
            }
        }
    }
}
