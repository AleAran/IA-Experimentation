using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparisonAndNode : BTBaseNode
{
    public ComparisonAndNode() : base()
    {
        mNodeType = "comparisonAnd";
    }

    public override BTNodeState start()
    {
        if (mChilds[0].start() == BTNodeState.Ready &&
            mChilds[1].start() == BTNodeState.Ready)
            return BTNodeState.Ready;

        return BTNodeState.Error;
    }
}