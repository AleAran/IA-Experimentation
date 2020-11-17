using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Base Node for the behaviour tree
/// </summary>

//Since we want the node to be edited and receive diferent functions, we are going to need a delegate
public delegate BTNodeState NodeFunction();
public class BTNode : MonoBehaviour
{
    protected NodeFunction mNodeFunction;
    protected List<BTNode> mChilds;
    BTNode mParent;

    public BTNode()
    {
        mChilds = new List<BTNode>();
    }

    public bool addChild(BTNode node)
    {
        if (canHaveChilds())
        {
            mChilds.Add(node);
            node.setParent(this);
            return true;
        }

        return false;
    }

    public BTNode getChild(int index)
    {
        if (index >= 0 && index <= mChilds.Count - 1)
            return mChilds[index];

        return null;
    }

    protected virtual bool canHaveChilds() { return true; }

    public void setFunctionToExecute(NodeFunction function)
    {
        if (function != null)
            mNodeFunction = function;
    }

    public void setParent(BTNode parent)
    {
        mParent = parent;
    }
}
