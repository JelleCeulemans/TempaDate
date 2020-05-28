using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.Model
{
    class Maand : BaseModel
    {
        private int id;
        private string naam;

        public Maand()
        {

        }

        public Maand (int id, string naam)
        {
            Id = id;
            Naam = naam;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
                NotifyPropertyChanged();
            }
        }

        public string Naam
        {
            get
            {
                return naam;
            }

            set
            {
                naam = value;
                NotifyPropertyChanged();
            }
        }
    }
}
