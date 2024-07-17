using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using YG;

public class SpawnEnemies : MonoBehaviour
{
    public WaveInfo[] waveInfo;
    public List<EnemyUnit> enemies;
    [SerializeField] private Transform[] spawnPos;
    private WaitForSeconds wait;
    private bool IsLock = false;
    public bool IsLife = true;
    public SpawnHero spawnHero;
    public Coroutine wave;

    private void Update()
    {
        IsLock = !(enemies.Count == 0);
    }
    public void StartWave()
    {
        IsLife = true;
        wave = StartCoroutine(SpawnWave(MainUi.Instance.IndexWave));
    }

    public IEnumerator SpawnWave(int index) // Найти ошибку в корутине
    {
        for (int i = index; i < waveInfo.Length; i++)
        {
            IsLock = waveInfo[i].WaitForDestroy;
            yield return new WaitWhile(() => IsLock);
            wait = new WaitForSeconds(waveInfo[i].Delay);
            yield return wait;
            if (IsLife)
            {
                MainUi.Instance.NowLevel = waveInfo[i].Level;
                for (int j = 0; j < waveInfo[i].MaxEnemies; j++)
                {
                    EnemyUnit prefab = Instantiate(waveInfo[i].prefab.modelEnemy.Prefab, spawnPos[Random.Range(0, spawnPos.Length)]);
                    prefab.Init(waveInfo[i].prefab.modelEnemy, this, spawnHero);
                    enemies.Add(prefab);

                }
                if (i == waveInfo.Length - 1 && enemies.Count == 0) // Победа
                {
                    MainUi.Instance.NowLevel = waveInfo[waveInfo.Length - 1].Level;
                    MainUi.Instance.IndexWave = waveInfo.Length - 1;
                    YandexGame.SaveProgress();
                    if (MainUi.Instance.TotalMoney > YandexGame.savesData.Money)
                    {
                        YandexGame.savesData.Money = MainUi.Instance.TotalMoney;
                        YandexGame.SaveProgress();
                    }
                    if (MainUi.Instance.TotalScore > YandexGame.savesData.Score)
                    {
                        YandexGame.savesData.Score = MainUi.Instance.TotalScore;
                        YandexGame.SaveProgress();
                        MainUi.Instance.AddNewLeaderBoard();
                        MainUi.Instance.AddLeaderBoard();
                    }
                    MainUi.Instance.Win();
                    MainUi.Instance.ButtonPlay.gameObject.SetActive(false);
                    MainUi.Instance.ButtonReset.gameObject.SetActive(true);
                }
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
            prefab.Init(models[i].modelEnemy, this, spawnHero);
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
    public int Level;
}

