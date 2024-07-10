using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Data/TypePets")]
public class SOPet : ScriptableObject
{
    public ModelPet Model;
}

[Serializable]
public struct ModelPet
{
    public ShootingPets prefab;
    public float Speed;
    public SOGun[] soGun;
}
