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

            //Makes a loop with nodes, remove and generate completely random graph.
            for (int i = 0; i < numberOfSensors - 1; i++)
            {
                int nextDoorNeighborIndex = i + 1;
                string nextDoorNeighborName = string.Format(sensorNamingConvention, nextDoorNeighborIndex);
                string personalName = string.Format(sensorNamingConvention, i);
                sensorsGraph[i].NeighborSensors.Add(nextDoorNeighborName);
                sensorsGraph[nextDoorNeighborIndex].NeighborSensors.Add(personalName);
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
    }
}
