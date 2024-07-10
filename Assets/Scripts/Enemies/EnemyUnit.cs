using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    public SpawnEnemies spawnEnemies;
    public EnemyController controller;
    public ParticleSystem particleSystem;
    public Rigidbody2D rb;
    public ModelEnemy ModelEnemy;


    public void Init(ModelEnemy model, SpawnEnemies spawn)
    {
        ModelEnemy = model;
        spawnEnemies = spawn;
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
        }
    }
}
