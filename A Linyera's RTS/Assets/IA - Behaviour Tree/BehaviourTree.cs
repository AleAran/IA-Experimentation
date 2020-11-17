using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Our first baby steps towards a BT editor, the idea is to have tree where we can set our own behaviour and them save the data.
/// Why are we doing this? Well... I'm compensating for the final. Gonna escalate the heck out of this RTS by March.
/// Zerg. Rush.
/// 
/// Also, chances are the amount of sugar in my blood after drinking so much cofee is making me delirious
/// 
/// Anyway, This is the base of the BT, a simple loader. The idea is simple, we are going to create an simple editor (BTGenerator, a scriptobject), then use the it to create a tree for a GameObject. Then, on the gameObject, we load it's tree by using BTSetup.
/// This is barebones, and later on, we are going to expand the usages.
/// </summary>
public class BehaviourTree : MonoBehaviour
{
    BTNode mRoot;
    public BTGenerator mBTLoader;

    public void BTSetup<T>(T editedObject)
    {
        mBTLoader.loadTree<T>(0, out mRoot, editedObject);
    }

    public BTNode getRoot()
    {
        return mRoot;
    }
}
