using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWPF.Model
{
    class Temperatuur : BaseModel
    {
        private int id;
        private int minimum;
        private int gemiddelde;
        private int maximum;
        private int maandId;
        private int jaar;
        private int maxWind;
        private int windkrachtId;

        public Temperatuur ()
        {

        }

        public Temperatuur(int id, int minimum, int gemiddelde, int maximum, int maandId, int jaar, int maxWind, int windkrachtId)
        {
            Id = id;
            Minimum = minimum;
            Gemiddelde = gemiddelde;
            Maximum = maximum;
            MaandId = maandId;
            Jaar = jaar;
            MaxWind = maxWind;
            WindkrachtId = windkrachtId;

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

        public int Minimum
        {
            get
            {
                return minimum;
            }

            set
            {
                minimum = value;
                NotifyPropertyChanged();
            }
        }

        public int Gemiddelde
        {
            get
            {
                return gemiddelde;
            }

            set
            {
                gemiddelde = value;
                NotifyPropertyChanged();
            }
        }

        public int Maximum
        {
            get
            {
                return maximum;
            }

            set
            {
                maximum = value;
                NotifyPropertyChanged();
            }
        }

        public int MaandId
        {
            get
            {
                return maandId;
            }

            set
            {
                maandId = value;
                NotifyPropertyChanged();
            }
        }

        public int Jaar
        {
            get
            {
                return jaar;
            }

            set
            {
                jaar = value;
                NotifyPropertyChanged();
            }
        }

        public int MaxWind
        {
            get
            {
                return maxWind;
            }

            set
            {
                maxWind = value;
                NotifyPropertyChanged();
            }
        }

        public int WindkrachtId
        {
            get
            {
                return windkrachtId;
            }

            set
            {
                windkrachtId = value;
                NotifyPropertyChanged();
            }
        }

        public static implicit operator ObservableCollection<object>(Temperatuur v)
        {
            throw new NotImplementedException();
        }
    }
}
