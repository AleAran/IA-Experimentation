using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ConditionNode : BTBaseNode
{
    public ConditionNode() : base()
    {
        mNodeType = "conditional";
    }

    protected override bool canHaveChilds() { return false; }

    public override BTNodeState start()
    {
        BTNodeState result = mNodeFunction();

        if (result == BTNodeState.Running)
            return BTNodeState.Error;

        return result;
    }
}