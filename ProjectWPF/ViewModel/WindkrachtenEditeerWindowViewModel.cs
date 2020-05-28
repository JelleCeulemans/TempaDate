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
    class WindkrachtenEditeerWindowViewModel : BaseViewModel
    {
        public WindkrachtenEditeerWindowViewModel()
        {
            Messenger.Default.Register<Windkracht>(this, OnWindkrachtReceived);
            UpdateCommand = new BaseCommand(UpdateWindkracht);
            VerwijderCommand = new BaseCommand(VerwijderWindkracht);
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

        private void OnWindkrachtReceived (Windkracht windkracht)
        {
            SelectedWindkracht = windkracht;
        }

        private void UpdateWindkracht ()
        {
            if (SelectedWindkracht.Benaming != "" && SelectedWindkracht.Benaming != null && SelectedWindkracht.Uitwerking != "" && SelectedWindkracht.Uitwerking != null)
            {
                WindkrachtDataService windkrachtDS = new WindkrachtDataService();
                int aantalRecords = windkrachtDS.GetWindkrachtWhereKracht(SelectedWindkracht).Count;

                if (SelectedWindkracht.Id > 0)
                {
                    
                    Windkracht teUpdaten = windkrachtDS.GetWindkracht(SelectedWindkracht)[0];
                    if (teUpdaten.Kracht == SelectedWindkracht.Kracht)
                    {
                        windkrachtDS.UpdateWindkracht(SelectedWindkracht);
                        Messenger.Default.Send<WindkrachtMessage>(new WindkrachtMessage(WindkrachtMessage.MessageType.Updated));
                    }
                    else
                    {
                        if (aantalRecords > 0)
                        {
                            MessageBox.Show("De windkracht met deze kracht (beaufort) bestaat al reeds!", "Fout!");
                        }
                        else
                        {
                            windkrachtDS.UpdateWindkracht(SelectedWindkracht);
                            Messenger.Default.Send<WindkrachtMessage>(new WindkrachtMessage(WindkrachtMessage.MessageType.Updated));
                        }
                    }
                }
                else
                {
                    if (aantalRecords > 0)
                    {
                        MessageBox.Show("De windkracht met deze kracht (beaufort) bestaat al reeds!", "Fout!");
                    }
                    else
                    {
                        windkrachtDS.ToevoegWindkracht(SelectedWindkracht);
                        Messenger.Default.Send<WindkrachtMessage>(new WindkrachtMessage(WindkrachtMessage.MessageType.Inserted));
                    }
                }


            }
            else
            {
                MessageBox.Show("Alle velden moeten ingevuld zijn!", "Fout!");
            }
        }

        private void VerwijderWindkracht()
        {
            WindkrachtDataService windkrachtDS = new WindkrachtDataService();
            TemperatuurDataService tempDS = new TemperatuurDataService();

            Temperaturen = tempDS.GetTemperaturenPerWindkracht(SelectedWindkracht);

            if (Temperaturen.Count == 0)
            {
                windkrachtDS.VerwijderTemperatuur(SelectedWindkracht);
                Messenger.Default.Send<WindkrachtMessage>(new WindkrachtMessage(WindkrachtMessage.MessageType.Deleted));
            }
            else
            {
                MessageBox.Show("Dit record is nog verbonden aan " + Temperaturen.Count + " record(s) van de tabel Temperatuur!", "Fout!");
            }
            

        }


    }
}
