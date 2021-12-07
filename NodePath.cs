using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The node class.
    ///</summary>
    public class NodePath
    {
        /// <summary>
        /// Gets and sets the list of nodes in the path.
        /// </summary>
        public List<string> NodesInPath{get; set;}

        /// <summary>
        /// Gets and sets the total energy reserve of the path.
        /// </summary>
        public int TotalEnergy {get; set;}

        /// <summary>
        /// Gets the value of the average energy consumption to energy reserve percentage.
        /// </summary>
        public double EnergyConsumptionPercentage {get; private set;}

        /// <summary>
        /// Gets the value of the node with the lowest energy's consumption to reserve percentage.
        /// </summary>
        public double LowestEnergyConsumption {get; private set;}

        /// <summary>
        /// Constructor.
        /// </summary>
        public NodePath()
        {
            NodesInPath = new List<string>();
            TotalEnergy = 0;
            EnergyConsumptionPercentage = 0.0;
        }

        /// <summary>
        /// Calculates the energy and percentages of the path.
        /// </summary>
        /// <param name="nodeGraph">The graph of nodes.</param>
        public void CalculateEnergy(List<Node> nodeGraph)
        {
            /// <summary>
            /// Variables
            /// </summary>
            int nodeCount = 0;
            int totalTransmission = 0;
            int totalProcessing = 0;
            int lowestEnergy = nodeGraph[0].Energy;
            Node lowestEnergyNode = nodeGraph[0];

            foreach(var node in nodeGraph)
            {
                //Calculates total energy reserve and determines node with  the lowest energy.
                if(NodesInPath.Contains(node.Name))
                {
                    nodeCount++;
                    TotalEnergy += node.Energy;
                    if(node.Energy < lowestEnergy)
                    {
                        lowestEnergy = node.Energy;;
                        lowestEnergyNode = node;
                    }
                    else if(node.Energy == lowestEnergy && node != nodeGraph[nodeGraph.Count - 1])
                    {
                        lowestEnergy = node.Energy;
                        lowestEnergyNode = node;
                    }
                }
            }

            //Determines total energy cost and percentages.
            totalTransmission = (nodeCount - 1) * Node.TransmissionCost;
            totalProcessing = nodeCount * Node.ProcessingCost;
            EnergyConsumptionPercentage = ((double)(totalProcessing + totalTransmission) * 100.0) / (double)TotalEnergy;
            if(lowestEnergyNode != nodeGraph[nodeGraph.Count - 1])
                LowestEnergyConsumption = ((double)Node.ProcessingCost * 100.0) / (double)lowestEnergyNode.Energy;
            else LowestEnergyConsumption = ((double)Node.ProcessingCost + (double)Node.TransmissionCost) * 100.0 / (double)lowestEnergyNode.Energy;
        }
    }
}