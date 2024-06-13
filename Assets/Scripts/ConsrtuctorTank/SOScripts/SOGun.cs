using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Data/Gun")]
public class SOGun : ScriptableObject
{
    public ModelGun modelGun;
}

[Serializable]
public struct ModelGun
{
    public SOBullet soBullet;
    public Gun prefab;
    public float DelayShoot;
}
