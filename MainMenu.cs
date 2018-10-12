using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    public float Laser;
    public float Flame;
    public float SniperBullet;
    public float Hit;
    public float Death;
    public float Music;
    public bool Menus;
    public bool Setting;
    public bool Credit;
    public GameObject MenuUI;
    public GameObject SettingsMenuUI;
    public GameObject CreditsMenuUI;
    private void Awake()
    {
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
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Setting == true)
            {
                SettingsClose();
            }
            else if (Credit == true)
            {
                CreditsClose();
            }
        }
        if (Camera.main.GetComponent<AudioSource>().volume != Music)
        {
            Camera.main.GetComponent<AudioSource>().volume = Music;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Infinite()
    {
        SceneManager.LoadScene("Infinite");
    }
    public void Credits()
    {
        Credit = true;
        MenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        CreditsMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void CreditsClose()
    {
        Credit = false;
        CreditsMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        MenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void Settings()
    {
        Setting = true;
        MenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        SettingsMenuUI.GetComponent<Animator>().SetTrigger("GoDown");
    }
    public void SettingsClose()
    {
        Setting = false;
        SettingsMenuUI.GetComponent<Animator>().SetTrigger("GoUp");
        MenuUI.GetComponent<Animator>().SetTrigger("GoDown");
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
        }
        else if (quality == 1)
        {
            QualitySettings.SetQualityLevel(4);
        }
        else if (quality == 2)
        {
            QualitySettings.SetQualityLevel(3);
        }
        else if (quality == 3)
        {
            QualitySettings.SetQualityLevel(2);
        }
        else if (quality == 4)
        {
            QualitySettings.SetQualityLevel(1);
        }
        else if (quality == 5)
        {
            QualitySettings.SetQualityLevel(0);
        }
    }
    public void FullScreen(bool Fullscreen)
    {
        Screen.fullScreen = Fullscreen;
    }
}
