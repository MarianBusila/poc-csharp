
using System;
using System.Collections.Generic;

namespace Transportation
{
    public class Engine
    {
        public double Temperature { get; set; }
        public int Rpm { get; set; }
        public double Thrust { get; set; }
        public List<int> RpmLog = new List<int>();

        private Random _random;

        public Engine()
        {
            _random = new Random();
        }

        public void Start()
        {
            Rpm = _random.Next(100, 201);
            Temperature = _random.Next(10, 51);
            Thrust = _random.Next(100, 301);
        }

        public void Run(int timeInSecs)
        {
            for (var i = 0; i < timeInSecs; i++)
            {
                var change = _random.Next(0, 2);
                var rpmChange = _random.Next(0, 21);
                var temperatureChange = rpmChange * _random.Next(0, 101) / 100;
                var thrustChange = rpmChange * _random.Next(0, 101) / 100;

                if (change == 0)
                {
                    Rpm = Rpm + rpmChange;
                    RpmLog.Add(Rpm);
                    Temperature = Temperature + temperatureChange;
                    Thrust = Thrust + thrustChange;
                }
                else
                {
                    Rpm = Rpm - rpmChange;
                    RpmLog.Add(Rpm);
                    Temperature = Temperature - temperatureChange;
                    Thrust = Thrust - thrustChange;
                }
            }
        }

    }
}
