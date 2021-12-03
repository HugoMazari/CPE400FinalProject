using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The sensor class.
    ///</summary>
    public class Sensors
    {
        ///<summary>
        /// Gets and sets the list of neighboring sensors.
        ///</summary>
        public List<string> NeighborSensors { get; }

        public void addNeighbor(string neighborName)
        {
            this.NeighborSensors.Add(neighborName);
        }

        ///<summary>
        /// Gets and sets the energy of the sensor.
        ///</summary>
        public int Energy { get; set; }

        ///<summary>
        /// Gets the name of the sensor.
        ///</summary>
        public string Name { get; private set; }

        ///<summary>
        /// The class constructor.
        ///</summary>
        ///<param name="name"> The name of the sensor. </param>
        ///<param name="energy"> The energy of the sensor. </param>
        public Sensors(string name, int energy)
        {
            NeighborSensors = new List<string>();
            Name = name;
            Energy = energy;
        }
    }
}