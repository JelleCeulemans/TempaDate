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
    class InfoPageViewModel : BaseViewModel
    {
        public InfoPageViewModel ()
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

        private ObservableCollection<Windkracht> windkrachten;
        public ObservableCollection<Windkracht> Windkrachten
        {
            get
            {
                return windkrachten;
            }
            set
            {
                windkrachten = value;
                NotifyPropertyChanged();
            }
        }

        private ObservableCollection<Maand> maanden;
        public ObservableCollection<Maand> Maanden
        {
            get
            {
                return maanden;
            }
            set
            {
                maanden = value;
                NotifyPropertyChanged();
            }
        }

        private Windkracht windkracht;
        public Windkracht Windkracht
        {
            get
            {
                return windkracht;
            }
            set
            {
                windkracht = value;
                NotifyPropertyChanged();
            }
        }


        private Temperatuur temperatuur;
        public Temperatuur Temperatuur
        {
            get
            {
                return temperatuur;
            }
            set
            {
                temperatuur = value;
                NotifyPropertyChanged();
            }
        }

        private void OnReceivedTemperaturen (ObservableCollection<Temperatuur> temperatuur)
        {
            Temperaturen = temperatuur;

            Windkracht wk1 = new Windkracht();
            foreach (Temperatuur temp in Temperaturen)
            {
                wk1.Id = temp.WindkrachtId;
            }

            WindkrachtDataService windkrachtDS = new WindkrachtDataService();
            Windkrachten = new ObservableCollection<Windkracht>(windkrachtDS.GetWindkracht(wk1));

            MaandDataService maandDS = new MaandDataService();
            Maanden = new ObservableCollection<Maand>(maandDS.GetMaandenWhere(Temperaturen[0]));
        }
    }
}
