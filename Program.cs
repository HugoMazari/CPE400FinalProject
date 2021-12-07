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
                bool networkOffline = false;
                numberOfPackets++;
                path = programFunctions.AshAlgo(nodesGraph);

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

                    if(node.Energy <= 0)
                    {
                        node.Energy = 0;
                        if(!networkOffline)
                        {
                            networkOffline = true;
                        }
                    }
                }

                //Determine the lowest node energy.
                lowestNodeEnergy = programFunctions.LowestNodeEnergy(nodesGraph);
                Console.WriteLine("Sending Packet #{0}", numberOfPackets);
                Console.WriteLine("Source Node: {0}\nDestination Node: {1}", path[0], path[path.Count - 1]);
                Console.Write("Path: {0}", path[0]);
                for(int i = 1; i < path.Count; i++)
                    Console.Write(" -> {0}", path[i]);
                Console.WriteLine();
                if(networkOffline)
                {
                    Console.WriteLine("ENERGY DEPLETED! DELIVERY FAILED!");
                    numberOfPackets--;
                }
                Console.WriteLine("Values\n--------------\n");
                programFunctions.DisplayValues(nodesGraph);
                Console.WriteLine("////////////////////////////////////////////////////////");
            }
            
            //Final values
            Console.WriteLine("Total Number of Packets Successfully Sent: {0}\n", numberOfPackets);
            Console.WriteLine("Final Values\n--------------\n");
            programFunctions.DisplayValues(nodesGraph);

            Console.WriteLine("\n\nPress any key to close application.");
            Console.ReadKey();
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

        /// <summary>
        /// Finds the node with the least amount of energy.
        /// </summary>
        /// <param name="nodesGraph">The graph of nodes.</param>
        /// <returns>The node with the least amount of energy.</returns>
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

        /// <summary>
        /// Finds all paths from source to destination and determines the one with the least amount of energy consumption.
        /// </summary>
        /// <param name="nodeGraph">The graph of nodes.</param>
        /// <returns>The graph that consumes the least power.</returns>
        private List<string> AshAlgo(List<Node> nodeGraph)
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

            //Choose path with the smallest percentage difference.
            foreach(var path in allPaths)
            {
                if(smallestPath == null)
                {
                    smallestPath = path;
                }
                else
                {
                    //Measures current path's percentage difference between average energy consumption percentage and the consumption
                    //percentage of the node with the smallest energy reserve.
                    double currSmallestPercentDiff = smallestPath.EnergyConsumptionPercentage - smallestPath.LowestEnergyConsumption;
                    double pathSmallestPercentDiff = path.EnergyConsumptionPercentage - path.LowestEnergyConsumption;
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