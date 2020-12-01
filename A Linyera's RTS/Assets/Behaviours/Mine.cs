using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (mGold > 0)
        {
            mGold -= Time.deltaTime;
            return Time.deltaTime;
        }
        else
        {
            mHasGold = false;
            GetComponent<SpriteRenderer>().color = Color.black;
            mSpawner.DecreaseMineCount();

            return 0;
        }
    }

    public bool Flagged() { return mIsFlagged; }
    public bool HasGold() { return mHasGold; }
    public bool IsBeingMined() { return mBeingMined; }
}
