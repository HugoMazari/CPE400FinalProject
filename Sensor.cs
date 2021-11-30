using System.Collections.Generic;
namespace CPE400FinalProject
{
    public class Sensors
    {
        public List<string> SensorNetwork { get; set; }

        public int Energy { get; set; }

        public string Name { get; set; }

        public Sensors(string name, int energy)
        {
            SensorNetwork = new List<string>();
            Name = name;
            Energy = energy;
        }
    }
}