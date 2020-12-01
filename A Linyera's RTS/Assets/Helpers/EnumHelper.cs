using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// No logic here, only enums
/// </summary>
public enum Events
{
    WakeUp,
    GoToTarget,
    ReachedGoal,
    FoundMine,
    ReturnToBase,
    KeepMining,
    EventsCount
}

public enum States
{
    Idle,
    Patrol,
    Marking,
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