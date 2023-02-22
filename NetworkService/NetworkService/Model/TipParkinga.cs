using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class TipParkinga : INotifyPropertyChanged
    {
        private string ime;
        private string adresaSlike;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Ime
        {
            get => ime;
            set
            {
                ime = value;
                RaisePropertyChanged("Ime");
            }

        }
        public string AdresaSlike
        {
            get => adresaSlike; set
            {
                adresaSlike = value;
                RaisePropertyChanged("AdresaSlike");
            }
        }
        public TipParkinga(string ime, string adresa)
        {
            Ime = ime;
            AdresaSlike = adresa;
        }
        public TipParkinga()
        {
            Ime = string.Empty;
            AdresaSlike = string.Empty;
        }
        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
