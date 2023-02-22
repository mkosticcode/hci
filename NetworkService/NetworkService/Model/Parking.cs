using NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Marko Kostic pr129-2018
namespace NetworkService.Model
{
    public class Parking : ValidationBase
    {
        private int id;
        private string stringId;
        private string ime;
        private TipParkinga tip;
        private double vrednost;

        public Parking()
        {
        }

        public Parking(int id, string ime, TipParkinga tip)
        {
            Id = id;
            Ime = ime;
            Tip = tip;
 
        }
        public Parking(Parking p)
        {
            Id = p.id;
            Ime = p.ime;
            Tip = p.tip;

        }

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    
                    id = value;
                    RaisePropertyChanged("Id");
                    OnPropertyChanged("Id");

                }
            }
        }

        public string Ime
        {
            get { return ime; }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    RaisePropertyChanged("Ime");
                    OnPropertyChanged("Ime");

                }
            }
        }
        public string StringId
        {
            get { return stringId; }
            set
            {
                if (stringId != value)
                {
                    stringId = value;
                    RaisePropertyChanged("StringId");
                    OnPropertyChanged("StringId");

                }
            }
        }

        public double Vrednost
        {
            get { return vrednost; }
            set
            {
                if (vrednost != value)
                {
                    RaisePropertyChanged("Vrednost");
                    OnPropertyChanged("Vrednost");
                    vrednost = value;
                    
                }
            }
        }

        public TipParkinga Tip
        {
            get { return tip; }
            set
            {
                if (tip != value)
                {
                    tip = value;
                     RaisePropertyChanged("Tip");
                    OnPropertyChanged("Tip");

                }
            }
        }
        protected override void ValidateSelf()
        {
            int result;
            bool uspeh=Int32.TryParse(StringId,out result);
            if (!uspeh)
            {
                this.ValidationErrors["Id"] = "Id mora biti integer";
            }
            else
            {
                Id = result;
            }
            foreach (Parking p in ParkingViewModel.Parkinzi)
            {
                if (p.Id==this.Id)
                {
                    this.ValidationErrors["Id"] = "Id entiteta mora biti jedinstven.";
                }
            }
            if (string.IsNullOrWhiteSpace(this.Ime))
            {
                this.ValidationErrors["Ime"] = "Ime ne sme biti prazno.";
            }
            if (this.Tip.Ime == "")
            {
                this.ValidationErrors["Combo"] = "Ova opcija  mora biti selektovana.";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
