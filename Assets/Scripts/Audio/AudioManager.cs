using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource Music;
    public AudioSource Sound;
    public AudioClip Death;
    public AudioClip EnemyDeath;
    public AudioClip Button;
    public AudioClip Shoot;
    public AudioClip Coin;

    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        float musicValue = AudioManager.Instance.Music.volume;

        if (PlayerPrefs.HasKey("Music"))
        {
            musicValue = PlayerPrefs.GetFloat("Music");
        }
        PlayerPrefs.SetFloat("Music", musicValue);
        AudioManager.Instance.Music.volume = musicValue;

        float soundValue = AudioManager.Instance.Sound.volume;
        if (PlayerPrefs.HasKey("Volume"))
        {
            soundValue = PlayerPrefs.GetFloat("Volume");
        }
        PlayerPrefs.SetFloat("Volume", soundValue);
        AudioManager.Instance.Sound.volume = soundValue;
    }
    public void ButtonSound()
    {
        AudioManager.Instance.Sound.PlayOneShot(Button);
    }
}
