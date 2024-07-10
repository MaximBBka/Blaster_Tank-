using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public SOShop shop;
    public SOTank tank;
    public SOPet pet;
    public PetDelivery petDelivery;
    public int indexChoiseTank = 0;
    public int indexChoisePet = -1;
    public UiShopItem UiShopItem;
    public Transform Content;
    public List<UiShopItem> Items;
    public Upgrade Upgrade;
    public SaveData SaveData;
    public void Start()
    {
        Filling();
        if (tank == null && indexChoiseTank < 1)
        {
            tank = shop.modelsShop[0].prefabTank;
        }
    }
    public void Filling()
    {
        for (int i = 0; i < shop.modelsShop.Length; i++)
        {
            UiShopItem item = Instantiate(UiShopItem, Content);
            item.Init(Upgrade, this, shop.modelsShop[i], i);
            item.shopItemCalback += ShopItemCalback;
            //item.typeItem = ItemShopType.Buy;
            //if (SaveData.Tanks != null && i < 8)
            //{
            //    item.BuyButton.gameObject.SetActive(SaveData.Tanks[i].CheckBuyTank);
            //}
            //item.SelectButton.gameObject.SetActive(false);
            Items.Add(item);
        }
    }
    public void ShopItemCalback(int index, ItemShopType item) // Проверки на деньги, проверка на кнопку менять ее
    {
        if (item == ItemShopType.Buy)
        {
            if (MainUi.Instance.TotalMoney >= shop.modelsShop[index].Price)
            {
                MainUi.Instance.TotalMoney -= shop.modelsShop[index].Price;
            }
        }
        if (item == ItemShopType.Select)
        {
            if (index < 7)
            {
                tank = shop.modelsShop[index].prefabTank;
                indexChoiseTank = index;
            }
            if (index >= 7 && index != shop.modelsShop.Length - 1)
            {
                pet = shop.modelsShop[index].prefabPet;
                indexChoisePet = index;
            }
            if (index == shop.modelsShop.Length - 1)
            {
                petDelivery = shop.modelsShop[index].prefabDelivery;
            }
        }
    }
}
