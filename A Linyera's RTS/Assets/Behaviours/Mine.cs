using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What wondorous trasures will this mine yield? Gold.
/// Nothing else. Because mines are dull.
/// Unless those mines are on Middle Earth, not only they would be dull, but also lethal.
/// 
/// This class has several flags to prevent being aquired as target when in use, already marked or empty.
/// It also notifies the spawner to generate more mine when this one goes empty.
/// </summary>
/// 
public class Mine : MonoBehaviour
{
    public float mGold;

    private bool mIsFlagged;
    private bool mHasGold;
    private bool mBeingMined;

    private Spawner mSpawner;

    void Start()
    {
        mSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();

        mIsFlagged = false;
        mHasGold = true;
        mBeingMined = false;
    }

    public void ChangeColor()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
        mIsFlagged = true;
    }
    public void BeingMined(bool beingMined) { mBeingMined = beingMined; }

    public float ExtractGold()
    {
        mGold -= Time.deltaTime;
        return Time.deltaTime;
    }
    public void CheckFunds()
    {
        if (mGold > 0 == false)
        {
            mHasGold = false;
            GetComponent<SpriteRenderer>().color = Color.black;
            mSpawner.DecreaseMineCount();
        }

    }
    public bool Flagged() { return mIsFlagged; }
    public bool HasGold() { return mHasGold; }
    public bool IsBeingMined() { return mBeingMined; }
}
