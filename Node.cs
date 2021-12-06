using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The sensor class.
    ///</summary>
    public class Node
    {
        #region Properties

        ///<summary>
        /// Gets and sets the list of neighboring sensors.
        ///</summary>
        public List<string> NeighborNodes { get; }

        ///<summary>
        /// Gets and sets the energy of the sensor.
        ///</summary>
        public int Energy { get; set; }

        ///<summary>
        /// Gets the name of the sensor.
        ///</summary>
        public string Name { get; private set; }

        #endregion

        #region Methods

        ///<summary>
        /// The class constructor.
        ///</summary>
        ///<param name="name"> The name of the sensor. </param>
        ///<param name="energy"> The energy of the sensor. </param>
        public Node(string name, int energy)
        {
            NeighborNodes = new List<string>();
            Name = name;
            Energy = energy;
        }

        /// <summary>
        /// Adds a sensor to the list of neighbors.
        /// </summary>
        /// <param name="neighborName">The name of the neighboring sensor.</param>
        public void AddNeighbor(string neighborName)
        {
            NeighborNodes.Add(neighborName);
        }

        #endregion
    }
}