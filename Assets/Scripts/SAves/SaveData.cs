using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SaveData
{
    public Tanks[] Tanks;
}

[Serializable]
public struct Tanks
{
    public float SpeedTank;
    public float DelayTank;
    public float DamageTank;
    public int indexUpgradeSpeed;
    public int indexUpgradeDelay;
    public int indexUpgradeDamage;
    public bool CheckBuyTank;
}
