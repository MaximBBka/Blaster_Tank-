using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TankBase _tank;
    public ModelBulet Bulet;
    [SerializeField] private Rigidbody2D rb;
    public void Init(TankBase tank, ModelBulet bulet)
    {
        _tank = tank;
        Bulet = bulet;
    }

    public void Move()
    {
        rb.velocity = Vector2.up * Bulet.Speed;
    }
    private void Update()
    {
        Move();
        Remove();
    }
    public void Remove()
    {
        Bulet.TimeLife -= Time.deltaTime;
        if (Bulet.TimeLife < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<EnemyUnit>(out EnemyUnit unit))
        {
            unit.ModelEnemy.Health -= Bulet.Damage;
            Destroy(gameObject);
            //if (unit.ModelEnemy.Health < 0)
            //{
            //    Destroy(unit.gameObject);
            //}
        }
    }

}
