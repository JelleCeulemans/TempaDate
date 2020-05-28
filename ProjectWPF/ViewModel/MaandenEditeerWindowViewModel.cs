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
    class MaandenEditeerWindowViewModel : BaseViewModel
    {
        DialogService dialogservice = null;
        public MaandenEditeerWindowViewModel()
        {
            VerwijderCommand = new BaseCommand(verwijderMaand);
            UpdateCommand = new BaseCommand(updateMaand);
            Messenger.Default.Register<Maand>(this, onMaandReceived);
            dialogservice = new DialogService();
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

        private ICommand verwijderCommand;
        public ICommand VerwijderCommand
        {
            get
            {
                return verwijderCommand;
            }

            set
            {
                verwijderCommand = value;
            }
        }

        private void onMaandReceived(Maand maand)
        {
            SelectedMaand = maand;
        }

        private void updateMaand ()
        {
            MaandDataService maandDS = new MaandDataService();
            int aantalRecords = maandDS.GetMaandenWhereNaam(SelectedMaand).Count;
            if (SelectedMaand.Naam != "" &&  SelectedMaand.Naam != null)
            {
                if (SelectedMaand.Id > 0)
                {
                    Maand teUpdaten = maandDS.GetMaandWhereId(SelectedMaand)[0];
                    if (teUpdaten.Naam == SelectedMaand.Naam)
                    {
                        maandDS.UpdateMaand(SelectedMaand);
                        Messenger.Default.Send<MaandMessage>(new MaandMessage(MaandMessage.MessageType.Updated));
                    }
                    else
                    {
                        if (aantalRecords > 0)
                        {
                            MessageBox.Show("Deze naam komt al voor in de tabel Maanden!", "Fout!");
                        }
                        else
                        {
                            maandDS.UpdateMaand(SelectedMaand);
                            Messenger.Default.Send<MaandMessage>(new MaandMessage(MaandMessage.MessageType.Updated));
                        }
                    }
                }
                else
                {
                    if (aantalRecords > 0)
                    {
                        MessageBox.Show("Deze naam komt al voor in de tabel Maanden!", "Fout!");
                    }
                    else
                    {
                        maandDS.ToevoegMaand(SelectedMaand);
                        Messenger.Default.Send<MaandMessage>(new MaandMessage(MaandMessage.MessageType.Inserted));
                    }
                }

            }
            else
            {
                MessageBox.Show("Alle velden moeten ingevuld zijn!", "Fout!");
            }
        }

        private void verwijderMaand ()
        {
            MaandDataService maandDS = new MaandDataService();
            TemperatuurDataService tempDS = new TemperatuurDataService();

            Temperaturen = tempDS.GetTemperaturenPerMaand(SelectedMaand);
            Console.WriteLine(Temperaturen.Count);

            if (Temperaturen.Count == 0)
            {
                maandDS.VerwijderMaand(SelectedMaand);
                Messenger.Default.Send<MaandMessage>(new MaandMessage(MaandMessage.MessageType.Deleted));
            }
            else
            {
                MessageBox.Show("Dit record is nog verbonden aan " + Temperaturen.Count + " record(s) van de tabel Temperatuur!", "Fout!");
            }
        }
    }
}
