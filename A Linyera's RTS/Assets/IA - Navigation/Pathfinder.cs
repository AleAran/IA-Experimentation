using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///Generic Pathfinding logic, in case we want to use multiple algorithm. 
///To give an example, AStar! Just make it a child from this class, override the virtual functions and add it's specific functions. It helps to diferentiate what each algorithm has in common, and avoids monolith clases. I hate those.
/// </summary>
public class Pathfinder
{
    protected List<Node> mClosedNodes;
    protected Stack<Vector3> mPath;

    Node[,] mNodes;

    protected Node mGoal;

    public void start(Node start, Node goal, Node[,] nodes)
    {
        mNodes = nodes;

        for (int i = 0; i < mNodes.GetLength(0); i++)
        {
            for (int j = 0; j < mNodes.GetLength(1); j++)
            {
                mNodes[i, j].initParent();
            }
        }
        if (mClosedNodes == null)
            mClosedNodes = new List<Node>();
        else
            mClosedNodes.Clear();

        mGoal = goal;

        OnStart();

        Node currentNode;

        openNode(start);

        while (!isOpenedNodesEmpty())
        {
            currentNode = makeCurrentNode();

            if (currentNode != goal)
            {
                closeNode(currentNode);

                findCurrentNodeAdjacents(currentNode);

                foreach (Node neighbour in currentNode.getAdjacentNodes())
                    openNode(neighbour);
            }
        }
    }

    public void buildPath(Node goal)
    {
        if (mPath == null)
            mPath = new Stack<Vector3>();
        else
            mPath.Clear();

        goal.addToPath(ref mPath);
    }

    public Stack<Vector3> getPath()
    {
        return mPath;
    }
    void findCurrentNodeAdjacents(Node currentNode)
    {
        Node left = null;

        if (currentNode.getCol() != 0)
            left = mNodes[currentNode.getCol() - 1, currentNode.getRow()];

        if (left != null)
        {
            if (!isInOpenedNodes(left) && !mClosedNodes.Contains(left) && !left.isWall())
                currentNode.addAdjacentNode(left);
        }
        
        Node right = null;

        if (currentNode.getCol() != mNodes.GetLength(0) - 1)
            right = mNodes[currentNode.getCol() + 1, currentNode.getRow()];

        if (right != null)
        {
            if (!isInOpenedNodes(right) && !mClosedNodes.Contains(right) && !right.isWall())
                currentNode.addAdjacentNode(right);
        }

        Node up = null;

        if (currentNode.getRow() != 0)
            up = mNodes[currentNode.getCol(), currentNode.getRow() - 1];

        if (up != null)
        {
            if (!isInOpenedNodes(up) && !mClosedNodes.Contains(up) && !up.isWall())
                currentNode.addAdjacentNode(up);
        }
       
        Node down = null;

        if (currentNode.getRow() != mNodes.GetLength(1) - 1)
            down = mNodes[currentNode.getCol(), currentNode.getRow() + 1];

        if (down != null)
        {
            if (!isInOpenedNodes(down) && !mClosedNodes.Contains(down) && !down.isWall())
                currentNode.addAdjacentNode(down);
        }
    }
    protected virtual void OnStart() { }

    protected virtual void openNode(Node node) { }

    protected virtual void closeNode(Node node)
    {
        mClosedNodes.Add(node);
    }

    protected virtual Node makeCurrentNode() { return null; }
    protected virtual bool isInOpenedNodes(Node node) { return true; }
    protected virtual bool isOpenedNodesEmpty() { return false; }
}