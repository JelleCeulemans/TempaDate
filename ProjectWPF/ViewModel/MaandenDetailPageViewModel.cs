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
    class MaandenDetailPageViewModel : BaseViewModel
    {
        DialogService dialogservice = null;
        public MaandenDetailPageViewModel()
        {
            LadenMaanden();
            dialogservice = new DialogService();
            WijzigCommand = new BaseCommand(MaandWijzigen);
            ToevoegCommand = new BaseCommand(MaandToevoegen);
            Messenger.Default.Register<MaandMessage>(this, OnMessageReceived);
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

        private void LadenMaanden()
        {
            MaandDataService maandDS = new MaandDataService();
            Maanden = new ObservableCollection<Maand>(maandDS.GetMaanden());
        }

        private void MaandWijzigen()
        {
            if (SelectedMaand != null)
            {
                Messenger.Default.Send<Maand>(SelectedMaand);
                dialogservice.ShowMaandenEditeer();
            }
            else
            {
                MessageBox.Show("U moet eerst een maand selecteren!", "Fout!");
            }
        }

        private void MaandToevoegen()
        {
            Maand maand = new Maand();
            Messenger.Default.Send<Maand>(maand);
            dialogservice.ShowMaandenEditeer();
        }

        private void OnMessageReceived (MaandMessage message)
        {
            LadenMaanden();
            dialogservice.CloseMaandenEditeer();
        }
    }
}
