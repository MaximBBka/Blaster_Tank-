using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Data/Upgrade")]
public class SOUpgrade : ScriptableObject
{
    public ModelUpgrade[] ModelsUpgrade;
}

[Serializable]
public struct ModelUpgrade
{
    public SOTank tank;
    public string NameTank;
    public int[] PriceUpgarde;
}