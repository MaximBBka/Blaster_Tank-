using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using YG;

public class MainUi : MonoBehaviour
{
    public Transform Menu;
    public Transform Shop;
    public Transform Upgrade;
    public Transform Game;
    public Transform WinPanel;
    public int TotalMoney;
    public int TotalScore;
    public int NowLevel;
    public int IndexWave = 0;
    [SerializeField] private TextMeshProUGUI totalmoneyInMenu;
    [SerializeField] private TextMeshProUGUI totalmoneyInGame;
    [SerializeField] private TextMeshProUGUI totalScore;
    [SerializeField] private TextMeshProUGUI levelMenu;
    [SerializeField] private TextMeshProUGUI levelGame;
    public Transform PanelShowAds;
    public TextMeshProUGUI TimerTextAds;
    public Transform ButtonNextAds;
    public LeaderboardYG leaderboardYG;
    public Transform ButtonPlay;
    public Transform ButtonReset;
    

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
        UpdateInfo();
    }
    private void Start()
    {
        Load();
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
    public void Win()
    {
        Game.gameObject.SetActive(false);
        Menu.gameObject.SetActive(true);
        WinPanel.gameObject.SetActive(true);
        Pause();
    }
    public void UpdateInfo()
    {
        //  Деньги
        totalmoneyInGame.SetText($"{TotalMoney}");
        totalmoneyInMenu.SetText($"{TotalMoney}");
        // Уровень и рекорд
        totalScore.SetText($"Рекорд: {TotalScore}");
        levelMenu.SetText($"Уровень: {NowLevel}");
        levelGame.SetText($"Уровень: {NowLevel}");
    }

    public void Load()
    {
        YandexGame.LoadProgress();
        TotalMoney = YandexGame.savesData.Money;
        TotalScore = YandexGame.savesData.Score;
        NowLevel = YandexGame.savesData.Level;
        IndexWave = YandexGame.savesData.IndexWave;
    }
    public void AddNewLeaderBoard()
    {
        YandexGame.NewLeaderboardScores("LiderBoardScore", TotalScore);
    }
    public void AddLeaderBoard()
    {
        leaderboardYG.NewScore(TotalScore);
        leaderboardYG.UpdateLB();
    }
    public void ResetLevel()
    {
        NowLevel = 1;
        IndexWave = 0;
        YandexGame.savesData.Level = NowLevel;
        YandexGame.savesData.IndexWave = IndexWave;
        YandexGame.SaveProgress();
    }
}
