using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorNode : BTBaseNode
{
    int i = 0;

    public SelectorNode() : base()
    {
        mNodeType = "selector";
    }

    public override BTNodeState start()
    {
        BTNodeState result;

        while (i < mChilds.Count)
        {
            result = mChilds[i].start();

            if (result == BTNodeState.Error)
                i++;

            else if (result == BTNodeState.Running)
                return result;

            else break;
        }

        i = 0;
        return BTNodeState.Ready;
    }
}
