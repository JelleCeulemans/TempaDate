using ProjectWPF.Extensions;
using ProjectWPF.Messages;
using ProjectWPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectWPF.ViewModel
{
    class TemperatuurDetailPageViewModel : BaseViewModel
    {
        DialogService dialogservice = null;
        public TemperatuurDetailPageViewModel ()
        {
            LadenTemperaturen();
            dialogservice = new DialogService();
            ToevoegCommand = new BaseCommand(TemperatuurToevoegen);
            WijzigCommand = new BaseCommand(TemperatuurWijzigen);
            Messenger.Default.Register<TemperatuurMessage>(this, OnMessageReceived);
        }

        private void LadenTemperaturen()
        {
            TemperatuurDataService temperatuurDS = new TemperatuurDataService();
            Temperaturen = temperatuurDS.GetTemperaturen();
        }

        private Temperatuur selectedTemperatuur;
        public Temperatuur SelectedTemperatuur
        {
            get
            {
                return selectedTemperatuur;
            }
            set
            {
                selectedTemperatuur = value;
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

        private ICommand toevoegCommand;
        public ICommand ToevoegCommand
        {
            get
            {
                return toevoegCommand;
            }

            set
            {
                toevoegCommand = value;
            }
        }

        private ICommand wijzigCommand;
        public ICommand WijzigCommand
        {
            get
            {
                return wijzigCommand;
            }

            set
            {
                wijzigCommand = value;
            }
        }

        private void TemperatuurToevoegen()
        {
            Temperatuur temp = new Temperatuur();
            Messenger.Default.Send<Temperatuur>(temp);
            dialogservice.ShowTemperaturenEditeer();
        }

        private void TemperatuurWijzigen()
        {
            if (SelectedTemperatuur != null)
            {
                Messenger.Default.Send<Temperatuur>(SelectedTemperatuur);
                dialogservice.ShowTemperaturenEditeer();
            }
            else
            {
                MessageBox.Show("U moet eerst een temperatuur selecteren!", "Fout!");
            }
        }

        private void OnMessageReceived(TemperatuurMessage message)
        {
            LadenTemperaturen();
            dialogservice.CloseTemperatuurEditeer();
        }
    }
}
