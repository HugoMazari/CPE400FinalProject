using System;
using System.Collections.Generic;
using System.Linq;

namespace CPE400FinalProject
{
    ///<summary>
    /// The main class.
    ///</summary>
    class Program
    {
        #region Private Members

        private int numberOfSensors = 7;
        private string sensorNamingConvention = "Sensor #{0}";

        #endregion

        #region Methods

        ///<summary>
        /// The program's entry function.
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

            //Creates the sensors in  the graph and assigns initial energy level.
            for (int i = 0; i < numberOfSensors; i++)
            {
                int initialEnergy = randomNumGen.Next(5, 50) * 100;
                string newSensorName = string.Format(sensorNamingConvention, i);
                sensorsGraph.Add(new Sensors(newSensorName, initialEnergy));
                sensorsGraph[i].AddNeighbor(newSensorName);
            }

            //Randomly generates paths between sensors until graph is fully connected.
            while(!SensorGraph.IsConnected(sensorsGraph))
            {
                Sensors [] randomSensors = new Sensors[2];
                //Randomly picks two sensors to connect with a path.
                for(int i = 0; i < 2; i++)
                {
                    int randomSensorsIndex = randomNumGen.Next(0,numberOfSensors);
                    randomSensors[i] = sensorsGraph[randomSensorsIndex];
                }

        private bool[] IsConnected(List<Sensors> sensorGraph)
        {
            bool[] isVisited = new bool[numberOfSensors];
            for(int i = 0; i < numberOfSensors; i++)
            {
                isVisited[i] = false;
            }

            for(int i = 0; i < numberOfSensors; i++)
            {
                isVisited[i] = sensorGraph[i].NeighborSensors.Any();
            }
            return isVisited;
        }

        /// <summary>
        /// Checks what sensors a specific sensor neighbors.
        /// </summary>
        /// <param name="sensor">Sensor being checked.</param>
        /// <param name="isVisited">Array checking a sensors neighbor.</param>
        private void CheckNeighbors(Sensors sensor, out bool[] isVisited)
        {

                // Checks random path has not been selected before.
                if(!randomSensors[0].NeighborSensors.Contains(randomSensors[1].Name))
                {
                    randomSensors[0].AddNeighbor(randomSensors[1].Name);
                    randomSensors[1].AddNeighbor(randomSensors[0].Name);
                }
            }

            return sensorsGraph;
        }
        
        #endregion
    }
}