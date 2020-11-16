using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// No logic here, only enums
/// </summary>
public enum Events
{
    Move,
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