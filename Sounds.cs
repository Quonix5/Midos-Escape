using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Sounds : MonoBehaviour {
    

    public void Awake ()
    {
		if (tag == "LaserSound")
        {
            if (PlayerPrefs.HasKey("Laser") && GetComponent<Slider>().value != PlayerPrefs.GetFloat("Laser"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("Laser");
            }
        }
        if (tag == "MusicSound")
        {
            if (PlayerPrefs.HasKey("Music") && GetComponent<Slider>().value != PlayerPrefs.GetFloat("Music"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("Music");
            }
        }
        if (tag == "DeathSound")
        {
            if (PlayerPrefs.HasKey("Death") && GetComponent<Slider>().value != PlayerPrefs.GetFloat("Death"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("Death");
            }
        }
        if (tag == "HitSound")
        {
            if (PlayerPrefs.HasKey("Hit") && GetComponent<Slider>().value != PlayerPrefs.GetFloat("Hit"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("Hit");
            }
        }
        if (tag == "FlameSound")
        {
            if (PlayerPrefs.HasKey("Flame") && GetComponent<Slider>().value != PlayerPrefs.GetFloat("Flame"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("Flame");
            }
        }
        if (tag == "SniperSound")
        {
            if (PlayerPrefs.HasKey("SniperBullet") && GetComponent<Slider>().value != PlayerPrefs.GetFloat("SniperBullet"))
            {
                GetComponent<Slider>().value = PlayerPrefs.GetFloat("SniperBullet");
            }
        }
    }
}
