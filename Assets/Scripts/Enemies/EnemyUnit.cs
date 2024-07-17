using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class EnemyUnit : MonoBehaviour
{
    public SpawnEnemies spawnEnemies;
    public SpawnHero spawnHero;
    public EnemyController controller;
    public ParticleSystem particleSystem;
    public Rigidbody2D rb;
    public ModelEnemy ModelEnemy;


    public void Init(ModelEnemy model, SpawnEnemies spawn, SpawnHero hero)
    {
        ModelEnemy = model;
        spawnEnemies = spawn;
        spawnHero = hero;
    }
    private void Start()
    {
        controller = new EnemyController(this);
        controller.Switch(new EnemyIdleState(controller));
    }

    private void Update()
    {
        if (controller.unit.ModelEnemy.Health <= 0 && !controller.Current.GetType().Equals(typeof(EnemyDeathState)))
        {
            controller.Switch(new EnemyDeathState(controller));
        }
        controller?.OnUpdate();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector3 direction = transform.TransformDirection((Vector2)transform.right * (rb.velocity.x < 0 ? 1 : -1) * 1f);
        Gizmos.DrawRay(transform.position, direction);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TankBase>(out TankBase tank))
        {
            Destroy(tank.gameObject);
            if(spawnHero.prefab != null)
            {
                Destroy(spawnHero.prefab.gameObject);
            }
            if (spawnHero.prefabDelivery != null)
            {
                Destroy(spawnHero.prefabDelivery.gameObject);
            }
            AudioManager.Instance.Sound.PlayOneShot(AudioManager.Instance.Death);
            StopCoroutine(spawnEnemies.wave);
            spawnEnemies.wave = null;
            spawnEnemies.IsLife = false;
            for (int i = spawnEnemies.enemies.Count - 1; i >= 0; i--)
            {
                Destroy(spawnEnemies.enemies[i].gameObject);
                spawnEnemies.enemies.Remove(spawnEnemies.enemies[i]);
            }
            if (spawnEnemies.enemies.Count == 0)
            {
                //Invoke("MainUi.Instance.Lose", 2f);
                MainUi.Instance.Lose();
            }
            
            if (YandexGame.savesData.Money < MainUi.Instance.TotalMoney)
            {
                YandexGame.savesData.Money = MainUi.Instance.TotalMoney;
                YandexGame.SaveProgress();
            }
            if (YandexGame.savesData.Level < MainUi.Instance.NowLevel)
            {
                YandexGame.savesData.Level = MainUi.Instance.NowLevel;
                YandexGame.SaveProgress();
            }
            for (int i = 0; i < spawnEnemies.waveInfo.Length; i++)
            {
                if (MainUi.Instance.NowLevel == spawnEnemies.waveInfo[i].Level)
                {
                    MainUi.Instance.IndexWave = i;
                    YandexGame.savesData.IndexWave = MainUi.Instance.IndexWave;
                    YandexGame.SaveProgress();
                    if (YandexGame.savesData.Score < MainUi.Instance.TotalScore)
                    {
                        YandexGame.savesData.Score = MainUi.Instance.TotalScore;
                        YandexGame.SaveProgress();
                        MainUi.Instance.AddNewLeaderBoard();
                        MainUi.Instance.AddLeaderBoard();
                    }
                    return;
                }
            }
        }
        if (collision.TryGetComponent<Zone>(out Zone zone))
        {
            Destroy(this.gameObject);
            spawnEnemies.enemies.Remove(this);
        }
    }
}
