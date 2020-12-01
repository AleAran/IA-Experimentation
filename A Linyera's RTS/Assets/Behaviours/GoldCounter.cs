using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldCounter : MonoBehaviour
{
    Text mTextComponent;
    string mScore;
    string mSpawn;

    bool mCountdown;
    float mTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        mSpawn = "Ready";
        mTextComponent = gameObject.GetComponent<Text>();
    }
    void Update()
    {
        if (mCountdown)
        {
            mTime -= Time.deltaTime;
            float truncated = (float)(System.Math.Truncate((double)mTime * 100.0) / 100.0);
            mSpawn = truncated.ToString();
            mTextComponent.text = "Gold: " + mScore + "   |   Spawn " + mSpawn;

            if (mTime < 0.1f)
            {
                mTime = 5;
                mCountdown = false;
                mSpawn = "Ready";
                mTextComponent.text = "Gold: " + mScore + "   |   Spawn " + mSpawn;
            }
        }
    }

    // Update is called once per frame
    public void GoldUpdate(float score)
    {
        float truncated = (float)(System.Math.Truncate((double)score * 100.0) / 100.0);
        mScore = truncated.ToString();
        mTextComponent.text = "Gold: " + mScore + "   |   Spawn " + mSpawn;
    }
    public void StartCountDown()
    {
        mCountdown = true;
    }
}
