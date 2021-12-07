using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    ///<summary>
    /// The node class.
    ///</summary>
    public class NodePath
    {
        public List<string> NodesInPath{get; set;}

        public int TotalEnergy {get; set;}

        public double EneryConsumptionPercentage {get; private set;}

        public double LowestEnergyConsumption {get; private set;}

        public NodePath()
        {
            NodesInPath = new List<string>();
            TotalEnergy = 0;
            EneryConsumptionPercentage = 0.0;
        }

        public void CalculateEnergy(List<Node> nodeGraph)
        {
            int nodeCount = 0;
            int totalTransmission = 0;
            int totalProcessing = 0;
            int lowestEnergy = nodeGraph[0].Energy;
            Node lowestEnergyNode = nodeGraph[0];

            foreach(var node in nodeGraph)
            {
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

            totalTransmission = (nodeCount - 1) * Node.TransmissionCost;
            totalProcessing = nodeCount * Node.ProcessingCost;
            EneryConsumptionPercentage = ((double)(totalProcessing + totalTransmission) * 100.0) / (double)TotalEnergy;
            if(lowestEnergyNode != nodeGraph[nodeGraph.Count - 1])
                LowestEnergyConsumption = ((double)Node.ProcessingCost * 100.0) / (double)lowestEnergyNode.Energy;
            else LowestEnergyConsumption = ((double)Node.ProcessingCost + (double)Node.TransmissionCost) * 100.0 / (double)lowestEnergyNode.Energy;
        }
    }
}