using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : BaseUnit
{
    public float mBagCapacity;
    private float mCollectedGold;

    private MinersBase mMinersHQ;

    protected override void Start()
    {
        base.Start();

        mCollectedGold = 0.0f;
        mMinersHQ = GameObject.FindGameObjectWithTag("MinersBase").GetComponent<MinersBase>();

        // We register the states and events. 
        mFSM.setRelation((int)States.Idle, (int)Events.WakeUp, (int)States.Patrol);
        mFSM.setRelation((int)States.Patrol, (int)Events.FoundMine, (int)States.Mining);
        mFSM.setRelation((int)States.Mining, (int)Events.ReturnToBase, (int)States.Returning);
        mFSM.setRelation((int)States.Mining, (int)Events.NothingToSee, (int)States.Patrol);
        mFSM.setRelation((int)States.Returning, (int)Events.KeepMining, (int)States.Mining);
        mFSM.setRelation((int)States.Returning, (int)Events.ReachedGoal, (int)States.Patrol);
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

            case States.Mining:
                Mining();
                break;

            case States.Returning:
                ReturnToBase();
                break;

            default:
                Debug.LogError("ERROR! Invalid State - " + mState + " not included on on the State Handler");
                break;
        }
    }

    protected override void MineFound(GameObject mine)
    {
        if (mine.GetComponent<Mine>().Flagged() && mine.GetComponent<Mine>().HasGold())
        {
            FieldOfView(mine);
        }
        else
            mFSM.changeState((int)Events.NothingToSee);
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

    void Mining()
    {
        if (transform.position == mTargetMine.transform.position)
        {
            if (mCollectedGold < mBagCapacity && mTargetMine.GetComponent<Mine>().HasGold())
            {
                mCollectedGold += mTargetMine.GetComponent<Mine>().ExtractGold();
            }
            else
            {
                Setpath(mMinersHQ.transform.position);
                mFSM.changeState((int)Events.ReturnToBase);
            }

        }
        else
            Moving();
    }

    private void ReturnToBase()
    {
        if (transform.position == mMinersHQ.transform.position)
        {
            mMinersHQ.DepositGold(mCollectedGold);
            mCollectedGold = 0;

            if (mTargetMine.GetComponent<Mine>().HasGold())
            {
                mPath.Clear();
                Setpath(mTargetMine.transform.position);
                mFSM.changeState((int)Events.KeepMining);
            }
            else
            {
                mTargetMine = null;
                mPathIsValid = false;
                mFSM.changeState((int)Events.ReachedGoal);
            }

        }
        else
            Moving();
    }

}
