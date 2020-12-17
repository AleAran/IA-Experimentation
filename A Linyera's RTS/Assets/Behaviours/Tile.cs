using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple Tile class, contains Node and allow us to handle the GameObject itself.
/// </summary>
public class Tile : MonoBehaviour
{
    public int mInitialScore;

    public Node mNode { get; set; }

    public void Init(int x, int y) 
    {
        mNode = new Node(x,y);
        mNode.setInitialScore(mInitialScore);
        mNode.setScore(mInitialScore);
    }

}
