using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class AdsProvider : MonoBehaviour
{
    
    public void SkipRewarded()
    {
        YandexGame.RewVideoShow(0);
    }
    public void ShowAds()
    {
        if (!MainUi.Instance.Menu.gameObject.activeSelf)
        {
            YandexGame.FullscreenShow();
        }
    }
    //public void Click()
    //{
    //    MainUI.Instance.RunGame();
    //    MainUI.Instance.addMoney += 1;
    //}
    //public void RunGame()
    //{
    //    Time.timeScale = 1f;
    //}
}
