using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NetworkService.ViewModel
{
    class ParkingViewModel : BindableBase
    {
        public static ObservableCollection<Parking> Parkinzi { get; set; } = new ObservableCollection<Parking>();
        public static ObservableCollection<Parking> FiltriraniParkinzi { get; set; } = new ObservableCollection<Parking>();
        public static ObservableCollection<Parking> CancelParkinzi { get; set; } = new ObservableCollection<Parking>();
        public static ObservableCollection<Parking> CancelFiltriraniParkinzi { get; set; } = new ObservableCollection<Parking>();
        public static List<string> TipParkinga { get; set; } = new List<string> { "Otvoren parking", "Zatvoren parking" };
        public MyICommand AddCommand { get; set; }
        public MyICommand DeleteCommand { get; set; }
        private Parking trenutniParking = new Parking();

        public MyICommand FilterCommand { get; set; }
        public MyICommand CancelFilterCommand { get; set; }
        public MyICommand VanOpsegaCommand { get; set; }
        public MyICommand UnutarOpsegaCommand { get; set; }
        public MyICommand IdVCommand { get; set; }
        public MyICommand IdMCommand { get; set; }
        public MyICommand CancelCommand { get; set; }
        public MyICommand ExitCommand { get; set; }
        private int opcijaOpseg = -1;
        private int opcijaVM = -1;
        private double duzinaGrafa = 275;
        private double x1, x2;
        string path = Directory.GetCurrentDirectory();

        private string idText;
        private string imeText;
        private string tipText2;
        private string idFilter;
        TipParkinga tipParkinga = new TipParkinga();
        private Parking selectedParking;
        

        public ParkingViewModel()
        {
          //  LoadParkings();
            AddCommand = new MyICommand(OnAdd);
            DeleteCommand = new MyICommand(OnDelete, CanDelete);
            VanOpsegaCommand = new MyICommand(OnOut);
            UnutarOpsegaCommand = new MyICommand(OnIn);
            FilterCommand = new MyICommand(OnFilter);
            CancelFilterCommand = new MyICommand(LoadFilter);
            IdVCommand = new MyICommand(OnV);
            IdMCommand = new MyICommand(OnM);
            CancelCommand = new MyICommand(OnCancel);
            ExitCommand = new MyICommand(OnExit);
            izracunajDuzinuGrafa();
        }
        public void OnExit()
        {
            MainWindow.close();
        }
        public void izracunajDuzinuGrafa()
        {
            int otvoreni = 0;
            int zatvoreni=0;
            foreach(Parking p in FiltriraniParkinzi)
            {
                if(p.Tip.Ime == "Otvoren parking")
                {
                    otvoreni++;
                }
                else
                {
                    zatvoreni++;
                }
            }
            if (otvoreni == 0 && zatvoreni == 0)
            {
                X1 = 150;
                X2 = 150;
            }
            else
            {
                double deo = duzinaGrafa / (otvoreni + zatvoreni);
                X1 = 15 + deo * otvoreni;
                X2 = 290 - deo * zatvoreni;
            }

        }
        public double X1
        {
            get { return x1; }
            set
            {
                if (x1 != value)
                {
                    x1 = value;
                    OnPropertyChanged("X1");
                }
            }
        }
        public double X2
        {
            get { return x2; }
            set
            {
                if (x2 != value)
                {
                    x2 = value;
                    OnPropertyChanged("X2");
                }
            }
        }
        public void OnCancel()
        {
            Parkinzi.Clear();
            FiltriraniParkinzi.Clear();
            foreach (Parking p in CancelFiltriraniParkinzi)
            {
                FiltriraniParkinzi.Add(p);
            }
            foreach (Parking p in CancelParkinzi)
            {
                Parkinzi.Add(p);
            }
            izracunajDuzinuGrafa();
        }

        public void LoadFilter()
        {
            
            foreach (Parking p in Parkinzi)
            {
                if (!FiltriraniParkinzi.Contains(p))
                {
                    FiltriraniParkinzi.Add(p);
                }
            }
            LoadCancel();
            izracunajDuzinuGrafa();
        }
        public void LoadCancel()
        {
            CancelParkinzi.Clear();
            CancelFiltriraniParkinzi.Clear();
            foreach (Parking p in FiltriraniParkinzi)
            {
               CancelFiltriraniParkinzi.Add(p);               
            }
            foreach (Parking p in Parkinzi)
            {
                CancelParkinzi.Add(p);
            }

        }
        public void OnOut()
        {
            opcijaOpseg = 0;
          
        }
        public void OnIn()
        {
            opcijaOpseg = 1;
           
        }
        public void OnV()
        {
            opcijaVM = 0;

        }
        public void OnM()
        {
            opcijaVM = 1;

        }
        public void OnFilter()
        {
            LoadCancel();
            FiltriraniParkinzi.Clear();
            LoadFilter();
            if (opcijaOpseg == 0)
            {
                foreach (Parking p in Parkinzi)
                {
                    if (p.Vrednost < 90)
                    {
                        if (FiltriraniParkinzi.Contains(p))
                        {
                            FiltriraniParkinzi.Remove(p);
                        }
                    }
                }
            }
            else if(opcijaOpseg == 1)
            {
                foreach (Parking p in Parkinzi)
                {
                    if (p.Vrednost >= 90)
                    {
                        FiltriraniParkinzi.Remove(p);
                    }
                }
            }
            if(TipText2== "Zatvoren parking")
            {
                foreach (Parking p in Parkinzi)
                {
                    if (p.Tip.Ime == "Otvoren parking")
                    {
                        FiltriraniParkinzi.Remove(p);
                    }
                }
            }
            else if (TipText2 == "Otvoren parking")
            {
                foreach (Parking p in Parkinzi)
                {
                    if (p.Tip.Ime == "Zatvoren parking")
                    {
                        FiltriraniParkinzi.Remove(p);
                    }
                }
            }
            int result;
            bool uspeh = Int32.TryParse(IdFilter, out result);
            if (!uspeh)
            {
                
            }
            else
            {
                if (opcijaVM == 1)
                {
                    foreach (Parking p in Parkinzi)
                    {
                        if (p.Id>=result)
                        {
                            FiltriraniParkinzi.Remove(p);
                        }
                    }
                }
                else if (opcijaVM == 0)
                {
                    foreach (Parking p in Parkinzi)
                    {
                        if (p.Id < result)
                        {
                            FiltriraniParkinzi.Remove(p);
                        }
                    }
                }
            }
            izracunajDuzinuGrafa();
        }
        public Parking SelectedParking
        {
            get { return selectedParking; }
            set
            {
                selectedParking = value;
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }
        private bool CanDelete()
        {
            return selectedParking != null;
        }

        private void OnDelete()
        {
            LoadCancel();
            Parkinzi.Remove(selectedParking);
            FiltriraniParkinzi.Remove(selectedParking);
        }
        public string IdFilter
        {
            get { return idFilter; }
            set
            {
                if (idFilter != value)
                {
                    idFilter = value;
                    OnPropertyChanged("IdFilter");
                }
            }
        }

        public string IdText
        {
            get { return idText; }
            set
            {
                if (idText != value)
                {
                    idText = value;
                    OnPropertyChanged("IdText");
                }
            }
        }

        public string ImeText
        {
            get { return imeText; }
            set
            {
                if (imeText != value)
                {
                    imeText = value;
                    OnPropertyChanged("ImeText");
                }
            }
        }
        public string TipText
        {
            get { return tipParkinga.Ime; }
            set
            {
                if (tipParkinga.Ime != value)
                {
                    tipParkinga.Ime = value;
                    OnPropertyChanged("TipText");
                    Adresa = "adresa";
                }
            }
        }
        public string TipText2
        {
            get { return tipText2; }
            set
            {
                if (tipText2 != value)
                {
                    tipText2 = value;
                    OnPropertyChanged("TipText2");
                }
            }
        }

        public string Adresa
        {
            get { return tipParkinga.AdresaSlike; }
            set
            {
                if (tipParkinga.AdresaSlike != value)
                {
                    if(tipParkinga.Ime== "Otvoren parking")
                        tipParkinga.AdresaSlike = path+"\\otvoren_parking.jpg";
                    else
                        tipParkinga.AdresaSlike = path + "\\zatvoren_parking.jpg";
                    OnPropertyChanged("Adresa");
                }
            }
        }

        public  void LoadParkings()

        {
            
            TipParkinga tipPrvog = new TipParkinga("Otvoren Parking", path + "\\otvoren_parking.jpg");
            ObservableCollection<Parking> parkinzi =
                new ObservableCollection<Parking>();



            parkinzi.Add(new Parking { Id = 1, Ime = "otvoren1", Tip = tipPrvog });
            parkinzi.Add(new Parking { Id = 2, Ime = "otvoren2", Tip = tipPrvog });
       //     parkinzi.Add(new Parking { Id = 3, Ime = "otvoren3", Tip = tipPrvog });
      //      parkinzi.Add(new Parking { Id = 4, Ime = "otvoren4", Tip = tipPrvog });


            Parkinzi = parkinzi;
            LoadFilter();
        }

        public Parking TrenutniParking
        {
            get { return trenutniParking; }
            set
            {
                trenutniParking = value;
                OnPropertyChanged("TrenutniParking");
            }
        }
        private void OnAdd()
        {

            LoadCancel();
            TipParkinga tipPark = new TipParkinga(TipText,Adresa);
            Parking p = new Parking(0, ImeText, tipPark);
            p.StringId = IdText;
            p.Vrednost = Parkinzi.Count;
            TrenutniParking = p;
            TrenutniParking.Validate();
            if (TrenutniParking.IsValid)
            {
                Parkinzi.Add(p);
                FiltriraniParkinzi.Add(p);
                izracunajDuzinuGrafa();


            }
            //Parkinzi.Add(new Parking { Id = Int32.Parse(IdText.Trim()), Ime = ImeText, Tip = tipPark });
        }

        private void dodavanjeSlike(){
          /*  BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(adresa);
            img.EndInit();
            canvasSlika.Background = new ImageBrush(img); */
        }

    }
}
