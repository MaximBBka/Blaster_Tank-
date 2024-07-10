using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Data/Tank")]
public class SOTank : ScriptableObject
{
    public ModelTank ModelTanks;
}

[Serializable]
public struct ModelTank
{
    public float speed;
    public TankBase Tank;
    public SOGun[] soGun;
}
