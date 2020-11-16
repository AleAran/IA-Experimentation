using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    void buildGrid()
    {
        for (int x = 0; x < mWidth; x++)
        {
            for (int y = 0; y < mHeight; y++)
            {
                mNodes[x, y] = new Node(x, y);
               //Aca creamos Tile
            }
        }
    }

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