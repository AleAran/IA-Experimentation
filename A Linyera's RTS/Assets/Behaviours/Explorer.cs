using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Skeleton Explorer Class, it uses FSM, and Pathfinding
/// </summary>
public class Explorer : MonoBehaviour
{
    StateMachine mFSM;
    States mState;

    public Grid mGrid;
    protected Stack<Vector3> mPath;
    protected Vector3 mPositionToMove;

    protected void Start()
    {
        mFSM = GetComponent<StateMachine>();
        mFSM.init((int)States.StatesCount, (int)Events.EventsCount);

        mState = (States)mFSM.getState();

        // We register the states and events. This time two, let's keept simple for this example
        mFSM.setRelation((int)States.Idle, (int)Events.FoundMine, (int)States.Moving);
        mFSM.setRelation((int)States.Moving, (int)Events.ReachedGoal, (int)States.Idle);
    }

    void Update()
    {
        stateHandler();
    }

    /// <summary>
    /// This function handles what to do in each state.
    /// </summary>
    void stateHandler()
    {
        mState = (States)mFSM.getState();

        switch (mState)
        {
            case States.Idle:
                idle();
                break;

            case States.Moving:
                moving();
                break;

            case States.Mining:
                mining();
                break;

            case States.Returning:
                returning();
                break;
        }
    }
    void idle()
    {
        //This is a bit of pseudo code, since we are working this code as template, we don't really have much real values or logic to work with.
        //This also means that there might be some weird shenaningans on terms of loop and movement, keep in mind in doing this on the flyyyyy!
        
        if (Input.GetKey(KeyCode.Space))
        {
            //Placeholder value, is the Vector3.Zero, this would be the "destination".
            mPath = mGrid.startAlgorithm(transform.position, Vector3.zero);
            mPositionToMove = mPath.Pop(); //Since the Path is a List, we pop it and assign the value to the next position to move

            mFSM.changeState((int)Events.GoToTarget);
        }
    }
    void moving()
    {
        //we start the moving loop, code is super messy, must properly loop this when we add actual stuff
        Vector3 pos = Vector3.MoveTowards(transform.position, mPositionToMove, 0.0f);
        transform.position = pos;

        //once we reach the position, we pop the next one.
        if (pos == mPositionToMove)
        {
            //Once we run out of things to pop, it means we are on the destination, so trigger the reachedGoal event
            if (mPath.Count > 0)
                mPositionToMove = mPath.Pop();
            else
                mFSM.changeState((int)Events.ReachedGoal);
        }
    }

    void mining() { }
    void returning() { }
}
