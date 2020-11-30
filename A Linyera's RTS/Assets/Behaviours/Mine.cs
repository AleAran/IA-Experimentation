using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    // Start is called before the first frame update
    private bool mIsFlagged;
    private bool mHasGold;
    public float mGold;

    private Spawner mSpawner;

    void Start()
    {
        mIsFlagged = false;
        mHasGold = true;
        mSpawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<Spawner>();
    }

    public void ChangeColor() {
        GetComponent<SpriteRenderer>().color = Color.cyan;
        mIsFlagged = true;
    }

    public float ExtractGold()
    {
        mGold -= Time.deltaTime;
        if (mGold > 0)
        {
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
}
