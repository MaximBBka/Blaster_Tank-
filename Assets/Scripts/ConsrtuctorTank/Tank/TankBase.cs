using System.Collections.Generic;
using UnityEngine;

public abstract class TankBase : MonoBehaviour
{
    public ModelTank ModelTank;
    public SOTank SOTank;
    public List<Gun> GunList;
    [SerializeField] protected Rigidbody2D rb;

    private void Start()
    {
        ModelTank = SOTank.GetModel(this).Value;
        for (int i = 0; i < GunList.Count; i++)
        {
            GunList[i].Init(this, ModelTank.soGun[i].modelGun);
        }
    }
    public virtual void Move()
    {
        Vector2 dictenary = new Vector2(Input.GetAxisRaw("Horizontal"), rb.velocity.y);
        rb.velocity = dictenary * ModelTank.speed;
    }
    private void FixedUpdate()
    {
        Move();
    }
}
