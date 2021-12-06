using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The main class.
    ///</summary>
    class Program
    {
        #region Private Members

        private int numberOfNodes = 7;
        private string sensorNamingConvention = "Node #{0}";

        #endregion

        #region Methods

        ///<summary>
        /// The program's entry function.
        ///</summary>
        static void Main(string[] args)
        {
            //Variables
            Program programFunctions = new Program();
            List<Node> sensorsGraph = programFunctions.CreateGraph();
            int numberOfPackets = 0;
            int lowestNodeEnergy = programFunctions.LowestNodeEnergy(sensorsGraph);

            //Display initial values
            Console.WriteLine("ASH Node Energy Conservation");
            Console.WriteLine("Number of Nodes: {0}\n", programFunctions.numberOfNodes);
            Console.WriteLine("Initial Values\n--------------\n");
            programFunctions.DisplayValues(sensorsGraph, true);
            Console.WriteLine("////////////////////////////////////////////////////////");

            //Packet sending loop
            while(lowestNodeEnergy > 0)
            {
                numberOfPackets++;
            }
            
            //Final values
            Console.WriteLine("////////////////////////////////////////////////////////");
            Console.WriteLine("Total Number of Packets Sent: {0}\n", numberOfPackets);
            Console.WriteLine("Final Values\n--------------\n");
            programFunctions.DisplayValues(sensorsGraph);
        }

        /// <summary>
        /// Displays the values of the sensor graph.
        /// </summary>
        /// <param name="sensorsGraph">The graph of sensors to display.</param>
        /// <param name="initialized">If display is first instance so to display the neighbors as well.</param>
        private void DisplayValues(List<Node> sensorsGraph, bool initialized = false)
        {
            string labels = "Name              Energy Level";
            string separator = "----              ------------";
            if(initialized)
            {
                labels += "      Connected Nodes";
                separator += "      ---------------";
            }
            Console.WriteLine(labels);
            Console.WriteLine(separator);

            foreach(var sensor in sensorsGraph)
            {
                Console.Write("{0}              {1}", sensor.Name, sensor.Energy);
                if(initialized)
                {
                    string neighbors = "      ";
                    foreach(var neighbor in sensor.NeighborNodes)
                    {
                        neighbors += neighbor;
                        if(neighbor != sensor.NeighborNodes[sensor.NeighborNodes.Count - 1])
                            neighbors += ", ";
                    }
                    Console.WriteLine(neighbors);
                }
                else
                    Console.Write("\n");
            }
        }

        private int LowestNodeEnergy(List<Node> sensorsGraph)
        {
            // Max amount of energy.
            int lowestEnergy = 9999;
            foreach(var sensor in sensorsGraph)
            {
                if (sensor.Energy < lowestEnergy)
                    lowestEnergy = sensor.Energy;
            }
            return lowestEnergy;
        }

        /// <summary>
        /// Creates a graph based on the available number of nodes.
        /// </summary>
        /// <returns>Randomly created graph.</returns>
        private List<Node> CreateGraph()
        {
            List<Node> sensorsGraph = new List<Node>();
            Random randomNumGen = new Random();

            //Creates the sensors in  the graph and assigns initial energy level.
            for (int i = 0; i < numberOfNodes; i++)
            {
                int initialEnergy = randomNumGen.Next(5, 50) * 100;
                string newNodeName = string.Format(sensorNamingConvention, i);
                sensorsGraph.Add(new Node(newNodeName, initialEnergy));
            }

            //Randomly generates paths between sensors until graph is fully connected.
            while(!NodeGraph.IsConnected(sensorsGraph))
            {
                Node [] randomNodes = new Node[2];
                //Randomly picks two sensors to connect with a path.
                for(int i = 0; i < 2; i++)
                {
                    int randomNodesIndex = randomNumGen.Next(0,numberOfNodes);
                    randomNodes[i] = sensorsGraph[randomNodesIndex];
                }

                // Checks random path has not been selected before.
                if(!randomNodes[0].NeighborNodes.Contains(randomNodes[1].Name))
                {
                    randomNodes[0].AddNeighbor(randomNodes[1].Name);
                    randomNodes[1].AddNeighbor(randomNodes[0].Name);
                }
            }

            return sensorsGraph;
        }
        
        #endregion
    }
}