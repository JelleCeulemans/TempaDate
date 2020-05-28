using LiveCharts;
using LiveCharts.Wpf;
using OxyPlot;
using ProjectWPF.Extensions;
using ProjectWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.ViewModel
{
    class ChartPageViewModel : BaseViewModel
    {
        public ChartPageViewModel ()
        {
            Messenger.Default.Register<ObservableCollection<Temperatuur>>(this, OnReceivedTemperaturen);
        }

        private ObservableCollection<Temperatuur> temperaturen;
        public ObservableCollection<Temperatuur> Temperaturen
        {
            get
            {
                return temperaturen;
            }
            set
            {
                temperaturen = value;
                NotifyPropertyChanged();
            }
        }


        private SeriesCollection punten;
        public SeriesCollection Punten
        {
            get { return punten; }
            set
            {
                punten = value;
                NotifyPropertyChanged();
            }
        }
        private string[] labels;
        public string[] Labels
        {

            get { return labels; }
            set
            {
                labels = value;
                NotifyPropertyChanged();
            }
        }

        private string xtekst;
        public string Xtekst
        {
            get { return xtekst; }
            set
            {
                xtekst = value;
                NotifyPropertyChanged();
            }
        }

        private void OnReceivedTemperaturen(ObservableCollection<Temperatuur> temperaturen)
        {
            Temperaturen = temperaturen;

            ChartValues<int> minimum = new ChartValues<int>();
            ChartValues<int> gemiddelde = new ChartValues<int>();
            ChartValues<int> maximum = new ChartValues<int>();
            string[] labellijst = new string[Temperaturen.Count];
            int teller = 0;
            string[] maanden = new string[] { "Januari", "Februari", "Maart", "April", "Mei", "Juni", "Juli", "Augustus", "September", "Oktober", "November", "December" };
            foreach (Temperatuur temp in Temperaturen)
            {
                minimum.Add(temp.Minimum);
                gemiddelde.Add(temp.Gemiddelde);
                maximum.Add(temp.Maximum);
                labellijst[teller] = Convert.ToString(temp.Jaar);
                teller++;

            }

            if (Temperaturen[0].Jaar == Temperaturen[Temperaturen.Count() - 1].Jaar)
            {
                Xtekst = "Temperaturen voor het jaar " + Temperaturen[0].Jaar;
                Labels = maanden;
            }
            else
            {
                Xtekst = "De evolutie van de maand " + maanden[Temperaturen[0].MaandId -1];
                Labels = labellijst; 
            }

            
            SeriesCollection sc = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Laagste",
                    Values = minimum,
                },
                new LineSeries
                {
                    Title = "Gemiddelde",
                    Values = gemiddelde,

                },
                new LineSeries
                {
                    Title = "Maximum",
                    Values = maximum,

                }
            };
            Punten = sc;
        }
    }
}
