using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid mGrid;
    public MinersBase mMinersBase;
    public GameObject mExplorer;
    public GameObject mMiner;
    public GameObject mNewMine;
    public int mMaxMines;

    private float mSpawnTimer;
    private int mMinesAmount;
    void Start()
    {
        mSpawnTimer = 0.0f;
        mMinesAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        mSpawnTimer += Time.deltaTime;

        if (mSpawnTimer > 5)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (mMinersBase.getGold() >= 10)
                {
                    mMinersBase.WithdrawalGold(10);
                    Instantiate(mMiner, new Vector2(mMinersBase.transform.position.x, mMinersBase.transform.position.y), Quaternion.identity, gameObject.transform);
                    mSpawnTimer = 0;
                }
            }

            if (Input.GetKey(KeyCode.Backspace))
            {
                if (mMinersBase.getGold() >= 5)
                {
                    mMinersBase.WithdrawalGold(5);
                    Instantiate(mExplorer, new Vector2(mMinersBase.transform.position.x, mMinersBase.transform.position.y), Quaternion.identity, gameObject.transform);
                    mSpawnTimer = 0;
                }
            }

            while (mMinesAmount < 3) 
            {
                Vector2 minePOsition = new Vector2(Random.Range(0, mGrid.getWidth()), Random.Range(0, mGrid.getHeight()));
               
                if (!mGrid.isWall((int)minePOsition.x, (int)minePOsition.y))
                {
                    Instantiate(mNewMine, new Vector2(minePOsition.x, minePOsition.y), Quaternion.identity, gameObject.transform);
                    mMinesAmount++;

                }
            }
        }

    }

    public void DecreaseMineCount()
    {
        mMinesAmount--;
    }
    public void IncreaseMineCount()
    {
        mMinesAmount++;
    }
}
