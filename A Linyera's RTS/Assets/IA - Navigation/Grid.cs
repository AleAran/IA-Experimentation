using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Soooo, since I'm rushing like a madman on coke, not unlike my day to day work, being honest. It needs a small explanation:
/// Grid -> This one contains unites everything, it has both the node array and Pathfinder.
/// Pathfinder -> this one contains the A*Star algorithm as child class, the idea behind this class is to make M O D U L A R. You don't want to have everything in the same class.
/// Node -> The smallest component, this class contains it's parents, an array of closest nodes, a score to mention if you can walk on it or no, and scores. 
/// 
/// Grid should be used by a manager that will build the grid, and then an agent should start the algortihm. Still haven't decided on what to do with the manager, I want to focus first on the FSM.
/// Updated: Decided to not create a manager, it would be adding an extra layer because reasons. So I'll leave the BuildGrid a the start of this class.
/// </summary>
public class Grid : MonoBehaviour
{
    Node[,] mNodes;
    
    public GameObject mGrassTile;
    public GameObject mDirtTile;
    public GameObject mSandTile;
    public GameObject mMine;
    public GameObject mBase;

    public Spawner mSpawner;

    public int mTotalRows;
    public int mTotalCols;

    void Start()
    {
        mNodes = new Node[mTotalRows, mTotalCols];

        buildGrid();
    }

    /// <summary>
    /// Build grid!
    /// </summary>
    void buildGrid()
    {
        int mineCounter = 0;
        for (int x = 0; x < mTotalRows; x++)
        {
            for (int y = 0; y < mTotalCols; y++)
            {
                //God, have mercy of my soul, for I've made crimes against legibility. Long story short, you if the first case is true, you create a DirtTile, if not,
                //another comparison is made with random values, if true, create sand tile, if false, create Grass.
                GameObject selectedTile = (Random.Range(0, 12) == 2)? mDirtTile : (Random.Range(0, 10) == 1) ? mSandTile : mGrassTile;

                //We instantiate a tile, initialize it and add it to the Nodes List.
                Tile tile = Instantiate(selectedTile, new Vector2(x, y), Quaternion.identity, gameObject.transform).GetComponent<Tile>();
                tile.Init(x, y);
                mNodes[x, y] = tile.mNode;

                bool isBase = (x == base.transform.position.x && y == base.transform.position.y) ? true : false;

                //First Mines are set here
                bool isMine = (Random.Range(0, 10) == 3 && mineCounter<2) ? Instantiate(mMine, new Vector2(x, y), Quaternion.identity, gameObject.transform) : false;
                if (isMine)
                {
                    mineCounter++;
                    mSpawner.IncreaseMineCount();
                }

                //Random Walls appear blocking progress, just like real life! Who needs fancy graphics?
                makeWall(x, y, Random.Range(0,20) == 2 && !isMine && !isBase, tile.gameObject);           
            }
        }
    }

    /// <summary>
    /// Calculate path.
    /// </summary>
    /// <param name="start">Initial position</param>
    /// <param name="goal">Position of where we want to go</param>
    /// <returns></returns>
    public Stack<Vector3> startAlgorithm(Vector3 start, Vector3 goal, Pathfinder pathFinder)
    {
        pathFinder.start(mNodes[(int)start.x, (int)start.y], //Start
                          mNodes[(int)goal.x, (int)goal.y], //Goal
                          (Node[,])mNodes.Clone()); //Node List

        pathFinder.buildPath(mNodes[(int)goal.x, (int)goal.y]);

        return pathFinder.getPath();
    }

    public int getWidth()
    {
        return mTotalRows;
    }
    public int getHeight()
    {
        return mTotalCols;
    }

    public void makeWall(int row, int col, bool isWall, GameObject Tile)
    {
        mNodes[row, col].makeWall(isWall);
        if (isWall && Tile != null)
        {
            Tile.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    public bool isWall(int row, int col)
    {
        return mNodes[row, col].isWall();
    }

    public int getNodeScore(int row, int col)
    {
        return mNodes[row, col].getScore();
    }

    public Node getNode(int row, int col)
    {
        return mNodes[row, col];
    }
}