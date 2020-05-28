using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.Model
{
    class Windkracht : BaseModel
    {
        private int id;
        private int kracht;
        private string benaming;
        private int van;
        private int tot;
        private string uitwerking;

        public Windkracht ()
        {
            
        }

        public Windkracht (int id, int kracht, string benaming, int van, int tot, string uitwerking)
        {
            Id = id;
            Kracht = kracht;
            Benaming = benaming;
            Van = van;
            Tot = tot;
            Uitwerking = uitwerking;
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

        public int Kracht
        {
            get
            {
                return kracht;
            }

            set
            {
                kracht = value;
                NotifyPropertyChanged();
            }
        }

        public string Benaming
        {
            get
            {
                return benaming;
            }

            set
            {
                benaming = value;
                NotifyPropertyChanged();
            }
        }

        public int Van
        {
            get
            {
                return van;
            }

            set
            {
                van = value;
                NotifyPropertyChanged();
            }
        }

        public int Tot
        {
            get
            {
                return tot;
            }

            set
            {
                tot = value;
                NotifyPropertyChanged();
            }
        }

        public string Uitwerking
        {
            get
            {
                return uitwerking;
            }

            set
            {
                uitwerking = value;
                NotifyPropertyChanged();
            }
        }
    }
}
