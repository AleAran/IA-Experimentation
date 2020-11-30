using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // Start is called before the first frame update
    public int mInitialScore;

    public Node mNode { get; set; }

    void Start()
    {

    }
    public void Init(int x, int y) 
    {
        mNode = new Node(x,y);
        mNode.setInitialScore(mInitialScore);
        mNode.setScore(mInitialScore);
    }

}
