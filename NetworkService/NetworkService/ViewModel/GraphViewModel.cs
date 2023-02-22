using NetworkService.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NetworkService.ViewModel
{
    public class GraphViewModel:BindableBase
    {
        

        public static ObservableCollection<Graph> grafovi { get; set; } = new ObservableCollection<Graph>();
        private int selectedEntitet=-5;
        public MyICommand ShowCommand { get; set; }


        public BindingList<Parking> parkingziPrikaz { get; set; }
        public BindingList<int> entiteti { get; set; }

        public  GraphViewModel()
        {

            entiteti = new BindingList<int>();
            foreach (Parking p in ParkingViewModel.Parkinzi)
            {
                entiteti.Add(p.Id);
            }


            // PreuzmiVrednosti();
            //OnPropertyChanged("Y1");
            Graph g = new Graph(80);
            grafovi.Add(g);
            ShowCommand = new MyICommand(OnShow);


        }
        public int SelectedEntitet
        {
            get { return selectedEntitet; }
            set
            {
                if (selectedEntitet != value)
                {
                    selectedEntitet = value;
                    OnPropertyChanged("SelectedEntitet");
                }
            }
        }
      /*  private bool CanShow()
        {
            return SelectedEntitet != -5;
        } */

        private void OnShow()
        {
            DodatneFunkcije.n = selectedEntitet;
        }
       
    }
}
