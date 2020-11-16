using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Soooo, since we are NOT going to do a complete TP, which, I'm rushing like a madman on coke, not unlike my day to day work, being honest. It needs a small explanation:
/// Grid -> This one contains unites everything, it has both the node array and Pathfinder.
/// Pathfinder -> this one contains the A*Star algorithm as child class, the idea behind this class is to make M O D U L A R. You don't want to have everything in the same class.
/// Node -> The smallest component, this class contains it's parents, an array of closest nodes, a score to mention if you can walk on it or no, and scores. 
/// 
/// Grid should be used by a manager that will build the grid, and then an agent should start the algortihm. Still haven't decided on what to do with the manager, I want to focus first on the FSM.
/// </summary>
public class Grid : MonoBehaviour
{
    Node[,] mNodes;
    Pathfinder mPathfinder;

    public int widthInFdd;
    public int heightInFd;

    int mWidth;
    int mHeight;

    void Start()
    {
        mNodes = new Node[mWidth, mHeight];
        mPathfinder = new AStar();
        mWidth = 30;
        mHeight = 30;

        buildGrid();
    }
    /// <summary>
    /// Build grid!
    /// </summary>
    void buildGrid()
    {
        for (int x = 0; x < mWidth; x++)
        {
            for (int y = 0; y < mHeight; y++)
            {
                mNodes[x, y] = new Node(x, y);
               //If I had time, I'd put here a logic tile to go with the position of the nodes.. Imagine an GameObject array of tiles.
            }
        }
    }

    /// <summary>
    /// Calculate path.
    /// </summary>
    /// <param name="start">Initial position</param>
    /// <param name="goal">Position of where we want to go</param>
    /// <returns></returns>
    public Stack<Vector3> startAlgorithm(Vector3 start, Vector3 goal)
    {
        mPathfinder.start(mNodes[(int)start.x, (int)start.z], //Start
                          mNodes[(int)goal.x, (int)goal.z], //Goal
                          mNodes); //Node List

        mPathfinder.buildPath(mNodes[(int)goal.x, (int)goal.z]);

        return mPathfinder.getPath();
    }

    public int getWidth()
    {
        return mWidth;
    }
    public int getHeight()
    {
        return mHeight;
    }

    public void makeWall(int col, int row, bool isWall)
    {
        mNodes[col, row].makeWall(isWall);
    }
    public bool isWall(int col, int row)
    {
        return mNodes[col, row].isWall();
    }

    public int getNodeScore(int col, int row)
    {
        return mNodes[col, row].getScore();
    }
    public void setNodeScore(int col, int row, int score)
    {
        mNodes[col, row].setScore(score);
        mNodes[col, row].setInitialScore(score);
    }

    public Node getNode(int col, int row)
    {
        return mNodes[col, row];
    }
}