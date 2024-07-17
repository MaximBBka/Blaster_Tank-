using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SpawnHero : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;
    [SerializeField] private Shop shop;
    [SerializeField] private Transform spawnPosTank;
    [SerializeField] private Transform spawnPosPet;
    [SerializeField] private Transform spawnPosDelivery;
    private TankBase prefabTank = null;
    public ShootingPets prefab = null;
    public PetDelivery prefabDelivery = null;
    public void Spawn()
    {
        if (shop.tank != null)
        {
            prefabTank = Instantiate(shop.tank.ModelTanks.Tank, spawnPosTank.position, Quaternion.identity);
            prefabTank.Init(shop.tank.ModelTanks);
            prefabTank.InitGun();
            camera.Follow = prefabTank.transform;
        }
        if (shop.pet != null)
        {
            prefab = Instantiate(shop.pet.Model.prefab, spawnPosPet.position, Quaternion.identity);
            prefab.Init(shop.pet.Model, prefabTank);
            prefab.InitGun();
        }
        if (shop.petDelivery != null)
        {
            prefabDelivery = Instantiate(shop.petDelivery, spawnPosDelivery.position, Quaternion.identity);
        }
    }
}
