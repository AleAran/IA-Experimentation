using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceNode : BTBaseNode
{
    int i = 0;

    public SequenceNode() : base()
    {
        mNodeType = "sequence";
    }

    public override BTNodeState start()
    {
        BTNodeState result;

        while (i < mChilds.Count)
        {
            result = mChilds[i].start();

            if (result == BTNodeState.Ready)
                i++;

            else if (result == BTNodeState.Running)
                return result;

            else break;
        }

        i = 0;
        return BTNodeState.Error;
    }
}
