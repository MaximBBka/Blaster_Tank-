using BayatGames.SaveGameFree.Examples;
using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BayatGames.SaveGameFree.Examples.ExampleSaveCustom;

public class Upgrade : MonoBehaviour
{
    public Shop shop;
    public SOUpgrade SOUpgrade;
    [SerializeField] private UIUpgradeItem upgradeItem;
    [SerializeField] private Transform SpawnPos;
    public float TotalSpeed;
    public float TotalDelayShoot;
    public float TotalDamage;
    public List<UIUpgradeItem> Upgrades;

    public SaveData SaveData;

    private void Awake()
    {
        Load();
        FillingUpgrade();
    }

    public void FillingUpgrade() // ���������� �� ����� ���������� � ����������� � ��������� ��� �������. // ������ ����������� ��� ������ ����????
    {
        for (int i = 0; i < SOUpgrade.ModelsUpgrade.Length; i++)
        {
            UIUpgradeItem upgrade = Instantiate(upgradeItem, SpawnPos);
            upgrade.Init(this, SOUpgrade.ModelsUpgrade[i], SOUpgrade.ModelsUpgrade[i].tank, i);
            upgrade.TitleName.SetText($"{SOUpgrade.ModelsUpgrade[i].NameTank}");
            upgrade.gameObject.SetActive(false);
            Upgrades.Add(upgrade);
        }
    }
    public void ShowItemUpgrade()
    {
        for (int i = 0; i < Upgrades.Count; i++)
        {
            if (i == shop.indexChoiseTank)
            {
                Upgrades[i].gameObject.SetActive(true);
                SOUpgrade.ModelsUpgrade[i].tank.ModelTanks.speed = SaveData.Tanks[i].SpeedTank;
                for (int j = 0; j < SOUpgrade.ModelsUpgrade[i].tank.ModelTanks.soGun.Length; j++)
                {
                    SOUpgrade.ModelsUpgrade[i].tank.ModelTanks.soGun[j].modelGun.DelayShoot = SaveData.Tanks[i].DelayTank;
                    SOUpgrade.ModelsUpgrade[i].tank.ModelTanks.soGun[j].modelGun.soBullet.ModelBulet.Damage = SaveData.Tanks[i].DamageTank;
                }
                if (SaveData.Tanks[i].indexUpgradeSpeed == 3)
                {
                    Upgrades[i].MaxSpeed.SetActive(true);
                    Upgrades[i].ButtonUpgradeSpeed.gameObject.SetActive(false);
                    Upgrades[i].NameProductSpeed.SetText($"��������. ����. �������!");
                }
                else
                {
                    Upgrades[i].NameProductSpeed.SetText($"��������. ��������� ���������: {SOUpgrade.ModelsUpgrade[i].PriceUpgarde[SaveData.Tanks[i].indexUpgradeSpeed]}");
                }
                if (SaveData.Tanks[i].indexUpgradeDelay == 3)
                {
                    Upgrades[i].MaxDelay.SetActive(true);
                    Upgrades[i].ButtonUpgradeDelayShoot.gameObject.SetActive(false);
                    Upgrades[i].NameProductDelayShoot.SetText($"�������� ��������. ����. �������!");
                }
                else
                {
                    Upgrades[i].NameProductDelayShoot.SetText($"�������� ��������. ��������� ���������: {SOUpgrade.ModelsUpgrade[i].PriceUpgarde[SaveData.Tanks[i].indexUpgradeDelay]}");
                }
                if (SaveData.Tanks[i].indexUpgradeDamage == 3)
                {
                    Upgrades[i].MaxDamage.SetActive(true);
                    Upgrades[i].ButtonUpgradeDamage.gameObject.SetActive(false);
                    Upgrades[i].NameProductDamage.SetText($"����. ����. �������!");
                }
                else
                {
                    Upgrades[i].NameProductDamage.SetText($"����. ��������� ���������: {SOUpgrade.ModelsUpgrade[i].PriceUpgarde[SaveData.Tanks[i].indexUpgradeDamage]}");
                }                          
            }
            else
            {
                Upgrades[i].gameObject.SetActive(false);
            }
        }
    }
    public void Load()
    {
        SaveData = SaveGame.Load<SaveData>("SavesTank");

        if (SaveData.Tanks == null)
        {
            SaveData.Tanks = new Tanks[11];
            for (int i = 0; i < SaveData.Tanks.Length; i++)
            {
                SaveData.Tanks[i].SpeedTank = 10f;
                SaveData.Tanks[i].DelayTank = 1f;
                SaveData.Tanks[i].DamageTank = 1f;
                SaveData.Tanks[i].indexUpgradeSpeed = 0;
                SaveData.Tanks[i].indexUpgradeDelay = 0;
                SaveData.Tanks[i].indexUpgradeDelay = 0;
                SaveData.Tanks[i].CheckBuyTank = true;
            }
        }
    }
}
