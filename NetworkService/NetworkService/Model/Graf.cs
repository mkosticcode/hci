using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.Model
{
    class Graf:BindableBase
    {
        private double y1, y2, y3, y4, y5;
        private double br1, br2, br3, br4, br5;
        private string blue, red;

        public Graf(double y1)
        {
            this.y1 = y1;
        }

        public double Y1 { set { if (y1 != value) { y1 = value; OnPropertyChanged("Y1"); } } get { return y1; } }
        public double Y2 { get { return y2; } }
        public double Y3 { get { return y3; } }
        public double Y4 { get { return y4; } }
        public double Y5 { get { return y5; } }
        public double Br1 { get { return br1; } }
        public double Br2 { get { return br2; } }
        public double Br3 { get { return br3; } }
        public double Br4 { get { return br4; } }
        public double Br5 { get { return br5; } }
    }
}
