using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersBase : MonoBehaviour
{
    // Start is called before the first frame update
    public float mTotalGold = 0;

    public void DepositGold(float gold)
    {
        mTotalGold += gold;
    }

    public float getGold()
    {
        return mTotalGold;
    }
}
