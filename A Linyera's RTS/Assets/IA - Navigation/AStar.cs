using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//AStar Algorithm for Pathfinder, not much to say, there are a thousand of examples. The new addition to the pathfinder (outside of the overrides) is the Heuristic calculation.
public class AStar : Pathfinder
{
    List<Node> mOpenedNodes;

    public AStar()
    {
        mOpenedNodes = new List<Node>();
    }

    protected override void OnStart()
    {
        mOpenedNodes.Clear();
    }

    protected override Node makeCurrentNode()
    {
        Node currentNode = mOpenedNodes[0];

        if (mOpenedNodes.Count < 2)
        {
            mOpenedNodes.Remove(currentNode);
            return currentNode;
        }

        currentNode.setHeuristic(calculateCurrentHeuristic(currentNode));

        for (int i = 1; i < mOpenedNodes.Count; i++)
        {
            Node nodeToCheck = mOpenedNodes[i];
            nodeToCheck.setHeuristic(calculateCurrentHeuristic(nodeToCheck));

            if (nodeToCheck.getScore() + nodeToCheck.getHeuristic() < currentNode.getScore() + currentNode.getHeuristic())
                currentNode = nodeToCheck;
        }

        mOpenedNodes.Remove(currentNode);
        return currentNode;
    }

    int calculateCurrentHeuristic(Node currentNode)
    {
        int currentHeuristic = 0;

        if (currentNode.getCol() > mGoal.getCol())
        {
            for (int i = currentNode.getCol(); i > mGoal.getCol(); i--)
                currentHeuristic++;
        }

        else
        {
            for (int i = currentNode.getCol(); i < mGoal.getCol(); i++)
                currentHeuristic++;
        }

        if (currentNode.getRow() > mGoal.getRow())
        {
            for (int i = currentNode.getRow(); i > mGoal.getRow(); i--)
                currentHeuristic++;
        }

        else
        {
            for (int i = currentNode.getRow(); i < mGoal.getRow(); i++)
                currentHeuristic++;
        }

        return currentHeuristic;
    }

    protected override void openNode(Node node)
    {
        mOpenedNodes.Add(node);
    }

    protected override bool isInOpenedNodes(Node node)
    {
        return mOpenedNodes.Contains(node);
    }

    protected override bool isOpenedNodesEmpty()
    {
        if (mOpenedNodes.Count <= 0)
            return true;

        return false;
    }
}