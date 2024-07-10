using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class MainUi : MonoBehaviour
{
    public Transform Menu;
    public Transform Shop;
    public Transform Upgrade;
    public Transform Game;
    public int TotalMoney;
    [SerializeField] private TextMeshProUGUI totalmoneyInMenu;
    [SerializeField] private TextMeshProUGUI totalmoneyInGame;

    [SerializeField] public static MainUi Instance;

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        UpdatetMoney();
    }
    private void Start()
    {
        Pause();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
    }
    public void PLay()
    {
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        Menu.gameObject.SetActive(false);
        Shop.gameObject.SetActive(false);
        Upgrade.gameObject.SetActive(false);
        Game.gameObject.SetActive(true);
        PLay();
    }
    public void Lose()
    {
        Game.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
        Pause();
    }
    public void UpdatetMoney()
    {
        totalmoneyInGame.SetText($"{TotalMoney}");
        totalmoneyInMenu.SetText($"{TotalMoney}");
    }
}
