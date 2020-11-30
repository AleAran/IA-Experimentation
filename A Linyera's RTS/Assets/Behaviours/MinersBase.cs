﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float mTotalGold = 0;
    public GoldCounter mText;

    public void DepositGold(float gold)
    {
        mTotalGold += gold;
        mText.GoldUpdate(mTotalGold);
    }
    
    public void WithdrawalGold(float gold)
    {
        mTotalGold -= gold;
        mText.GoldUpdate(mTotalGold);
        mText.StartCountDown();
    }

    public float getGold()
    {
        return mTotalGold;
    }
}
