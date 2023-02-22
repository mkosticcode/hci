using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkService.ViewModel
{
    public class DodatneFunkcije:BindableBase
    {
        public static int n = -1;
        static int  poslednjiN = -2;
        static int id=-3;
        public static void PreuzmiVrednosti(int index)
        {
            if (ParkingViewModel.Parkinzi.Count > 0)
            {
                
                if (poslednjiN != n)
                {
                    for (int i = 0; i < ParkingViewModel.Parkinzi.Count; i++)
                    {
                        if (n == ParkingViewModel.Parkinzi[i].Id)
                        {
                            id = i;
                            poslednjiN = n;
                            GraphViewModel.grafovi[0].Y5 = 0;
                            GraphViewModel.grafovi[0].Br5 = 0;

                            GraphViewModel.grafovi[0].Y4 = 0;
                            GraphViewModel.grafovi[0].Br4 = 0;


                            GraphViewModel.grafovi[0].Y3= 0;
                            GraphViewModel.grafovi[0].Br3 = 0;

                            GraphViewModel.grafovi[0].Y2 = 0;
                            GraphViewModel.grafovi[0].Br2 = 0;


                            GraphViewModel.grafovi[0].Y1 = 0;
                            GraphViewModel.grafovi[0].Br1 = 0;
                            break;

                        }
                    }
                }
                if (index == id)
                {
                    GraphViewModel.grafovi[0].Y5 = GraphViewModel.grafovi[0].Y4;
                    GraphViewModel.grafovi[0].Br5 = GraphViewModel.grafovi[0].Br4;
                    GraphViewModel.grafovi[0].Color5 = GraphViewModel.grafovi[0].Color4;

                    GraphViewModel.grafovi[0].Y4 = GraphViewModel.grafovi[0].Y3;
                    GraphViewModel.grafovi[0].Br4 = GraphViewModel.grafovi[0].Br3;
                    GraphViewModel.grafovi[0].Color4 = GraphViewModel.grafovi[0].Color3;


                    GraphViewModel.grafovi[0].Y3 = GraphViewModel.grafovi[0].Y2;
                    GraphViewModel.grafovi[0].Br3 = GraphViewModel.grafovi[0].Br2;
                    GraphViewModel.grafovi[0].Color3 = GraphViewModel.grafovi[0].Color2;

                    GraphViewModel.grafovi[0].Y2 = GraphViewModel.grafovi[0].Y1;
                    GraphViewModel.grafovi[0].Br2 = GraphViewModel.grafovi[0].Br1;
                    GraphViewModel.grafovi[0].Color2 = GraphViewModel.grafovi[0].Color1;


                    GraphViewModel.grafovi[0].Y1 = IzracunajVisinu(ParkingViewModel.Parkinzi[id].Vrednost);
                    GraphViewModel.grafovi[0].Br1 = ParkingViewModel.Parkinzi[id].Vrednost;
                    if (ParkingViewModel.Parkinzi[0].Vrednost > 90) { GraphViewModel.grafovi[id].Color1 = "Red"; }
                    else { GraphViewModel.grafovi[0].Color1 = "Blue"; }
                }

            }
        }

        public static double IzracunajVisinu(double vrednost)
        {
            return 210 - vrednost * 2;
        }
    }
}
