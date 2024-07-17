using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class UiShopItem : MonoBehaviour
{
    public delegate void ShopItemHelper(int index, ItemShopType type);
    public event ShopItemHelper shopItemCalback;
    public Image Image;
    public TextMeshProUGUI title;
    public TextMeshProUGUI textButton;
    public Button BuyButton;
    public Button SelectButton;
    public int indexSelect;
    public ItemShopType typeItem;
    public Shop Shop;
    public Upgrade Upgrade;

    public void Init(Upgrade upgrade, Shop shop, ModelShop modelShop, int index, ItemShopType type = ItemShopType.Buy)
    {
        Upgrade = upgrade;
        Shop = shop;
        Image.sprite = modelShop.image;
        title.SetText($"{modelShop.Name}: {modelShop.Price}");
        indexSelect = index;
        typeItem = type;
        BuyButton.onClick.AddListener(Buy);
        BuyButton.onClick.AddListener(AudioManager.Instance.ButtonSound);
        SelectButton.onClick.AddListener(Select);
        SelectButton.onClick.AddListener(AudioManager.Instance.ButtonSound);
        BuyButton.gameObject.SetActive(Upgrade.SaveData.Tanks[indexSelect].CheckBuyTank);
        if(Upgrade.SaveData.Tanks[indexSelect].CheckBuyTank == false)
        {
            SelectButton.gameObject.SetActive(true);
            typeItem = ItemShopType.Select;
        }
        

    }
    public void Buy() // Вызываю только // Логика метода всегда срабатывает
    {
        int temp = MainUi.Instance.TotalMoney;
        shopItemCalback?.Invoke(indexSelect, typeItem);
        if (MainUi.Instance.TotalMoney < temp || indexSelect == 0)
        {
            Upgrade.SaveData.Tanks[indexSelect].CheckBuyTank = false;

            //SaveGame.Save<SaveData>("SavesTank", Upgrade.SaveData);
            YandexGame.savesData.SaveData = Upgrade.SaveData;
            YandexGame.SaveProgress();

            BuyButton.gameObject.SetActive(false);
            SelectButton.gameObject.SetActive(true);
            textButton.SetText("ВЫБРАТЬ");
            typeItem = ItemShopType.Select;
        }
    }
    public void Select() // 
    {
        if (typeItem == ItemShopType.Select)
        {
            shopItemCalback?.Invoke(indexSelect, typeItem);
            textButton.SetText("ВЫБРАН");
            Selected();
        }
    }
    public void Selected() // Как сдлеать отслеживание для сбрасывания
    {
        for (int i = 0; i < Shop.Items.Count - 1; i++)
        {
            if (Shop.Items[i].typeItem == ItemShopType.Select && Shop.Items[i].indexSelect != Shop.indexChoiseTank)
            {
                Shop.Items[i].textButton.SetText("ВЫБРАТЬ");
                if (Shop.Items[i].typeItem == ItemShopType.Select && Shop.Items[i].indexSelect != Shop.indexChoisePet)
                {
                    Shop.Items[i].textButton.SetText("ВЫБРАТЬ");
                }
                else if (Shop.Items[i].typeItem == ItemShopType.Select && Shop.Items[i].indexSelect == Shop.indexChoisePet && Shop.Items[i].indexSelect != Shop.indexChoiseTank)
                {
                    Shop.Items[i].textButton.SetText("ВЫБРАН");
                }
            }
            else if (Shop.Items[i].typeItem == ItemShopType.Select && Shop.Items[i].indexSelect == Shop.indexChoiseTank)
            {
                Shop.Items[i].textButton.SetText("ВЫБРАН");
                Upgrade.ShowItemUpgrade();
            }
        }
    }
}
public enum ItemShopType
{
    Buy,
    Select
}
