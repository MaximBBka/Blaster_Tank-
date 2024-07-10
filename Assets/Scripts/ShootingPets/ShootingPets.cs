using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootingPets : MonoBehaviour
{
    public TankBase Tank;
    public ModelPet ModelPet;
    public SOPet SOPet;
    public List<Gun> GunList;
    public float minDistance;
    public Rigidbody2D rb;
    public void Init(ModelPet pet ,TankBase tank)
    {
        ModelPet = pet;
        Tank = tank;
    }

    //private void Start()
    //{
    //    for (int i = 0; i < GunList.Count; i++)
    //    {
    //        GunList[i].Init(ModelPet.soGun[i].modelGun, this, null);
    //    }
    //}
    public void InitGun()
    {
        for (int i = 0; i < GunList.Count; i++)
        {
            GunList[i].Init(ModelPet.soGun[i].modelGun, this, null);
        }
    }
    private void FixedUpdate()
    {
        if (Tank != null)
        {
            Move();
        }
    }


    public void Move()
    {
        Vector2 direction = (Tank.transform.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, Tank.transform.position) > minDistance)
        {
            rb.MovePosition((Vector2)transform.position + direction * ModelPet.Speed * Time.fixedDeltaTime);
        }
        else if (Vector2.Distance(transform.position, Tank.transform.position) < minDistance)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
