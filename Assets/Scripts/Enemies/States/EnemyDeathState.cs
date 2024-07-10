using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    private EnemyController controller;
    public EnemyDeathState(StateMachine machine) : base(machine)
    {
        controller = machine as EnemyController;
    }

    public override void OnFinish()
    {

    }

    public override void OnStart()
    {
        controller.unit.particleSystem.Play();
        controller.unit.StartCoroutine(DelayParticle());
        controller.unit.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        controller.unit.spawnEnemies.TryPlayChildSpawn(controller.unit, controller.unit.ModelEnemy.enemy);
        for (int i = 0; i < controller.unit.ModelEnemy.prefabRewarded.Length; i++)
        {
            Rewarded prefab = GameObject.Instantiate(controller.unit.ModelEnemy.prefabRewarded[i], (Vector2)controller.unit.transform.position + Random.insideUnitCircle * Random.Range(1, 3), Quaternion.identity);
        }
    }

    public override void OnUpdate()
    {

    }
    public IEnumerator DelayParticle()
    {
        yield return new WaitForSeconds(controller.unit.particleSystem.main.duration);
        GameObject.Destroy(controller.unit.gameObject);
    }
}
