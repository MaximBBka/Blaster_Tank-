using System;
using UnityEngine;
[CreateAssetMenu(menuName = "Game/Data/Tank")]
public class SOTank : ScriptableObject
{
    public ModelTank[] ModelTanks;
    public ModelTank? GetModel(TankBase tank)
    {
        for (int i = 0; i < ModelTanks.Length; i++)
        {
            if (ModelTanks[i].Tank.GetType() == tank.GetType())
            {
                return ModelTanks[i];
            }
        }
        return null;
    }
}

[Serializable]
public struct ModelTank
{
    public float speed;
    public TankBase Tank;
    public SOGun[] soGun;
}
