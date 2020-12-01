using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Feed it gold, and new Units shall rise to do their dissapoiting work.
/// Outisde of the unit instantiation, it also handles the addition of more mines.
/// </summary>
public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    public Grid mGrid;
    public MinersBase mMinersBase;
    public GameObject mExplorer;
    public GameObject mMiner;
    public GameObject mNewMine;
    public int mMaxMines = 3;

    private float mSpawnTimer;
    private int mMinesAmount;
    void Start()
    {
        mSpawnTimer = 0.0f;
        mMinesAmount = 0;

        for (int i = mMinesAmount; i < mMaxMines; i++)
        {
            Vector2 minePOsition = new Vector2(Random.Range(0, mGrid.getWidth()), Random.Range(0, mGrid.getHeight()));

            if (!mGrid.isWall((int)minePOsition.x, (int)minePOsition.y))
            {
                Instantiate(mNewMine, new Vector2(minePOsition.x, minePOsition.y), Quaternion.identity, gameObject.transform);
                mMinesAmount++;
            }
        }
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

            for (int i = mMinesAmount; i < mMaxMines; i++)
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
