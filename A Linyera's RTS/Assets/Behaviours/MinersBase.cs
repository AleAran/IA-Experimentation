using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Home away from home, here, the gold is stored and then used to buy new units.
/// It also tells the ui to show how much money you have. Don't worry, it's gold, it won't devaluate.
/// </summary>
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
