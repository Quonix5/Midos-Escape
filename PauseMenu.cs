using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public float Laser;
    public float Flame;
    public float SniperBullet;
    public float Hit;
    public float Death;
    public float Music;
    public bool Playerdead;
    public bool Paused;
    public bool Setting;
    public bool Credit;
    public GameObject PauseMenuUI;
    public GameObject SettingsMenuUI;
    public GameObject CreditsMenuUI;
    public GameObject Background;

    private void Awake()
    {
        Pause();
        if (PlayerPrefs.HasKey("Laser"))
        {
            Laser = PlayerPrefs.GetFloat("Laser");
        }
        if (PlayerPrefs.HasKey("Flame"))
        {
            Flame = PlayerPrefs.GetFloat("Flame");
        }
        if (PlayerPrefs.HasKey("SniperBullet"))
        {
            SniperBullet = PlayerPrefs.GetFloat("SniperBullet");
        }
        if (PlayerPrefs.HasKey("Hit"))
        {
            Hit = PlayerPrefs.GetFloat("Hit");
        }
        if (PlayerPrefs.HasKey("Death"))
        {
            Death = PlayerPrefs.GetFloat("Death");
        }
        if (PlayerPrefs.HasKey("Music"))
        {
            Music = PlayerPrefs.GetFloat("Music");
        }
    }
    void Start() {
        Camera.main.GetComponent<AudioSource>().ignoreListenerPause = true;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Paused == true && Setting == false && Credit == false && Playerdead == false)
            {
                Resume();
            }
            else if (Setting == true)
            {
                SettingsClose();
            }
            else if (Credit == true)
            {
                CreditsClose();
            }
            else
            {
                Pause();
            }
        }
        if (Camera.main.GetComponent<AudioSource>().volume != Music)
        {
            Camera.main.GetComponent<AudioSource>().volume = Music;
        }
        if (Playerdead == true)
        {
            GameObject.FindGameObjectWithTag("Continue").GetComponent<Button>().interactable = false;
            Pause();
        }else if (Playerdead == false)
        {
            GameObject.FindGameObjectWithTag("Continue").GetComponent<Button>().interactable = true;
        }
    }
    public void Resume()
    {
        AudioListener.pause = false;
        Background.SetActive(false);
        PauseMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        Paused = false;
        Time.timeScale = 1;
    }
    public void Pause()
    {
        AudioListener.pause = true;
        Background.SetActive(true);
        PauseMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
        Paused = true;
        Time.timeScale = 0;
    }
    public void Settings()
    {
        Setting = true;
        PauseMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        SettingsMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void SettingsClose()
    {
        Setting = false;
        SettingsMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        PauseMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void Credits()
    {
        Credit = true;
        PauseMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        CreditsMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void CreditsClose()
    {
        Credit = false;
        CreditsMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        PauseMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void Restart()
    {
        SceneManager.LoadScene("Infinite");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void LaserSound(float laser)
    {
        Laser = laser;
        PlayerPrefs.SetFloat("Laser", laser);
    }
    public void FlameSound(float flame)
    {
        Flame = flame;
        PlayerPrefs.SetFloat("Flame", flame);
    }
    public void SniperBulletSound(float sniperbullet)
    {
        SniperBullet = sniperbullet;
        PlayerPrefs.SetFloat("SniperBullet", sniperbullet);
    }
    public void HitSound(float hit)
    {
        Hit = hit;
        PlayerPrefs.SetFloat("Hit", hit);
    }
    public void DeathSound(float death)
    {
        Death = death;
        PlayerPrefs.SetFloat("Death", death);
    }
    public void MusicSound(float music)
    {
        Music = music;
        PlayerPrefs.SetFloat("Music", music);
    }
    public void Resolution(int quality)
    {
        if (quality == 0)
        {
            QualitySettings.SetQualityLevel(5);
        }else if (quality == 1)
        {
            QualitySettings.SetQualityLevel(4);
        }else if (quality == 2)
        {
            QualitySettings.SetQualityLevel(3);
        }else if (quality == 3)
        {
            QualitySettings.SetQualityLevel(2);
        }else if (quality == 4)
        {
            QualitySettings.SetQualityLevel(1);
        }else if (quality == 5)
        {
            QualitySettings.SetQualityLevel(0);
        }
    }
    public void FullScreen (bool Fullscreen)
    {
        Screen.fullScreen = Fullscreen;
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
}
