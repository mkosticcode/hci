using NetworkService.Model;
using NetworkService.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetworkService
{
    public class MainWindowViewModel: BindableBase
    {
        private int count =-1; // Inicijalna vrednost broja objekata u sistemu
        private int id;             // ######### ZAMENITI stvarnim brojem elemenata
        private double value;                            //           zavisno od broja entiteta u listi

        public MyICommand<string> NavCommand { get; private set; }
        private ParkingViewModel parkingtViewModel = new ParkingViewModel();
        private NetworkViewModel networkViewModel = new NetworkViewModel();
        private GraphViewModel graphViewModel = new GraphViewModel();
        private BindableBase currentViewModel;

        private Uri path = new Uri("LogFile.txt", UriKind.Relative);
        private bool file = false;

        public MainWindowViewModel()
        {
            createListener(); //Povezivanje sa serverskom aplikacijom
            NavCommand = new MyICommand<string>(OnNav);
            CurrentViewModel = parkingtViewModel;
           
        }
        public BindableBase CurrentViewModel
        {
            get { return currentViewModel; }
            set
            {
                SetProperty(ref currentViewModel, value);
            }
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case "parking":
                    CurrentViewModel = parkingtViewModel;
                    break;
                case "network":
                    CurrentViewModel = networkViewModel;
                    break;
                case "graph":
                    CurrentViewModel = graphViewModel;
                    break;
            }
        }

        private void createListener()
        {
            var tcp = new TcpListener(IPAddress.Any, 25565);
            tcp.Start();

            var listeningThread = new Thread(() =>
            {
                while (true)
                {
                    var tcpClient = tcp.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(param =>
                    {
                        //Prijem poruke
                        NetworkStream stream = tcpClient.GetStream();
                        string incomming;
                        byte[] bytes = new byte[1024];
                        int i = stream.Read(bytes, 0, bytes.Length);
                        //Primljena poruka je sacuvana u incomming stringu
                        incomming = System.Text.Encoding.ASCII.GetString(bytes, 0, i);

                        //Ukoliko je primljena poruka pitanje koliko objekata ima u sistemu -> odgovor
                        if (incomming.Equals("Need object count"))
                        {
                            //Response
                            /* Umesto sto se ovde salje count.ToString(), potrebno je poslati 
                             * duzinu liste koja sadrzi sve objekte pod monitoringom, odnosno
                             * njihov ukupan broj (NE BROJATI OD NULE, VEC POSLATI UKUPAN BROJ)
                             * */
                            Byte[] data = System.Text.Encoding.ASCII.GetBytes(ParkingViewModel.Parkinzi.Count.ToString());
                            stream.Write(data, 0, data.Length);
                        }
                        else
                        {
                            //U suprotnom, server je poslao promenu stanja nekog objekta u sistemu
                            Console.WriteLine(incomming); //Na primer: "Entitet_1:272"
                            string[] split = incomming.Split('_', ':');
                            int index = Int32.Parse(split[1]);
                            
                            if (ParkingViewModel.Parkinzi.Count > index)
                                id = ParkingViewModel.Parkinzi[index].Id;
                            else
                                id = -1;
                            value = double.Parse(split[2]);
                           // Parking v = new Parking(id);
                            if (id != -1)
                            {
                                ParkingViewModel.Parkinzi[index].Vrednost = value;
                                ParkingViewModel.FiltriraniParkinzi[index].Vrednost = value;
                                DodatneFunkcije.PreuzmiVrednosti(index);
                               // NetworkViewModel.proveraVrednosti(index);
                                UpisUFajl();
                            }
                            
                            //################ IMPLEMENTACIJA ####################
                            // Obraditi poruku kako bi se dobile informacije o izmeni
                            // Azuriranje potrebnih stvari u aplikaciji

                        }
                    }, null);
                }
            });

            listeningThread.IsBackground = true;
            listeningThread.Start();
        }
        private void UpisUFajl()
        {
            if (!file)
            {
                StreamWriter wr;
                using (wr = new StreamWriter(path.ToString()))
                {
                    wr.WriteLine("Date Time:\t" + DateTime.Now.ToString() + "\tObject_" + id + "\tValue:\t" + value);
                }
            }
            else
            {
                StreamWriter wr;
                using (wr = new StreamWriter(path.ToString(), true))
                {
                    wr.WriteLine("Date Time:\t" + DateTime.Now.ToString() + "\tObject_" + id + "\tValue:\t" + value);
                }
            }
            file = true;
        }
    }
}
