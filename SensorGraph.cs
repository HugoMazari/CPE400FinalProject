using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    public class SensorGraph
    {
        #region Properties

        /// <summary>
        /// Gets the array of which sensors are visited.
        /// </summary>
        public bool[] IsVisited {get; private set;}

        #endregion

        #region Methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="amountOfSensors">The amount of sensors in the graph.</param>
        /// <param name="defaultState">The desired default state of the IsVisited graph, defaults to false.</param>
        public SensorGraph(int amountOfSensors, bool defaultState = false)
        {
            IsVisited = new bool[amountOfSensors];
            for (int i = 0; i < amountOfSensors; i++)
            {
                IsVisited[i] = false;
            }
        }

        /// <summary>
        /// Checks if a graph of sensors is fully connected.
        /// </summary>
        /// <param name="sensorGraph">The sensor graph.</param>
        /// <returns>If the graph is fully connected.</returns>
        public static bool IsConnected(List<Sensors> sensorGraph)
        {
            SensorGraph connectionTest = new SensorGraph(sensorGraph.Count, false);
            connectionTest.CheckNeighbors(sensorGraph, 0);
            bool isConnected = true;

            foreach(bool connection in connectionTest.IsVisited)
            {
                isConnected = isConnected && connection;
            }

            return isConnected;
        }

        /// <summary>
        /// Checks what sensors each sensor is connected too.
        /// </summary>
        /// <param name="sensorGraph">The graph of sensors.</param>
        /// <param name="index">The index of the selected sensor.</param>
        public void CheckNeighbors(List<Sensors> sensorGraph, int index)
        {
            //Marks sensor as visited.
            IsVisited[index] = true;
            
            //Checks all neighbors of selected sensor.
            foreach(string sensorName in sensorGraph[index].NeighborSensors)
            {
                int sensorNameIndex = default;
                //Gets the index of current neighbor sensor.
                foreach(Sensors sensor in sensorGraph)
                {
                    if(sensor.Name == sensorName)
                    {
                        sensorNameIndex = sensorGraph.IndexOf(sensor);
                        break;
                    }
                }

                // If it has not been marked as visited, mark it as such and check it's neighbors.
                if(!IsVisited[sensorNameIndex])
                {
                    CheckNeighbors(sensorGraph, sensorNameIndex);
                }
            }
        }

        #endregion
    }
}