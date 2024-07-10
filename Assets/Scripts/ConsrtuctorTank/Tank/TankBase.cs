using System.Collections.Generic;
using UnityEngine;

public abstract class TankBase : MonoBehaviour
{
    public Transform CameraTraget;
    public ModelTank ModelTank;
    public SOTank SOTank;
    public List<Gun> GunList;
    [SerializeField] protected Rigidbody2D rb;

    public void Init(ModelTank tank)
    {
        ModelTank = tank;
    }
    //private void Awake()
    //{
    //    for (int i = 0; i < GunList.Count; i++)
    //    {
    //        GunList[i].Init(ModelTank.soGun[i].modelGun,null,this);
    //    }
    //}
    public void InitGun()
    {
        for (int i = 0; i < GunList.Count; i++)
        {
            GunList[i].Init(ModelTank.soGun[i].modelGun, null, this);
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
