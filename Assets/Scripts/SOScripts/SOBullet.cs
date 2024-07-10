using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Game/Data/Bullet")]
public class SOBullet : ScriptableObject
{
    public ModelBulet ModelBulet;
}

[Serializable]
public struct ModelBulet
{
    public Bullet prefab;
    public float Speed;
    public float Damage;
    public float TimeLife;
}