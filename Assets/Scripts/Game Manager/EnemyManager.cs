using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public static EnemyManager instance;
    [SerializeField] 
    private GameObject boarPrefab, cannibalPrefab;
    public Transform[] cannibalSpawnPoint, boarSpawnPoint;
    [SerializeField]
    private int cannibalEnemyCount = 6, boarEnemyCount = 10;

    private int initialCanibalCount, initialBoarCount;
    public float waitBeforeSpawnEnemiesTime = 10f;

    private void Awake()
    {
        this.MakeInstance();
    }
    // Start is called before the first frame update
    void Start()
    {
        initialBoarCount = boarEnemyCount;
        initialCanibalCount = cannibalEnemyCount;
        this.SpawnEnemy();
        StartCoroutine("CheckSpawnEnemies");
    }

    private void SpawnEnemy()
    {
        this.SpawnBoars();
        this.SpawnCannibals();
    }

    private void SpawnCannibals()
    {
        int index = 0;
        for(int i = 0; i < cannibalEnemyCount; i++)
        {
            if(index >= cannibalSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(cannibalPrefab, cannibalSpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        cannibalEnemyCount = 0;
    }

    private void SpawnBoars()
    {
        int index = 0;
        for (int i = 0; i < boarEnemyCount; i++)
        {
            if (index >= boarSpawnPoint.Length)
            {
                index = 0;
            }
            Instantiate(boarPrefab, boarSpawnPoint[index].position, Quaternion.identity);
            index++;
        }
        boarEnemyCount = 0;
    }

    IEnumerator CheckSpawnEnemies()
    {
        yield return new WaitForSeconds(waitBeforeSpawnEnemiesTime);
        this.SpawnBoars();
        this.SpawnCannibals();
        StartCoroutine("CheckSpawnEnemies");

    }
    public void EnemyDied(bool cannibal)
    {
        Debug.Log("code run here");
        if (cannibal)
        {
            cannibalEnemyCount++;
            if(cannibalEnemyCount > initialCanibalCount)
            {
                cannibalEnemyCount = initialCanibalCount;
            }
        }
        else
        {
            boarEnemyCount++;
            if(boarEnemyCount > initialBoarCount)
            {
                boarEnemyCount = initialBoarCount;
            }
        }
    }

    public void StopSpawning()
    {
        StopCoroutine("CheckSpawnEnemies");
    }
    void MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
