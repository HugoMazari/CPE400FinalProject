using System;
using System.Collections.Generic;

namespace CPE400FinalProject
{
    public class NodeGraph
    {
        #region Properties

        /// <summary>
        /// Gets the array of which nodes are visited.
        /// </summary>
        public bool[] IsVisited {get; private set;}

        #endregion

        #region Methods

        /// <summary>
        /// The class constructor.
        /// </summary>
        /// <param name="amountOfNodes">The amount of nodes in the graph.</param>
        /// <param name="defaultState">The desired default state of the IsVisited graph, defaults to false.</param>
        public NodeGraph(int amountOfNodes, bool defaultState = false)
        {
            IsVisited = new bool[amountOfNodes];
            for (int i = 0; i < amountOfNodes; i++)
            {
                IsVisited[i] = false;
            }
        }

        /// <summary>
        /// Checks if a graph of nodes is fully connected.
        /// </summary>
        /// <param name="nodeGraph">The node graph.</param>
        /// <returns>If the graph is fully connected.</returns>
        public static bool IsConnected(List<Node> nodeGraph)
        {
            NodeGraph connectionTest = new NodeGraph(nodeGraph.Count, false);
            connectionTest.CheckNeighbors(nodeGraph, 0);
            bool isConnected = true;

            foreach(bool connection in connectionTest.IsVisited)
            {
                isConnected = isConnected && connection;
            }

            return isConnected;
        }

        /// <summary>
        /// Checks what nodes each node is connected too.
        /// </summary>
        /// <param name="nodeGraph">The graph of nodes.</param>
        /// <param name="index">The index of the selected node.</param>
        public void CheckNeighbors(List<Node> nodeGraph, int index)
        {
            //Marks node as visited.
            IsVisited[index] = true;
            
            //Checks all neighbors of selected node.
            foreach(string nodeName in nodeGraph[index].NeighborNodes)
            {
                int nodeNameIndex = default;
                //Gets the index of current neighbor node.
                foreach(Node node in nodeGraph)
                {
                    if(node.Name == nodeName)
                    {
                        nodeNameIndex = nodeGraph.IndexOf(node);
                        break;
                    }
                }

                // If it has not been marked as visited, mark it as such and check it's neighbors.
                if(!IsVisited[nodeNameIndex])
                {
                    CheckNeighbors(nodeGraph, nodeNameIndex);
                }
            }
        }

        /// <summary>
        /// Finds all unique paths from the source to destination nodes. 
        /// </summary>
        /// <param name="nodeGraph">The graph of nodes.</param>
        /// <param name="srcSensor">The source node.</param>
        /// <param name="destSensor">The destination node.</param>
        /// <returns>List of all possible paths.</returns>
        public static List<NodePath> FindAllPaths(List<Node> nodeGraph, Node srcSensor, Node destSensor)
        {
            List<NodePath> allPaths = new List<NodePath>();
            NodeGraph pathFinder = new NodeGraph(1);
            NodePath startPath = new NodePath();
            startPath.NodesInPath.Add(srcSensor.Name);
            pathFinder.FindNode(nodeGraph, srcSensor, destSensor, allPaths, startPath);
            return allPaths;
            
        }

    /// <summary>
    /// Find desired node in graph. 
    /// </summary>
    /// <param name="nodeGraph">The graph of nodes.</param>
    /// <param name="srcSensor">The node to start the search in.</param>
    /// <param name="destSensor">The target node.</param>
    /// <param name="allPaths">List of all possible paths to take.</param>
    /// <param name="currPath">The current path being checked.</param>
        private void FindNode(List<Node> nodeGraph, Node srcSensor, Node destSensor, List<NodePath> allPaths, NodePath currPath)
        {
            foreach (var neighbor in srcSensor.NeighborNodes)
            {
                if(neighbor == destSensor.Name)
                {
                    NodePath temp = new NodePath();
                    foreach (var nodes in currPath.NodesInPath)
                    {
                        temp.NodesInPath.Add(nodes);
                    }
                    temp.CalculateEnergy(nodeGraph);
                    allPaths.Add(temp);
                }
                else if (!currPath.NodesInPath.Contains(neighbor))
                {
                    Node neighborNode = null;
                    foreach (var node in nodeGraph)
                    {
                        if(node.Name == neighbor)
                        {
                            neighborNode = node;
                            break;
                        }
                    }
                    
                    currPath.NodesInPath.Add(neighbor);
                    FindNode(nodeGraph, neighborNode, destSensor, allPaths, currPath);
                    currPath.NodesInPath.Remove(neighbor);
                }
            }
        }
        #endregion
    }
}