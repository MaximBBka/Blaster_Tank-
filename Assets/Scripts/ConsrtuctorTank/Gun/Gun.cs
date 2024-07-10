using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public TankBase TankBase;
    public ShootingPets shootingPets;
    public ModelGun ModelGun;
    public WaitForSeconds shootTime;
    [SerializeField] private Transform[] SpawnPos;

    public void Init(ModelGun gun, ShootingPets shooting = null, TankBase tank = null)
    {
        TankBase = tank;
        ModelGun = gun;
        shootingPets = shooting;
        shootTime = new WaitForSeconds(ModelGun.DelayShoot);
        StartCoroutine(DelayShoot());
    }
    public void Shoot()
    {
        for (int i = 0; i < SpawnPos.Length; i++)
        {
            Bullet bullet = Instantiate(ModelGun.soBullet.ModelBulet.prefab, SpawnPos[i].position, Quaternion.identity);
            bullet.Init(TankBase, ModelGun.soBullet.ModelBulet, shootingPets);
        }
    }
    public IEnumerator DelayShoot()
    {
        while (true)
        {
            yield return shootTime;
            Shoot();
        }
    }
}
