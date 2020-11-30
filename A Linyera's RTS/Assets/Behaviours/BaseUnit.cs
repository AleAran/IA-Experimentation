using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    protected StateMachine mFSM;
    protected States mState;

    public float mDetectionAngle;
    public float mSpeed;

    protected Grid mGrid;
    protected Stack<Vector3> mPath;
    private Pathfinder mPathFinder;

    protected GameObject mTargetMine;
    protected bool mPathIsValid;

    protected Vector3 mPositionToMove;
    protected Vector3 mGoal;

    protected static int MAXTIME = 5;
    protected float mTimer;

    protected virtual void Start()
    {

        mPathFinder = new AStar();
        mGrid = GameObject.FindGameObjectWithTag("Grid").GetComponent<Grid>();
        mFSM = GetComponent<StateMachine>();
        mFSM.init((int)States.StatesCount, (int)Events.EventsCount);

        mState = (States)mFSM.getState();

        mTimer = 0;
    }

    protected virtual void Update()
    {
        stateHandler();
    }

    /// <summary>
    /// This function handles what to do in each state.
    /// </summary>
    protected virtual void stateHandler()
    {
        mState = (States)mFSM.getState();
    }

    protected virtual void MineFound(GameObject mine) { }

    protected virtual void FieldOfView(GameObject mine)
    {
        float vectorAngle = Vector2.Angle(transform.up, mine.transform.position);
        if (vectorAngle < mDetectionAngle)
        {
            mTargetMine = mine;

            mPath.Clear();
            Setpath(mTargetMine.transform.position);

            mFSM.changeState((int)Events.FoundMine);
        }
    }

    protected virtual void Setpath(Vector3 goal)
    {
        mPath = mGrid.startAlgorithm(transform.position, goal, mPathFinder);

        //Sanity check, we always need these in code and are easy to afford. Shame I can't same about therapy.
        if (mPath.Count > 0)
        {
            mPositionToMove = mPath.Pop(); //Since the Path is a List, we pop it and assign the value to the next position to move
            mPathIsValid = true;
        }
        else
            mPathIsValid = false;
    }
    protected virtual void Moving()
    {

        Vector3 newPosition = Vector3.MoveTowards(transform.position, mPositionToMove, mSpeed * Time.deltaTime);
        transform.position = newPosition;

        //Googled a lot, and turns out, this is the easiest method to rotate in 2D 
        Vector3 dir = mPositionToMove - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //once we reach the position, we pop the next one.
        if (newPosition == mPositionToMove)
        {
            if (mPath.Count > 0)
                mPositionToMove = mPath.Pop();
        }
    }
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Mine" && mState == States.Patrol)
        {
            MineFound(col.gameObject);
        }
    }
}
