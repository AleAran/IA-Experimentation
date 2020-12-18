using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// No logic here, only structs
/// </summary>

//We want to save the nodes after editing them.
[System.Serializable]
public struct SerializedNode
{
    public string mFuntionName;
    public BtNodeType mNodeType;
    public int mChildCount;
    public int mIndexofFirstChild;
}
