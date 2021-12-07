using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The node class.
    ///</summary>
    public class Node
    {
        #region Properties

        ///<summary>
        /// Gets and sets the list of neighboring nodes.
        ///</summary>
        public List<string> NeighborNodes { get; }

        ///<summary>
        /// Gets and sets the energy of the node.
        ///</summary>
        public int Energy { get; set; }

        ///<summary>
        /// Gets the name of the node.
        ///</summary>
        public string Name { get; private set; }

        public static int TransmissionCost = 100;
        public static int ProcessingCost = 10; 

        #endregion

        #region Methods

        ///<summary>
        /// The class constructor.
        ///</summary>
        ///<param name="name"> The name of the node. </param>
        ///<param name="energy"> The energy of the node. </param>
        public Node(string name, int energy)
        {
            NeighborNodes = new List<string>();
            Name = name;
            Energy = energy;
        }

        /// <summary>
        /// Adds a node to the list of neighbors.
        /// </summary>
        /// <param name="neighborName">The name of the neighboring node.</param>
        public void AddNeighbor(string neighborName)
        {
            NeighborNodes.Add(neighborName);
        }

        #endregion
    }
}