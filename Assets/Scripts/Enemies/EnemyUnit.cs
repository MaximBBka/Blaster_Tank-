using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : MonoBehaviour
{
    [SerializeField] private SpawnEnemies spawnEnemies;
    public ParticleSystem ParticleSystem;
    public ModelEnemy ModelEnemy;
    private Vector2 direction;
    public void Init(ModelEnemy modelEnemy)
    {
        
        ModelEnemy = modelEnemy;
    }
    //public void SetTarget(Vector2 target)
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * ModelEnemy.Speed);
    //}
    private void Start()
    {
        //direction = Vector2.down;
    }
    private void Update()
    {
        Move();
        DestroyEnemy();
    }
    protected void Move()
    {
        transform.Translate(direction * ModelEnemy.Speed * Time.deltaTime);
    }

    protected RaycastHit2D Find(Vector2 direction)
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, direction, 1f);
        return hit;
    }

    protected void FindLogic()
    {
        if (Find(transform.forward).collider.TryGetComponent<EnemyUnit>(out EnemyUnit unit))
        {
            direction = transform.forward * -1;
        }
        if (Find(transform.forward * -1).collider.TryGetComponent<EnemyUnit>(out EnemyUnit unit2))
        {
            direction = transform.forward;
        }
        if (Find(transform.right).collider.TryGetComponent<TankBase>(out TankBase tank))
        {
            direction = transform.right;
        }
        if (Find(transform.right * -1).collider.TryGetComponent<TankBase>(out TankBase tank2))
        {
            direction = transform.right * -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<TankBase>(out TankBase tank))
        {
            Destroy(tank.gameObject);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, direction);
    }
    private void DestroyEnemy()
    {
        if (ModelEnemy.Health <= 0)
        {
            ParticleSystem.Play();
            Destroy(gameObject, ParticleSystem.main.duration);
            StartCoroutine(spawnEnemies.ChildSpawn());
        }        
    }
}
