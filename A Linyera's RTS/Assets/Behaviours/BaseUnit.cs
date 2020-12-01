using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What did you expect, to handcraft each unit like if it where a local ice cream shop? Nah, we use base clases, and only differentiate when needed.
/// Be lazy. Be smart.
/// All units contain their own path, after all, we don't want to share.
/// </summary>
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

    /// <summary>
    /// We found a Mine... What do we do with it?
    /// </summary>
    /// <param name="mine">Target Mine</param>
    protected virtual void MineFound(GameObject mine) { }

    /// <summary>
    /// I'm seeing a mine? Or Am I staring at the wall, contemplating the cosmic horrors that haunts my very essence after drinking Fernet with Manaos?.
    /// </summary>
    /// <param name="mine">Target Mine</param>
    protected virtual void FieldOfView(GameObject mine)
    {
        float vectorAngle = Vector2.Angle(transform.up, mine.transform.position);
        if (vectorAngle < mDetectionAngle)
        {
            mTargetMine = mine;

            // pew pew pew... I cast a Ray, and pray for it to reach fair winds and don't return as null.
            RaycastHit2D hit = Physics2D.Raycast(transform.position, - Vector2.up);

            // Does it hit? IS IT WALL?!
            if (hit.collider != null && !mGrid.isWall((int)hit.point.x, (int)hit.point.y))
            {
                mPath.Clear();
                Setpath(mTargetMine.transform.position);
                mFSM.changeState((int)Events.FoundMine);

            }
        }
    }

    /// <summary>
    /// We have a goal, now, let us see if we have a Path.
    /// If we don't, try again buddy.
    /// </summary>
    /// <param name="goal"></param>
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

    /// <summary>
    /// Move Towards your goal, at your own pace, once you made your first steap, look for the next in your path.
    /// </summary>
    protected virtual void Moving()
    {

        Vector3 newPosition = Vector3.MoveTowards(transform.position, mPositionToMove, mSpeed * Time.deltaTime);
        transform.position = newPosition;

        //Googled a lot, and turns out, this is the easiest method to rotate in 2D. I hate this... Does Unity feeds on my misery?
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

    /// <summary>
    /// Standard collision affair. Nothing remarkable.
    /// </summary>
    /// <param name="col"></param>
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Mine" && mState == States.Patrol)
        {
            MineFound(col.gameObject);
        }
    }
}
