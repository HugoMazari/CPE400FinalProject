using System;

namespace CPE400FinalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Sensors sensors = new Sensors();
            sensors.SensorNetwork.Add("Test");
            Console.WriteLine(sensors.SensorNetwork[0]);
        }
    }
}
