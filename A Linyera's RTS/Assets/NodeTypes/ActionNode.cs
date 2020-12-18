using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : BTBaseNode
{

    public ActionNode() : base()
    {
        mNodeType = "action";
    }

    protected override bool canHaveChilds() { return false; }
}

