using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base Node for the behaviour tree
/// </summary>

//Since we want the node to be edited and receive diferent functions, we are going to need a delegate
public delegate BTNodeState NodeFunction();

public class BTBaseNode : MonoBehaviour
{
    protected NodeFunction mNodeFunction;
    protected List<BTBaseNode> mChilds;
    BTBaseNode mParent;

    protected string mNodeType;

    public BTBaseNode()
    {
        mChilds = new List<BTBaseNode>();
    }

    protected virtual bool canHaveChilds() { return true; }

    public bool addChild(BTBaseNode node)
    {
        if (canHaveChilds())
        {
            mChilds.Add(node);
            node.setParent(this);
            return true;
        }

        return false;
    }

    public BTBaseNode getChild(int index)
    {
        if (index >= 0 && index <= mChilds.Count - 1)
            return mChilds[index];

        return null;
    }

    public void setFunctionToExecute(NodeFunction function)
    {
        if (function != null)
            mNodeFunction = function;
    }

    public virtual BTNodeState start() 
    { 
        return mNodeFunction(); 
    }


    public void setParent(BTBaseNode parent)
    {
        mParent = parent;
    }
}
