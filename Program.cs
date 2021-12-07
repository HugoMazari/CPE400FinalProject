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
        private string nodeNamingConvention = "Node #{0}";

        #endregion

        #region Methods

        ///<summary>
        /// The program's entry function.
        ///</summary>
        static void Main(string[] args)
        {
            //Variables
            Program programFunctions = new Program();
            List<Node> nodesGraph = programFunctions.CreateGraph();
            List<string> path = null;
            int numberOfPackets = 0;
            int lowestNodeEnergy = programFunctions.LowestNodeEnergy(nodesGraph);

            //Display initial values
            Console.WriteLine("ASH Node Energy Conservation");
            Console.WriteLine("Number of Nodes: {0}\n", programFunctions.numberOfNodes);
            
            Console.WriteLine("Initial Values\n--------------\n");
            programFunctions.DisplayValues(nodesGraph, true);
            Console.WriteLine("////////////////////////////////////////////////////////");

            //Packet sending loop
            while(lowestNodeEnergy > 0)
            {
                numberOfPackets++;
                path = programFunctions.UnamedAlgo(nodesGraph);

                foreach(var nodeName in path)
                {
                    Node node = null;
                    foreach(var item in nodesGraph)
                    {
                        if(item.Name == nodeName)
                        {
                            node = item;
                            break;
                        }
                    }
                    node.Energy -= Node.ProcessingCost;
                    if(node.Name != path[path.Count - 1])
                    {
                        node.Energy -= Node.TransmissionCost;
                    }
                }

                //Determine the lowest node energy.
                lowestNodeEnergy = programFunctions.LowestNodeEnergy(nodesGraph);
            }
            
            //Final values
            Console.WriteLine("////////////////////////////////////////////////////////");
            Console.WriteLine("Total Number of Packets Sent: {0}\n", numberOfPackets);
            Console.WriteLine("Final Values\n--------------\n");
            programFunctions.DisplayValues(nodesGraph);
        }

        /// <summary>
        /// Displays the values of the node graph.
        /// </summary>
        /// <param name="nodesGraph">The graph of nodes to display.</param>
        /// <param name="initialized">If display is first instance so to display the neighbors as well.</param>
        private void DisplayValues(List<Node> nodesGraph, bool initialized = false)
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

            foreach(var node in nodesGraph)
            {
                Console.Write("{0}              {1}", node.Name, node.Energy);
                if(initialized)
                {
                    string neighbors = "      ";
                    foreach(var neighbor in node.NeighborNodes)
                    {
                        neighbors += neighbor;
                        if(neighbor != node.NeighborNodes[node.NeighborNodes.Count - 1])
                            neighbors += ", ";
                    }
                    Console.WriteLine(neighbors);
                }
                else
                    Console.Write("\n");
            }
        }

        private int LowestNodeEnergy(List<Node> nodesGraph)
        {
            // Max amount of energy.
            int lowestEnergy = 9999;
            foreach(var node in nodesGraph)
            {
                if (node.Energy < lowestEnergy)
                    lowestEnergy = node.Energy;
            }
            return lowestEnergy;
        }

        /// <summary>
        /// Creates a graph based on the available number of nodes.
        /// </summary>
        /// <returns>Randomly created graph.</returns>
        private List<Node> CreateGraph()
        {
            List<Node> nodesGraph = new List<Node>();
            Random randomNumGen = new Random();

            //Creates the nodes in  the graph and assigns initial energy level.
            for (int i = 0; i < numberOfNodes; i++)
            {
                int initialEnergy = randomNumGen.Next(5, 50) * 100;
                string newNodeName = string.Format(nodeNamingConvention, i);
                nodesGraph.Add(new Node(newNodeName, initialEnergy));
            }

            //Randomly generates paths between nodes until graph is fully connected.
            while(!NodeGraph.IsConnected(nodesGraph))
            {
                Node [] randomNodes = new Node[2];
                //Randomly picks two nodes to connect with a path.
                for(int i = 0; i < 2; i++)
                {
                    int randomNodesIndex = randomNumGen.Next(0,numberOfNodes);
                    randomNodes[i] = nodesGraph[randomNodesIndex];
                }

                // Checks random path has not been selected before.
                if(!randomNodes[0].NeighborNodes.Contains(randomNodes[1].Name))
                {
                    randomNodes[0].AddNeighbor(randomNodes[1].Name);
                    randomNodes[1].AddNeighbor(randomNodes[0].Name);
                }
            }

            return nodesGraph;
        }

        private List<string> UnamedAlgo(List<Node> nodeGraph)
        {
            //Variables
            List<NodePath> allPaths = new List<NodePath>();
            Random randomNumGen = new Random();
            NodePath smallestPath = null;
            int [] startAndEndNode = {numberOfNodes, numberOfNodes}; //The source and destination nodes. 0 is start and 1 is destination.

            //Randomly determine two distinct nodes to transfer between.
            while(startAndEndNode[0] == startAndEndNode[1])
            {
                for(int i = 0; i < 2; i++)
                {
                    startAndEndNode[i] = randomNumGen.Next(0, numberOfNodes);
                }
            }

            //Find all paths
            allPaths = NodeGraph.FindAllPaths(nodeGraph, nodeGraph[startAndEndNode[0]], nodeGraph[startAndEndNode[1]]);

            //Choose path with shortest percentage.
            foreach(var path in allPaths)
            {
                if(smallestPath == null)
                {
                    smallestPath = path;
                }
                else
                {
                    double currSmallestPercentDiff = smallestPath.EneryConsumptionPercentage - smallestPath.LowestEnergyConsumption;
                    double pathSmallestPercentDiff = path.EneryConsumptionPercentage - path.LowestEnergyConsumption;
                    if(currSmallestPercentDiff > pathSmallestPercentDiff)
                    {
                        smallestPath = path;
                    }
                }
            }

            return smallestPath.NodesInPath;
        }
        
        #endregion
    }
}