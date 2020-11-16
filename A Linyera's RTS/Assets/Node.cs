using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node Classs!! Contains all yout needs for A*Star: Parent node, list of close nodes, walkable flag, position in array, aaand scores, to determine its worth to the pathfinder.
/// </summary>
public class Node
{
    Node mParent;
    protected List<Node> mAdjacent;

    bool mWalkable = false;

    protected int mCol;
    protected int mRow;

    int mTotalScore = 1;
    int mInitialScore = 1;
    int mHeuristic = 0;

    public Node(int col, int row)
    {
        mCol = col;
        mRow = row;
        mAdjacent = new List<Node>();
    }
    public void addToPath(ref Stack<Vector3> path)
    {
        if (mParent != null)
        {
            path.Push(new Vector3(mParent.getCol(), 0, mParent.getRow()));
            mParent.addToPath(ref path);
        }
    }

    public virtual void addAdjacentNode(Node neighbour)
    {
        mAdjacent.Add(neighbour);
        neighbour.setParent(this);
        neighbour.setScore(mTotalScore + neighbour.getScore());
    }

    public List<Node> getAdjacentNodes()
    {
        return mAdjacent;
    }

    public void setParent(Node parent)
    {
        mParent = parent;
    }
    public Node getParent()
    {
        return mParent;
    }
    public void initParent()
    {
        mParent = null;
        mAdjacent.Clear();
        mTotalScore = mInitialScore;
    }

    public void setScore(int score)
    {
        mTotalScore = score;

    }
    public void setInitialScore(int startingScore)
    {
        mInitialScore = startingScore;
    }
    public int getScore()
    {
        return mTotalScore;
    }

    public void setHeuristic(int heuristic)
    {
        mHeuristic = heuristic;
    }
    public int getHeuristic()
    {
        return mHeuristic;
    }

    public int getCol() { return mCol; }
    public int getRow() { return mRow; }

    public void makeWall(bool isWall) { mWalkable = isWall; }
    public bool isWall() { return mWalkable; }

}