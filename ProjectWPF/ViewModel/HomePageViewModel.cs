using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using OxyPlot;
using ProjectWPF.Model;
using ProjectWPF.Extensions;
using ProjectWPF.Messages;
using System.Windows;

namespace ProjectWPF.ViewModel
{

    class HomePageViewModel : BaseViewModel
    {

        private DialogService dialogService;

        public HomePageViewModel()
        {
            LadenMaanden();
            LadenJaren();

            ToonDiagramCommand = new BaseCommand(toonDiagram);
            ToonInfoCommand = new BaseCommand(toonInfoPerMaand);
            ToonEvolutieCommand = new BaseCommand(toonEvolutieDiagram);
            dialogService = new DialogService();
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

        private Temperatuur selectedJaar;
        public Temperatuur SelectedJaar
        {
            get
            {
                return selectedJaar;
            }
            set
            {
                selectedJaar = value;
                NotifyPropertyChanged();
            }
        }

        private Temperatuur selectedJaarInfo;
        public Temperatuur SelectedJaarInfo
        {
            get
            {
                return selectedJaarInfo;
            }
            set
            {
                selectedJaarInfo = value;
                NotifyPropertyChanged();
            }
        }

        private Maand selectedMaand;
        public Maand SelectedMaand
        {
            get
            {
                return selectedMaand;
            }
            set
            {
                selectedMaand = value;
                NotifyPropertyChanged();
            }
        }

        private Maand selectedMaandEvolutie;
        public Maand SelectedMaandEvolutie
        {
            get
            {
                return selectedMaandEvolutie;
            }
            set
            {
                selectedMaandEvolutie = value;
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

        private void LadenMaanden()
        {
            MaandDataService maandDS = new MaandDataService();
            Maanden = new ObservableCollection<Maand>(maandDS.GetMaanden());
        }

        private ObservableCollection<Temperatuur> jaren;
        public ObservableCollection<Temperatuur> Jaren
        {
            get
            {
                return jaren;
            }
            set
            {
                jaren = value;
                NotifyPropertyChanged();
            }
        }

        private void LadenJaren()
        {
            TemperatuurDataService temperatuurDS = new TemperatuurDataService();
            Jaren = new ObservableCollection<Temperatuur>(temperatuurDS.GetJaren());
        }
        
        private ICommand toonEvolutieCommand;
        public ICommand ToonEvolutieCommand
        {
            get
            {
                return toonEvolutieCommand;
            }

            set
            {
                toonEvolutieCommand = value;
            }
        }


        private ICommand windkrachtOverzichtCommand;
        public ICommand WindkrachtOverzichtCommand
        {
            get
            {
                return windkrachtOverzichtCommand;
            }

            set
            {
                windkrachtOverzichtCommand = value;
            }
        }

        private ICommand toonDiagramCommand;
        public ICommand ToonDiagramCommand
        {
            get
            {
                return toonDiagramCommand;
            }

            set
            {
                toonDiagramCommand = value;
            }
        }

        private ICommand toonInfoCommand;
        public ICommand ToonInfoCommand
        {
            get
            {
                return toonInfoCommand;
            }

            set
            {
                toonInfoCommand = value;
            }
        }
        
        private void toonDiagram()
        {
            if (SelectedJaar != null)
            {
                TemperatuurDataService temperatuurDS = new TemperatuurDataService();
                Temperaturen = new ObservableCollection<Temperatuur>(temperatuurDS.GetTemperaturenPerJaar(SelectedJaar));
                Messenger.Default.Send<ObservableCollection<Temperatuur>>(Temperaturen);
            }
            else
            {
                MessageBox.Show("U heeft geen jaar geselecteerd!", "Fout!");
            }
        }

        private void toonInfoPerMaand ()
        { 
            if (selectedJaar != null && SelectedMaand != null )
            {
                TemperatuurDataService temperatuurDS = new TemperatuurDataService();
                SelectedJaar.MaandId = SelectedMaand.Id;
                if (temperatuurDS.GetTemperatuur(SelectedJaar).Count > 0)
                {
                    Temperaturen = new ObservableCollection<Temperatuur>(temperatuurDS.GetTemperatuur(SelectedJaar));
                    Messenger.Default.Send<ObservableCollection<Temperatuur>>(Temperaturen);
                }
                else
                {
                    MessageBox.Show("Voor deze keuze is er geen data beschikbaar!", "Fout!");
                }  
            }
            else
            {
                MessageBox.Show("U heeft geen maand en/of jaar geselecteerd!", "Fout!");
            }
        }

        private void toonEvolutieDiagram ()
        {
            if (SelectedMaand != null)
            {
                TemperatuurDataService temperatuurDS = new TemperatuurDataService();
                Temperaturen = new ObservableCollection<Temperatuur>(temperatuurDS.GetTemperaturenPerMaand(SelectedMaand));
                Messenger.Default.Send<ObservableCollection<Temperatuur>>(Temperaturen);
            }
            else
            {
                MessageBox.Show("U heeft geen maand geselecteerd!", "Fout!");
            }
        }
    }
}