using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    public class Graph  : INotifyPropertyChanged
    {
        private double y1, y2, y3, y4, y5;
        private double br1, br2, br3, br4, br5;
        private string color1, color2, color3, color4, color5;
        public double Y1 { set { if (y1 != value) { y1 = value; RaisePropertyChanged("Y1"); } } get { return y1; } }
        public double Y2 { set { if (y2 != value) { y2 = value; RaisePropertyChanged("Y2"); } } get { return y2; } }
        public double Y3 { set { if (y3 != value) { y3 = value; RaisePropertyChanged("Y3"); } } get { return y3; } }
        public double Y4 { set { if (y4 != value) { y4 = value; RaisePropertyChanged("Y4"); } } get { return y4; } }
        public double Y5 { set { if (y5 != value) { y5 = value; RaisePropertyChanged("Y5"); } } get { return y5; } }

        public double Br1 { set { if (br1 != value) {br1 = value; RaisePropertyChanged("Br1"); } } get { return br1; } }
        public double Br2 { set { if (br2 != value) { br2 = value; RaisePropertyChanged("Br2"); } } get { return br2; } }
        public double Br3 { set { if (br3 != value) { br3 = value; RaisePropertyChanged("Br3"); } } get { return br3; } }
        public double Br4 { set { if (br4 != value) { br4 = value; RaisePropertyChanged("Br4"); } } get { return br4; } }
        public double Br5 { set { if (br5 != value) { br5 = value; RaisePropertyChanged("Br5"); } } get { return br5; } }
        public string Color1 { set { if (color1 != value) { color1 = value; RaisePropertyChanged("Color1"); } } get { return color1; } }
        public string Color2 { set { if (color2 != value) { color2 = value; RaisePropertyChanged("Color2"); } } get { return color2; } }
        public string Color3 { set { if (color3 != value) { color3 = value; RaisePropertyChanged("Color3"); } } get { return color3; } }
        public string Color4 { set { if (color4 != value) { color4 = value; RaisePropertyChanged("Color4"); } } get { return color4; } }
        public string Color5 { set { if (color5 != value) { color5 = value; RaisePropertyChanged("Color5"); } } get { return color5; } }

        public Graph(double y1)
        {
            Y1 = y1;
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
