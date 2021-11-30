using System;

namespace CPE400FinalProject
{
    ///<summary>
    /// The main class.
    ///</summary>
    class Program
    {
        ///<summary>
        /// The main function.
        ///</summary>
        static void Main(string[] args)
        {
            Sensors sensors = new Sensors("FirstSensor", 20);
            sensors.NeighborSensors.Add("Test");
            Console.WriteLine(sensors.NeighborSensors[0]);
        }
    }
}
