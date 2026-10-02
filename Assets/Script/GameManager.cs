using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject blueNexus;
    [SerializeField] private GameObject redNexus;
    [SerializeField] private GameObject player;
    [SerializeField] private UIManager uiManager;

    public GameObject minionPrefab;
    public GameObject rangeMinionPrefab;
    public GameObject cannonMinionPrefab;
    private Vector3 blueSpawnLocation = new Vector3(-50, 1, 0);
    private Vector3 redSpawnLocation = new Vector3(50, 1, 0);

    private int waveSpawned = 0;
    private float minionSpawnTime = 30;
    private float timer = 0;
    private bool isGameOver = false;

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnMinions();
            timer = minionSpawnTime;
        }


        if (blueNexus == null || player == null)
        {
            uiManager.HandleWinLose(false);
        }

        if (redNexus == null)
        {
            uiManager.HandleWinLose(true);
        }
    }

    void SpawnMinions()
    {
        GameObject minionSpawned;

        //Blue Side Minion
        if (waveSpawned % 2 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(minionPrefab, blueSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(true);
            }

            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(rangeMinionPrefab, blueSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(true);
            }
        }

        if (waveSpawned % 2 == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(minionPrefab, blueSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(true);
            }

            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(rangeMinionPrefab, blueSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(true);
            }

            minionSpawned = Instantiate(cannonMinionPrefab, blueSpawnLocation, Quaternion.identity);
            minionSpawned.GetComponent<EnemyAI>().setup(true);

        }

        //Red Side Minion
        if (waveSpawned % 2 == 0)
        {
            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(minionPrefab, redSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(false);
            }

            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(rangeMinionPrefab, redSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(false);
            }
        }

        if (waveSpawned % 2 == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(minionPrefab, redSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(false);
            }

            for (int i = 0; i < 3; i++)
            {
                minionSpawned = Instantiate(rangeMinionPrefab, redSpawnLocation, Quaternion.identity);
                minionSpawned.GetComponent<EnemyAI>().setup(false);
            }

            minionSpawned = Instantiate(cannonMinionPrefab, redSpawnLocation, Quaternion.identity);
            minionSpawned.GetComponent<EnemyAI>().setup(false);

        }

        waveSpawned++;
    }
}
