using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private WaveInfo[] waveInfo;
    [SerializeField] private Transform[] spawnPos;
    [SerializeField] private List<EnemyUnit> enemies;
    private WaitForSeconds wait;
    private WaitForSeconds waitPatrticle;
    private bool IsLock;

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
                EnemyUnit prefab = Instantiate(waveInfo[i].prefab.modelEnemy.Prefab, spawnPos[Random.Range(0, spawnPos.Length)].transform);
                prefab.Init(waveInfo[i].prefab.modelEnemy);
                enemies.Add(prefab);
            }
        }
    }
    public IEnumerator ChildSpawn()
    {
        if (enemies.Count > 0) // �������� ���� �� ����� � ������, �����
        {
            for (int i = enemies.Count - 1; i >= 0; i--) // ������ ���� � ����� � ��� ������, �����
            {
                if (enemies[i].ModelEnemy.Health <= 0) // ��� �������� �����, �����
                {
                    if (enemies[i].ModelEnemy.enemy.Length > 0) // ������ ���� �� � ��� ���� ����� ����������, �����
                    {
                        for (int j = 0; j < enemies[i].ModelEnemy.enemy.Length; j++) // �� ������� ���� ����� ���������� ���, �����
                        {
                            waitPatrticle = new WaitForSeconds(enemies[i].ParticleSystem.main.duration); // ������ ������ �� ������� � ��������, ����� ((( � ������� ��� ��������, ��� �� 0 ))))!
                            yield return waitPatrticle; // �������� ���� �� �������, �����
                            EnemyUnit ChildPrefab = Instantiate(enemies[i].ModelEnemy.enemy[j].modelEnemy.Prefab, enemies[i].transform.position, Quaternion.identity); // �����, �����
                            ChildPrefab.Init(enemies[i].ModelEnemy.enemy[j].modelEnemy); // � ������������ ������ ����� ������, �����
                            enemies.Add(ChildPrefab); // ��������� � ������, �����
                        }
                        enemies.Remove(enemies[i]); // ����� ����� ��� ������ ������ �� ������ ���� � �������� ��������, �����
                    }
                    else // ���� ���������� ������, �����
                    {
                        enemies.Remove(enemies[i]);// ��������� �� ������, �����
                    }
                }
            }
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

