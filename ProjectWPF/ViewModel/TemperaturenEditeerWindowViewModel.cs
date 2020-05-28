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
    class TemperaturenEditeerWindowViewModel : BaseViewModel
    {
        public TemperaturenEditeerWindowViewModel ()
        {
            LadenMaanden();
            Messenger.Default.Register<Temperatuur>(this, OnTemperatuurReceived);
            UpdateCommand = new BaseCommand(UpdateTemperatuur);
            VerwijderCommand = new BaseCommand(VerwijderTemperatuur);
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

        private void OnTemperatuurReceived(Temperatuur temperatuur)
        {
            SelectedTemperatuur = temperatuur;
        }


        private void UpdateTemperatuur()
        {
            MaandDataService maandDS = new MaandDataService();
            Maanden = maandDS.GetMaanden();
            int[] maandIdLijst = new int[Maanden.Count];
            int teller = 0;
            foreach (Maand maand in Maanden)
            {
                maandIdLijst[teller] = maand.Id;
                teller++;
            }

            WindkrachtDataService wkDS = new WindkrachtDataService();
            Windkrachten = wkDS.GetWindkrachten();
            int[] windkrachtIdLijst = new int[Windkrachten.Count];
            teller = 0;
            foreach (Windkracht wk in Windkrachten)
            {
                windkrachtIdLijst[teller] = wk.Id;
                teller++;
            }

            if (maandIdLijst.Contains(SelectedTemperatuur.MaandId) && windkrachtIdLijst.Contains(selectedTemperatuur.WindkrachtId))
            {
                TemperatuurDataService tempDS = new TemperatuurDataService();
                int aantalRecords = tempDS.GetTemperaturenWhereJaarMaand(SelectedTemperatuur).Count;
                if (SelectedTemperatuur.Id > 0)
                {
                    Temperatuur teUpdaten = tempDS.GetTemperaturenWhereId(SelectedTemperatuur)[0];
                    if (teUpdaten.Jaar == SelectedTemperatuur.Jaar && teUpdaten.MaandId == SelectedTemperatuur.MaandId)
                    {
                        tempDS.UpdateTemperatuur(SelectedTemperatuur);
                        Messenger.Default.Send<TemperatuurMessage>(new TemperatuurMessage(TemperatuurMessage.MessageType.Updated));
                    }
                    else
                    {
                        if (aantalRecords > 0)
                        {
                            MessageBox.Show("Er bestaan al reeds gegevens voor " + SelectedTemperatuur.MaandId + "/" + SelectedTemperatuur.Jaar + ".", "Fout!");
                        }
                        else
                        {
                            tempDS.UpdateTemperatuur(SelectedTemperatuur);
                            Messenger.Default.Send<TemperatuurMessage>(new TemperatuurMessage(TemperatuurMessage.MessageType.Updated));
                        }
                    }
                }
                else
                {
                    if (aantalRecords > 0)
                    {
                        MessageBox.Show("Er bestaan al reeds gegevens voor " + SelectedTemperatuur.MaandId + "/" + SelectedTemperatuur.Jaar + ".", "Fout!");
                    }
                    else
                    {
                        tempDS.ToevoegTemperatuur(SelectedTemperatuur);
                        Messenger.Default.Send<TemperatuurMessage>(new TemperatuurMessage(TemperatuurMessage.MessageType.Inserted));
                    }

                }
            }
            else
            {
                string maandenLijst = "";
                string windkrachtenLijst = "";
                teller = 1;
                foreach (var item in maandIdLijst)
                {

                    maandenLijst += item;
                    if (teller != windkrachtIdLijst.Count())
                    {
                        maandenLijst += ", ";
                    }
                    teller++;
                }
                teller = 1;
                foreach (var item in windkrachtIdLijst)
                {
                    windkrachtenLijst += item;
                    if (teller != windkrachtIdLijst.Count())
                    {
                        windkrachtenLijst += ", ";
                    }
                    teller++;

                    
                }

                MessageBox.Show("Maand ID en/of Windkract ID is niet correct ingevult.\n" +
                        "De mogelijke ID's voor de maanden:\n" + maandenLijst + "\n" +
                        "De mogelijke ID's voor de windkrachten\n" + windkrachtenLijst, "Fout!");
            }
        }

        private void VerwijderTemperatuur ()
        {
            TemperatuurDataService tempDS = new TemperatuurDataService();
            tempDS.VerwijderTemperatuur(SelectedTemperatuur);
            Messenger.Default.Send<TemperatuurMessage>(new TemperatuurMessage(TemperatuurMessage.MessageType.Deleted));
        }

        private void LadenMaanden()
        {
            MaandDataService maandDS = new MaandDataService();
            Maanden = maandDS.GetMaanden();
        }


    }
}
