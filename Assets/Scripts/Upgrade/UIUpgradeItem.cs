using BayatGames.SaveGameFree.Examples;
using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static BayatGames.SaveGameFree.Examples.ExampleSaveCustom;
using YG;

public class UIUpgradeItem : MonoBehaviour
{
    public TextMeshProUGUI TitleName;
    public TextMeshProUGUI NameProductSpeed;
    public TextMeshProUGUI NameProductDelayShoot;
    public TextMeshProUGUI NameProductDamage;
    public Button ButtonUpgradeSpeed;
    public Button ButtonUpgradeDelayShoot;
    public Button ButtonUpgradeDamage;
    public Upgrade Upgrade;
    private SOTank SOTank;
    public GameObject MaxSpeed;
    public GameObject MaxDelay;
    public GameObject MaxDamage;

    private int _index;


    // Сохранка

    public float Speed = 10;
    public float Delay = 1;
    public float Damage = 1;

    public void Init(Upgrade upgrade, ModelUpgrade model, SOTank tank, int index) // Сделать сохранку. Устанавливать дефолтные значения
    {
        _index = index;
        Upgrade = upgrade;
        SOTank = tank;
        TitleName.SetText($"{model.NameTank}");
        ButtonUpgradeSpeed.onClick.AddListener(AudioManager.Instance.ButtonSound);
        ButtonUpgradeDelayShoot.onClick.AddListener(AudioManager.Instance.ButtonSound);
        ButtonUpgradeDamage.onClick.AddListener(AudioManager.Instance.ButtonSound);
    }
    public void UpgradeSpeed()
    {
        if (Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed] <= MainUi.Instance.TotalMoney)
        {
            if (Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed < 3)
            {
                Speed += Upgrade.TotalSpeed;

                //Сохранение данных
                Upgrade.SaveData.Tanks[_index].SpeedTank = Speed;
                YandexGame.savesData.SaveData = Upgrade.SaveData;
                YandexGame.SaveProgress();

                SOTank.ModelTanks.speed = Speed;

                MainUi.Instance.TotalMoney -= Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed];
                YandexGame.savesData.Money = MainUi.Instance.TotalMoney;
                YandexGame.SaveProgress();
                Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed++;
                YandexGame.savesData.SaveData = Upgrade.SaveData;
                YandexGame.SaveProgress();
                if (Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed < 3)
                {
                    NameProductSpeed.SetText($"Скорость. Стоимость улучшения: {Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed]}");
                }
            }
            if (Upgrade.SaveData.Tanks[_index].indexUpgradeSpeed == 3)
            {
                MaxSpeed.SetActive(true);
                ButtonUpgradeSpeed.gameObject.SetActive(false);
                NameProductSpeed.SetText($"Скорость. Макс. уровень!");
            }
        }
    }
    public void UpgradeDelayShoot()
    {
        if (Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeDelay] <= MainUi.Instance.TotalMoney)
        {
            if (Upgrade.SaveData.Tanks[_index].indexUpgradeDelay < 3)
            {
                Delay -= Upgrade.TotalDelayShoot;
                Upgrade.SaveData.Tanks[_index].DelayTank = Delay;
                for (int i = 0; i < SOTank.ModelTanks.soGun.Length; i++)
                {
                    YandexGame.savesData.SaveData = Upgrade.SaveData;
                    YandexGame.SaveProgress();
                    SOTank.ModelTanks.soGun[i].modelGun.DelayShoot = Delay;
                }
                MainUi.Instance.TotalMoney -= Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeDelay];
                YandexGame.savesData.Money = MainUi.Instance.TotalMoney;
                YandexGame.SaveProgress();
                Upgrade.SaveData.Tanks[_index].indexUpgradeDelay++;
                YandexGame.savesData.SaveData = Upgrade.SaveData;
                YandexGame.SaveProgress();
                if (Upgrade.SaveData.Tanks[_index].indexUpgradeDelay < 3)
                {
                    NameProductDelayShoot.SetText($"Скорость стрельбы. Стоимость улучшения: {Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeDelay]}");
                }

            }
            if (Upgrade.SaveData.Tanks[_index].indexUpgradeDelay == 3)
            {
                MaxDelay.SetActive(true);
                ButtonUpgradeDelayShoot.gameObject.SetActive(false);
                NameProductDelayShoot.SetText($"Скорость стрельбы. Макс. уровень!");
            }
        }
    }
    public void UpgradeDamage()
    {
        if (Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeDamage] <= MainUi.Instance.TotalMoney)
        {
            if (Upgrade.SaveData.Tanks[_index].indexUpgradeDamage < 3)
            {
                Damage += Upgrade.TotalDamage;
                Upgrade.SaveData.Tanks[_index].DamageTank = Damage;
                for (int i = 0; i < SOTank.ModelTanks.soGun.Length; i++)
                {
                    YandexGame.savesData.SaveData = Upgrade.SaveData;
                    YandexGame.SaveProgress();
                    SOTank.ModelTanks.soGun[i].modelGun.soBullet.ModelBulet.Damage = Damage;
                }
                MainUi.Instance.TotalMoney -= Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeDamage];
                YandexGame.savesData.Money = MainUi.Instance.TotalMoney;
                YandexGame.SaveProgress();
                Upgrade.SaveData.Tanks[_index].indexUpgradeDamage++;
                YandexGame.savesData.SaveData = Upgrade.SaveData;
                YandexGame.SaveProgress();
                if (Upgrade.SaveData.Tanks[_index].indexUpgradeDamage < 3)
                {
                    NameProductDamage.SetText($"Урон. Стоимость улучшения: {Upgrade.SOUpgrade.ModelsUpgrade[0].PriceUpgarde[Upgrade.SaveData.Tanks[_index].indexUpgradeDamage]}");
                }

            }
            if (Upgrade.SaveData.Tanks[_index].indexUpgradeDamage == 3)
            {
                MaxDamage.SetActive(true);
                ButtonUpgradeDamage.gameObject.SetActive(false);
                NameProductDamage.SetText($"Урон. Макс. уровень!");
            }
        }
    }
}
