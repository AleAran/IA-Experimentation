using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(menuName = "test")]
public class BTGenerator : ScriptableObject
{
    public List<SerializedNode> mSerializedNode = new List<SerializedNode>();

    public int loadTree<T>(int index, out BTNode node, T instanceType)
    {
        SerializedNode serializableNode = mSerializedNode[index];

        switch ((BtNodeType)serializableNode.mNodeType)
        {
            //We have this as placeholder until we have more nodestypes created
            case BtNodeType.Sequencer:
                node = new BTNode();
                break;
            case BtNodeType.Selector:
                node = new BTNode();
                break;
            default:
                node = new BTNode();
                break;
        }

        for (int i = 0; i < serializableNode.mChildCount; i++)
        {
            BTNode childNode;
            index = loadTree<T>(++index, out childNode, instanceType);
            node.addChild(childNode);
        }

        return index;
    }
}