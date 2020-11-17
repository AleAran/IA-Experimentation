using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// No logic here, only enums
/// </summary>
public enum Events
{
    GoToTarget,
    ReachedGoal,
    FoundMine,
    ReturnToBase,
    EventsCount
}

public enum States
{
    Idle,
    Moving,
    Mining,
    Returning,
    StatesCount
}

public enum BtNodeType
{
    Sequencer,
    Selector,
    NodeTypeCount
}
public enum BTNodeState
{
    Done,
    Running,
    Error,
    NodeStateCount
}