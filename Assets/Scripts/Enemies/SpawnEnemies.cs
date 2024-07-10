using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private WaveInfo[] waveInfo;
    [SerializeField] private List<EnemyUnit> enemies;
    [SerializeField] private Transform[] spawnPos;
    private WaitForSeconds wait;
    private WaitForSeconds waitPatrticle;
    private bool IsLock = false;

    private void Update()
    {

    }
    private void Start()
    {
        StartCoroutine(SpawnWave());
    }

    public IEnumerator SpawnWave()
    {
        for (int i = 0; i < waveInfo.Length; i++)
        {
            IsLock = waveInfo[i].WaitForDestroy;
            yield return new WaitWhile(() => IsLock);
            wait = new WaitForSeconds(waveInfo[i].Delay);
            yield return wait;
            for (int j = 0; j < waveInfo[i].MaxEnemies; j++)
            {
                EnemyUnit prefab = Instantiate(waveInfo[i].prefab.modelEnemy.Prefab, spawnPos[Random.Range(0, spawnPos.Length)]);
                prefab.Init(waveInfo[i].prefab.modelEnemy,this);
                enemies.Add(prefab);
            }
        }
    }
    public void TryPlayChildSpawn(EnemyUnit enemy, SOEnemy[] Modelenemies)
    {
        enemies.Remove(enemy);
        if (Modelenemies.Length > 0)
        {
            SpawnChild(enemy.transform.position, Modelenemies);
        }
    }
    private void SpawnChild(Vector2 pos, SOEnemy[] models)
    {
        for (int i = 0; i < models.Length; i++)
        {
            EnemyUnit prefab = Instantiate(models[i].modelEnemy.Prefab, pos, Quaternion.identity);
            prefab.Init(models[i].modelEnemy, this);
            enemies.Add(prefab);
        }
    }
}

[Serializable]
public struct WaveInfo
{
    public SOEnemy prefab;
    public float Delay;
    public float MaxEnemies;
    public bool WaitForDestroy;
}

