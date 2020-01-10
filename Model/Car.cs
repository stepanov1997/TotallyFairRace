using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using TotallyFairRace.View;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using TotallyFairRace.View.Controls;

namespace TotallyFairRace.Model
{
    public class Car
    {
        //staticka polja
        public static bool EndCheck;
        public static Queue<Car> EndQueue = new Queue<Car>();
        public static readonly string[] Places =
        {
            "1st", "2nd", "3rd", "4th", "5th", "6th", "7th", "8th", "9th", "10th"
        };
        public static int OrdinalNumber;

        //nestaticka polja
        public string Nickname { get; set; }
        public double Speed { get; set; }
        public double NumberOfKilometers { get; set; }
        public CarRaceBar CarBar { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TextBlock Description { get; set; }

        private Car(string nickname, CarRaceBar progressCarBar, TextBlock description)
        {
            Nickname = nickname;
            OrdinalNumber++;
            CarBar = progressCarBar;
            Description = description;
            StartTime = DateTime.UtcNow;
        }

        public static void Resetuj()
        {
            EndCheck = false;
            EndQueue.Clear();
            OrdinalNumber = 0;
        }

        public static Car GetCarOfCurrentUser()
        {
            return EndQueue.ToList().FirstOrDefault(e => e.Nickname == Account.CurrentAccount.Nickname) ??
                   throw new Exception("You should play game first!");
        }

        public static int PlaceOfCurrentUser() => PlaceOfCar(GetCarOfCurrentUser());

        public static int PlaceOfCar(Car car) => EndQueue.ToList().IndexOf(car) + 1;
        public static Car NapraviAuto(string nickname, CarRaceBar progressBar, TextBlock description) => new Car(nickname, progressBar, description);

        /// <summary>Ова метода покреће ауто.</summary>
        public async Task StartCar()
        {
            double max = 0;
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
            {
                max = CarBar.Maximum;
            });
            StartTime = DateTime.UtcNow;
            await RoadTrace(max, 10, 1, 2, 75, false);
            //if ((DateTime.UtcNow - StartTime).Seconds > 2) CarBar.TrafficLights.Visibility = Visibility.Collapsed;
            await RoadTrace(max, 20, 1, 2, 70, false);
            await RoadTrace(max, 30, 2, 2, 65, false);
            await RoadTrace(max, 40, 2, 2, 60, false);
            await RoadTrace(max, 50, 2, 2, 55, false);
            await RoadTrace(max, 60, 2, 2, 50, false);
            await RoadTrace(max, 70, 2, 3, 45, false);
            await RoadTrace(max, 80, 2, 3, 40, false);
            await RoadTrace(max, 90, 2, 3, 35, false);
            await RoadTrace(max, 100, 3, 4, 30, true);
            EndTime = DateTime.Now;
            if (EndQueue.Count == OrdinalNumber)
            {
                EndCheck = true;
            }
        }

        private async Task RoadTrace(double maxPut, double putProcent, double minus, double plus, int postotak, bool zadnjaTrasa)
        {
            while (NumberOfKilometers < putProcent * maxPut / 100)
            {
                if (putProcent >= 20 && Speed < 20)
                {
                    Speed += 5;
                }
                else if (Speed > 300)
                {
                    var velikaBrzina = RandomGen2.Next() % 100;
                    if (velikaBrzina > 100)
                    {
                        Speed -= minus;
                    }
                }
                else if (Speed > 270)
                {
                    var velikaBrzina = RandomGen2.Next() % 100;
                    if (velikaBrzina > 97)
                    {
                        Speed -= minus;
                    }
                }
                else if (Speed > 250)
                {
                    var velikaBrzina = RandomGen2.Next() % 100;
                    if (velikaBrzina > 95)
                    {
                        Speed -= minus;
                    }
                }
                else if (Speed > 230)
                {
                    var velikaBrzina = RandomGen2.Next() % 100;
                    if (velikaBrzina > 93)
                    {
                        Speed -= minus;
                    }
                }
                else if (Speed > 200)
                {
                    var velikaBrzina = RandomGen2.Next() % 100;
                    if (velikaBrzina > 93)
                    {
                        Speed -= minus;
                    }
                }

                var slucaj = RandomGen2.Next() % 100;
                Speed += RandomGen2.Next() % (slucaj>postotak?(-minus):plus);

                await Task.Delay(15);
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    CarBar.CurrentPosition = NumberOfKilometers;
                    Description.Text = $"{Nickname}" + Environment.NewLine +
                                       $"{Math.Round(Speed, 2)} km/s" + Environment.NewLine;
                });
                NumberOfKilometers += Speed / 3600;
            }
            if (zadnjaTrasa)
            {
                CarBar.CurrentPosition = maxPut;
                CarBar.End();
                EndQueue.Enqueue(this);
                Description.Text = $"{Nickname}" + Environment.NewLine +
                                   $"{Places[EndQueue.Count - 1]} place";
            }
        }

        public override string ToString()
        {
            return
                "Broj pređenih kilometara: " +
                $"{Math.Round(NumberOfKilometers, 2, MidpointRounding.ToEven) + Environment.NewLine} " +
                $"Brzina = {Speed + Environment.NewLine}";
        }

        public double GetDurance() =>
            (EndTime - StartTime).Minutes * 60 + (EndTime - StartTime).Seconds;

    }

    public static class RandomGen2
    {
        private static readonly Random Global = new Random();
        [ThreadStatic]
        private static Random _local;
        public static int Next()
        {
            Random inst = _local;
            if (inst != null) return inst.Next();
            int seed;
            lock (Global) seed = Global.Next();
            _local = inst = new Random(seed);
            return inst.Next();
        }
    }
}
