using UnityEngine;

public class Bullet : MonoBehaviour
{
    public TankBase _tank;
    public ShootingPets pets;
    public ModelBulet Bulet;
    [SerializeField] private Rigidbody2D rb;
    public void Init(TankBase tank, ModelBulet bulet, ShootingPets shooting)
    {
        _tank = tank;
        Bulet = bulet;
        pets = shooting;
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
        }
    }
}