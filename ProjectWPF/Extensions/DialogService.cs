using ProjectWPF.View;
using ProjectWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;

namespace ProjectWPF.Extensions
{
    public class DialogService
    {
        Window maandenediteerwindow = null;
        Window windkrachtenediteerwindow = null;
        Window temperaturenediteerwindow = null;

        public DialogService() {}

        public void ShowMaandenEditeer()
        {
            maandenediteerwindow = new MaandenEditeerWindow();
            maandenediteerwindow.ShowDialog();
        }

        public void CloseMaandenEditeer()
        {
            maandenediteerwindow.Close();
        }

        public void ShowWindkrachtenEditeer()
        {
            windkrachtenediteerwindow = new WindkrachtenEditeerWindow();
            windkrachtenediteerwindow.ShowDialog();
        }

        public void CloseWindkrachtEditeer()
        {
            windkrachtenediteerwindow.Close();
        }

        public void ShowTemperaturenEditeer()
        {
            temperaturenediteerwindow = new TemperaturenEditeerWindow();
            temperaturenediteerwindow.ShowDialog();
        }

        public void CloseTemperatuurEditeer()
        {
            temperaturenediteerwindow.Close();
        }
    }
}
