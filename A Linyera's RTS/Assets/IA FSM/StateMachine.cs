using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the State Machine, it's only behaviour, is to register, compare events and change states. This class MUST NOT contain any behaviour.
/// </summary>
public class StateMachine : MonoBehaviour
{
    public int[,] SMGrid;
    int mCurrentState = 0;

    public void init(int statesCount, int eventsCount)
    {
        SMGrid = new int[statesCount, eventsCount];

        for (int x = 0; x < statesCount; x++)
        {
            for (int y = 0; y < eventsCount; y++)
                SMGrid[x, y] = -1;
        }
    }

    // Register State and Event
    public void setRelation(int initialState, int currentEvent, int newState)
    {
        SMGrid[initialState, currentEvent] = newState;
    }

    // Change state.
    public void changeState(int eventToCheck)
    {
        int eventToSet = SMGrid[mCurrentState, eventToCheck];

        if (eventToSet != -1) 
        {
            mCurrentState = eventToSet;
        }
    }

    public int getState()
    {
        return mCurrentState;
    }

}
