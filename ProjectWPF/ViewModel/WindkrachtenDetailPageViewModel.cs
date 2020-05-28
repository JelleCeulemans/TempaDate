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
    class WindkrachtenDetailPageViewModel : BaseViewModel
    {
        DialogService dialogservice = null;
        public WindkrachtenDetailPageViewModel()
        {
            LadenWindkrachten();
            dialogservice = new DialogService();
            ToevoegCommand = new BaseCommand(WindkrachtToevoegen);
            UpdateCommand = new BaseCommand(UpdateWindkracht);
            
            Messenger.Default.Register<WindkrachtMessage>(this, OnMessageReceived);
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


        private void LadenWindkrachten()
        {
            WindkrachtDataService windkrachtDS = new WindkrachtDataService();
            Windkrachten = new ObservableCollection<Windkracht>(windkrachtDS.GetWindkrachten());
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

        private ICommand updateCommand;
        public ICommand UpdateCommand
        {
            get
            {
                return updateCommand;
            }

            set
            {
                updateCommand = value;
            }
        }

        private Windkracht selectedWindkracht;
        public Windkracht SelectedWindkracht
        {
            get
            {
                return selectedWindkracht;
            }
            set
            {
                selectedWindkracht = value;
                NotifyPropertyChanged();
            }
        }

        private void WindkrachtToevoegen ()
        {
            Windkracht windkracht = new Windkracht();
            Messenger.Default.Send<Windkracht>(windkracht);
            dialogservice.ShowWindkrachtenEditeer();

        }

        private void UpdateWindkracht ()
        {
            if (SelectedWindkracht != null)
            {
                Messenger.Default.Send<Windkracht>(SelectedWindkracht);
                dialogservice.ShowWindkrachtenEditeer();
            }
            else
            {
                MessageBox.Show("U moet eerst een windkracht selecteren!", "Fout!");
            }
        }

        private void OnMessageReceived (WindkrachtMessage message)
        {
            LadenWindkrachten();
            dialogservice.CloseWindkrachtEditeer();
        }
    }
}
