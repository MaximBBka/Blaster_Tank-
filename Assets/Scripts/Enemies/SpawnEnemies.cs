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
        if (enemies.Count > 0) // œ–Œ¬≈–ﬂﬁ ≈—“‹ À» ¬–¿√» ¬ —œ»— ≈, ¿Õ“ŒÕ
        {
            for (int i = enemies.Count - 1; i >= 0; i--) // —Œ«ƒ¿ﬁ ÷» À —  ŒÕ÷¿ ¬ ›“Œ —œ»— ≈, ¿Õ“ŒÕ
            {
                if (enemies[i].ModelEnemy.Health <= 0) // »Ÿ” Ã≈–“¬Œ√Œ ¬–¿√¿, ¿Õ“ŒÕ
                {
                    if (enemies[i].ModelEnemy.enemy.Length > 0) // —ÃŒ“–ﬁ ≈—“‹ À» ¬ Õ≈Ã  Œ√Œ ÃŒ∆ÕŒ «¿—œ¿¬Õ»“‹, ¿Õ“ŒÕ
                    {
                        for (int j = 0; j < enemies[i].ModelEnemy.enemy.Length; j++) // œŒ Ã¿——»¬”  Œ√Œ ÃŒ∆ÕŒ «¿—œ¿¬Õ»“‹ »ƒ”, ¿Õ“ŒÕ
                        {
                            waitPatrticle = new WaitForSeconds(enemies[i].ParticleSystem.main.duration); // —“¿¬Àﬁ “¿…Ã≈– œŒ ¬–≈Ã≈Õ» — œ¿–“» À¿, ¿Õ“ŒÕ ((( ﬂ ƒ≈ﬁ¿∆»À ŒÕŒ —“¿¬»“—ﬂ, “¿Ã Õ≈ 0 ))))!
                            yield return waitPatrticle; // «¿ƒ≈–∆ ¿  Œƒ¿ œŒ “¿…Ã≈–”, ¿Õ“ŒÕ
                            EnemyUnit ChildPrefab = Instantiate(enemies[i].ModelEnemy.enemy[j].modelEnemy.Prefab, enemies[i].transform.position, Quaternion.identity); // —œ¿¬Õ, ¿Õ“ŒÕ
                            ChildPrefab.Init(enemies[i].ModelEnemy.enemy[j].modelEnemy); // ¬ «¿—œ¿¬Õ≈ÕÕ€… œ–≈‘¿¡ œ»’¿ﬁ ÃŒƒ≈À‹, ¿Õ“ŒÕ
                            enemies.Add(ChildPrefab); // «¿ »ƒ€¬¿ﬁ ¬ —œ»—Œ , ¿Õ“ŒÕ
                        }
                        enemies.Remove(enemies[i]); // œŒ—À≈ ÷» À¿ ƒÀﬂ —œ¿¬Õ¿ ”ƒ¿Àﬂﬁ »« —œ»— ¿ “¿Õ  —  Œ“Œ–Œ√Œ —œ¿¬Õ»À», ¿Õ“ŒÕ
                    }
                    else // ≈—À» «¿—œ¿¬Õ»“‹ Õ≈ Œ√Œ, ¿Õ“ŒÕ
                    {
                        enemies.Remove(enemies[i]);// ”«ƒ¿À≈Õ»≈ »« —œ»— ¿, ¿Õ“ŒÕ
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

