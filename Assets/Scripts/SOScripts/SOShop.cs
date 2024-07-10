using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[CreateAssetMenu(menuName = "Game/Data/Shop")]
public class SOShop : ScriptableObject
{
    public ModelShop[] modelsShop;
}

[Serializable]
public struct ModelShop
{
    public SOTank prefabTank;
    public SOPet prefabPet;
    public PetDelivery prefabDelivery;
    public int Price;
    public string Name;
    public Sprite image;
}