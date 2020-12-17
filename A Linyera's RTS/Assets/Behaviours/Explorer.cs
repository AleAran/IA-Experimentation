using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Explorer, you want something, this unit should find it.
/// Emphasis on "should".
/// </summary>
public class Explorer : BaseUnit
{
    protected override void Start()
    {
        base.Start();

        // We register the states and events. 
        mFSM.setRelation((int)States.Idle, (int)Events.WakeUp, (int)States.Patrol);
        mFSM.setRelation((int)States.Patrol, (int)Events.FoundMine, (int)States.Marking);
        mFSM.setRelation((int)States.Marking, (int)Events.ReachedGoal, (int)States.Patrol);
    }

    /// <summary>
    /// This function handles what to do in each state.
    /// </summary>
    protected override void stateHandler()
    {
        base.stateHandler();

        switch (mState)
        {
            case States.Idle:
                Idle();
                break;

            case States.Patrol:
                Patrol();
                break;

            case States.Marking:
                Marking();
                break;

            default:
                Debug.LogError("ERROR! Invalid State - " + mState + " not included on on the State Handler");
                break;
        }
    }

    /// <summary>
    /// The functions below have pretty simple logic, no need to bother here.
    /// </summary>
    protected override void MineFound(GameObject mine)
    {
        if (!mine.GetComponent<Mine>().Flagged())
        {
            FieldOfView(mine);
        }
    }

    void Idle()
    {
        mTimer += Time.deltaTime;
        if (mTimer > MAXTIME)
        {
            mTimer = 0;
            mPathIsValid = false;
            mFSM.changeState((int)Events.WakeUp);
        }
    }

    private void Patrol()
    {
        if (mPathIsValid)
        {
            Moving();
        }

        if (transform.position == mGoal || !mPathIsValid)
        {
            if (mPath != null)
            {
                mPath.Clear();
            }

            mGoal = new Vector3(Random.Range(0, mGrid.getWidth()), Random.Range(0, mGrid.getHeight()), 0);
            Setpath(mGoal);
        }
    }

    void Marking()
    {
        Moving();

        if (transform.position == mTargetMine.transform.position)
        {
            mTargetMine.GetComponent<Mine>().ChangeColor();
            mTargetMine = null;

            mFSM.changeState((int)Events.ReachedGoal);
            mPathIsValid = false;
        }
    }
}
