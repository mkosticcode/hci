using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace NetworkService.Model
{
    public class ParkinziNetwork : INotifyPropertyChanged
    {
        private int id;
        private int brojKonekcija;
        private List<Line> listaLinija;
        private Canvas zauzet;
        private List<int> povezaniKanvasi;


        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    RaisePropertyChanged("Id");
                }
            }
        }
        public int BrojKonekcija
        {
            get { return brojKonekcija; }
            set
            {
                if (brojKonekcija != value)
                {
                    brojKonekcija = value;
                    RaisePropertyChanged("BrojKonekcija");
                }
            }
        }
        public Canvas Zauzet
        {
            get { return zauzet; }
            set
            {
                if (zauzet != value)
                {
                    zauzet = value;
                    RaisePropertyChanged("Zauzet");
                }
            }
        }
        public List<Line> ListaLinija
        {
            get { return listaLinija; }
           
        }
        public Line ListaLinijaDodaj
        {
            set
            {
                if (!listaLinija.Contains(value))
                {
                    listaLinija.Add(value);
                    RaisePropertyChanged("ListaLinija");
                }
            }
        }
        public List<int> PovezaniKanvasi
        {
            get { return povezaniKanvasi; }
        }

        public int PovezaniKanvasiDodaj
        {
            
            set
            {
                if (!povezaniKanvasi.Contains(value))
                {
                    povezaniKanvasi.Add(value);
                    RaisePropertyChanged("PovezaniKanvasi");
                }
            }
        }
        public ParkinziNetwork()
        {
        }

        public ParkinziNetwork(int id,  Canvas c)
        {
            Id = id;
            zauzet = c;
            listaLinija = new List<Line>();
            brojKonekcija = 0;
            povezaniKanvasi = new List<int>();

        }
        public ParkinziNetwork(ParkinziNetwork p)
        {
            Id = p.Id;
            zauzet = p.zauzet;
            listaLinija =p.listaLinija;
            brojKonekcija = p.BrojKonekcija;
            povezaniKanvasi = p.povezaniKanvasi;

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
