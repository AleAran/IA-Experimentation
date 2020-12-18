using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class BTGenerator : ScriptableObject
{
    public List<SerializedNode> mSerializedNode = new List<SerializedNode>();

    public int loadTree<T>(int index, out BTBaseNode node, T instanceType)
    {
        SerializedNode serializableNode = mSerializedNode[index];

        switch (serializableNode.mNodeType)
        {
            //We have this as placeholder until we have more nodestypes created
            case BtNodeType.Sequencer:
                node = new SequenceNode();
                break;
            case BtNodeType.Selector:
                node = new BTBaseNode();
                break;
            default:
                node = new BTBaseNode();
                break;
        }

        for (int i = 0; i < serializableNode.mChildCount; i++)
        {
            BTBaseNode childNode;
            index = loadTree<T>(++index, out childNode, instanceType);
            node.addChild(childNode);
        }

        return index;
    }
}