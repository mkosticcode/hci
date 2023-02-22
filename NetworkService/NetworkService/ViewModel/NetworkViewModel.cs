using NetworkService.Model;
using NetworkService.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetworkService.ViewModel
{
    public class NetworkViewModel :BindableBase
    {
        public static Parking draggItm = null;
        private bool dragging = false;
        private static bool exists = false;
        private int selectedIndex = 0;
        private double pozicijaLevo, pozicijaGore;
        private static double pozicijaDesno, pozicijaDole;
        private static bool pokrenuto = false;
        private static Canvas crtanjeLinija;
        //  private static Canvas pokrenutCanvas;
        //  private static int pokrenutCanvasId;
        private static ParkinziNetwork pokrenutNetwork;
         Line value ;
        private ListView lv;

        // obraditi blokiranje ubacene slike i prebacivanje slika

        public MyICommand<ListView> MLBUCommand { get; set; }
        public MyICommand<Parking> SCCommand { get; set; }
        public MyICommand<ListView> LLWCommand { get; set; }

        public MyICommand<Canvas> DCommand { get; set; }//on drop
        public MyICommand<Canvas> MDCommand { get; set; }
        public MyICommand<Canvas> MRBDCommand { get; set; }
        public MyICommand<Canvas> MECommand { get; set; }
        public static Dictionary<string, Parking> CanvasObj { get; set; } = new Dictionary<string, Parking>();

        public BindingList<Parking> parkingziPrikaz { get; set; }
        public static BindingList<ParkinziNetwork> ParkinziCanvas { get; set; } = new BindingList<ParkinziNetwork>();
        public NetworkViewModel()
        {
            pokrenutNetwork = null;
            parkingziPrikaz = new BindingList<Parking>();

            loadNetwork();
            MLBUCommand = new MyICommand<ListView>(OnMLBU);
            SCCommand = new MyICommand<Parking>(SelectionChange);
            LLWCommand = new MyICommand<ListView>(OnLLW);
            DCommand = new MyICommand<Canvas>(OnDrop);
            MDCommand = new MyICommand<Canvas>(OnSelect);
            MRBDCommand = new MyICommand<Canvas>(OnConnect);
            MECommand = new MyICommand<Canvas>(OnEnter);
            pokrenuto = false;
            draggItm = null;
            ParkinziCanvas.Clear();

        }
        public void loadNetwork()
        {
            foreach (Parking p in ParkingViewModel.Parkinzi)
            {
                parkingziPrikaz.Add(new Parking(p));
            }
                     ukloniTagove();
            //     ucitajKanvase();
        }
        public void ucitajKanvase()
        {
            foreach (ParkinziNetwork p in ParkinziCanvas)
            {
                foreach (Parking p1 in parkingziPrikaz)
                {
                    if (p1.Id == p.Id)
                    {
                        
                        UnosSlike(p.Zauzet, p1);
                        break;
                    }
                }
            }
        }
        public void ukloniTagove()
        {
            foreach (ParkinziNetwork p in ParkinziCanvas)
            {
                Canvas c = p.Zauzet;
                c.Resources.Remove(p.Id);
                c.Resources.Remove("taken");

            }
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");

            }
        }
        public double PozicijaLevo
        {
            get => pozicijaLevo;
            set
            {
                pozicijaLevo = value;
                OnPropertyChanged("PozicijaLevo");

            }
        }
        public double PozicijaGore
        {
            get => pozicijaGore;
            set
            {
                pozicijaGore = value;
                OnPropertyChanged("PozicijaGore");

            }
        }
        
        public void OnMLBU(ListView lw)
        {//podizanje klika, sve stavi na null/false ->ponisti sve
            draggItm = null;
            lw.SelectedItem = null;
            dragging = false;
        }
        public void SelectionChange(Parking p)
        {
            if (!dragging )
            {//izvrsi promenu ako nema pomeranja
                dragging = true;
                if (p != null)
                {
                    draggItm = p;

                    DragDrop.DoDragDrop(lv, draggItm, DragDropEffects.Move);
                }
            }
        }
        public void OnLLW(ListView listview)
        {
            //lv dobija vrednost ListView-a gde su obj
            lv = listview;
        }
        public void UnosSlike(Canvas c,Parking p)
        {
            BitmapImage slika = new BitmapImage();
            slika.BeginInit();
            string temp = p.Tip.AdresaSlike;
            slika.UriSource = new Uri(temp);

            slika.EndInit();
            c.Background = new ImageBrush(slika);
            c.Resources.Add(p.Id, true);
            c.Resources.Add("taken", true);
            //   if (parkingziPrikaz.Contains(draggItm)){
            parkingziPrikaz.Remove(parkingziPrikaz.Single(x => x.Id == p.Id));
            //    }
        }
        public void OnDrop(Canvas c)
        {
            if (draggItm != null)
            {
                if (c.Resources["taken"] == null)
                {

                    UnosSlike(c, draggItm);
                    // if(ParkinziCanvas
                    bool dodaj = true;
                    ParkinziNetwork p2=new ParkinziNetwork();
                    foreach(ParkinziNetwork p in ParkinziCanvas)
                    {
                        if (draggItm.Id == p.Id)
                        {
                            dodaj = false;
                            p2 = p;
                            break;
                        }
                    }
                    if (dodaj)
                    {
                        ParkinziNetwork p1 = new ParkinziNetwork(draggItm.Id, c);
                        ParkinziCanvas.Add(p1);
                    }
                    else
                    {
                        p2.Zauzet = c;
                        dodavanjeSvihLinija(p2);
                    }

                }
                dragging = false;
                SelectedIndex = 0;

              //  crtanjeLinija.Children.Remove(value);
            }
        }
        public void OnSelect(Canvas c)
        {

            foreach (Parking p in ParkingViewModel.Parkinzi)
            {
                if (c.Resources.Contains(p.Id))
                {
                    parkingziPrikaz.Add(new Parking(p));
                    draggItm = p;
                    c.Background =Brushes.Blue;
                    c.Resources.Remove(draggItm.Id);
                    c.Resources.Remove("taken");
                    foreach (ParkinziNetwork pNet in ParkinziCanvas)
                    {
                        if (pNet.Id == p.Id)
                        {
                            ukloniSveLinije(pNet);
                        }
                    }
                    DragDrop.DoDragDrop(lv, draggItm.Id, DragDropEffects.Move);
                    
                }
            }
           

        }
        public void OnConnect(Canvas c)
        {
            foreach (ParkinziNetwork p in ParkinziCanvas)
            {
                if (c == p.Zauzet)
                {

                    if (pokrenuto )
                    {
                        if (c != pokrenutNetwork.Zauzet)
                        {
                            dodavanjeVeze(p,false);
                        }
                        else
                        {
                            pokrenuto = false;
                        }
                    }
                    else
                    { 
                        pokrenuto = true;
                        pozicijaDole = Canvas.GetTop(c);
                        pozicijaDesno = Canvas.GetLeft(c);
                        pokrenutNetwork = p;
                    }
                }
            }

        }
        public void dodavanjeVeze(ParkinziNetwork p, bool stara=false)
        {
            List<int> lista = p.PovezaniKanvasi;
            Canvas c = p.Zauzet;
            if (!lista.Contains(pokrenutNetwork.Id) || stara)
            {
                Line l;
                if (!stara)
                {
                     l = iscrtavanjeLinije(Canvas.GetLeft(c), Canvas.GetTop(c) + pokrenutNetwork.BrojKonekcija * 25,
                        pozicijaDesno + p.BrojKonekcija * 25, pozicijaDole, Brushes.PaleVioletRed);
                }
                else {
                     l = iscrtavanjeLinije(Canvas.GetLeft(c), Canvas.GetTop(c) + p.BrojKonekcija * 25,
                      pozicijaDesno + pokrenutNetwork.BrojKonekcija * 25, pozicijaDole, Brushes.PaleVioletRed);
                }
                p.BrojKonekcija++;
                pokrenutNetwork.BrojKonekcija++;
                     //iz nekog razlog se ne uveca         
                p.ListaLinijaDodaj = l;
                pokrenutNetwork.ListaLinijaDodaj = l;
                Canvas.SetZIndex(crtanjeLinija, 1);
                p.PovezaniKanvasiDodaj = pokrenutNetwork.Id;
                pokrenutNetwork.PovezaniKanvasiDodaj = p.Id;
                crtanjeLinija.Children.Add(l);
                pokrenuto = false;
                // value = l;

            }
        }
        public void dodavanjeSvihLinija(ParkinziNetwork p)
        {
            foreach (int iterator in p.PovezaniKanvasi)
            {
                foreach(ParkinziNetwork park in ParkinziCanvas)
                {
                    if (park.Id == iterator)
                    {
                        Canvas c1 = park.Zauzet;
                        pozicijaDole = Canvas.GetTop(c1);
                        pozicijaDesno = Canvas.GetLeft(c1);
                        pokrenutNetwork = park;
                        dodavanjeVeze(p,true);                      
                        break;
                    }
                }
            }
            
        }

        public Line iscrtavanjeLinije(double x1,double y1,double x2,double y2,Brush b)
        {
            Line l = new Line();
            l.X1 = x1;
            l.Y1 = y1;
            l.X2 = x2;
            l.Y2 = y2;
            l.Stroke = b;
            l.StrokeThickness = 3;
            return l;
        }
        public void ukloniSveLinije(ParkinziNetwork p)
        {
            List<Line> listaLinijaP = p.ListaLinija;
            foreach (ParkinziNetwork iterator in ParkinziCanvas)
            {
                List<int> lista = iterator.PovezaniKanvasi;
                
                if (lista.Contains(p.Id))
                {
                    List<Line> listaLinija = iterator.ListaLinija;
                    ukloniLiniju(listaLinija, listaLinijaP);
                    lista.Remove(p.Id);
                    iterator.BrojKonekcija--;
                    p.BrojKonekcija--;
                    
                }
            }
        }
        public void ukloniLiniju(List <Line> l1,List<Line>l2)
        {
            foreach(Line i in l1)
            {
                foreach(Line j in l2)
                {
                    if (i == j)
                    {
                        crtanjeLinija.Children.Remove(i);
                        l1.Remove(i);
                        l2.Remove(j);
                        return;
                    }
                }
            }
        }
        public void OnEnter(Canvas c)
        {

            crtanjeLinija = c;

        }
        public static void proveraVrednosti(int i)
        {
                foreach (ParkinziNetwork p1 in ParkinziCanvas)
                {
                    if (p1.Id == ParkingViewModel.Parkinzi[i].Id)
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Canvas c = p1.Zauzet;
                        
                        
                        if (ParkingViewModel.Parkinzi[i].Vrednost >= 90)
                        {
                            ((Border)(c).Children[0]).BorderBrush = Brushes.Red;
                        }
                        else
                        {
                            ((Border)(c).Children[0]).BorderBrush = Brushes.Transparent;
                        }
                        
                    });
                    break;
                    }
                }
            
        }
    }
}
