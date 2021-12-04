using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The main class.
    ///</summary>
    class Program
    {
        private int numberOfSensors = 7;
        private string sensorNamingConvention = "Sensor #{0}";

        ///<summary>
        /// The main function.
        ///</summary>
        static void Main(string[] args)
        {
            Program programFunctions = new Program();
            List<Sensors> sensorsGraph = programFunctions.createGraph();
            Console.WriteLine("Test");
        }

        /// <summary>
        /// Creates a graph based on the available number of nodes.
        /// </summary>
        /// <returns>Randomly created graph.</returns>
        private List<Sensors> createGraph()
        {
            List<Sensors> sensorsGraph = new List<Sensors>();
            Random randomNumGen = new Random();

            for (int i = 0; i < numberOfSensors; i++)
            {
                int initialEnergy = randomNumGen.Next(5, 50) * 100;
                string newSensorName = string.Format(sensorNamingConvention, i);
                sensorsGraph.Add(new Sensors(newSensorName, initialEnergy));
                sensorsGraph[i].NeighborSensors.Add(newSensorName);
            }

            for (int i = 0 ; i < numberOfSensors; i++)
            {
                int neighborNumber = randomNumGen.Next(3, 4);
                for (int j = 0; j < neighborNumber; j++)
                {
                    int randomNeighbor = randomNumGen.Next(1, numberOfSensors);
                    List<string> neighbors = sensorsGraph[i].NeighborSensors;
                    string neighborName = string.Format(sensorNamingConvention, randomNeighbor);
                    if (!neighbors.Contains(neighborName))
                    {
                        sensorsGraph[i].addNeighbor(neighborName);
                        string source = string.Format(sensorNamingConvention, i);
                        sensorsGraph[randomNeighbor].addNeighbor(source);
                    }
                }
            }
            return sensorsGraph;
        }

        private bool IsConnected(List<Sensors> sensorGraph)
        {
            bool[] isVisited = new bool[numberOfSensors];
            for(int i = 0; i < numberOfSensors; i++)
            {
                isVisited[i] = false;
            }


        }

        /// <summary>
        /// Checks what sensors a specific sensor neighbors.
        /// </summary>
        /// <param name="sensor">Sensor being checked.</param>
        /// <param name="isVisited">Array checking a sensors neighbor.</param>
        private void CheckNeighbors(Sensors sensor, out bool[] isVisited)
        {

        }
    }
}
