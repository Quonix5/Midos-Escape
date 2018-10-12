using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour {

    public float ProjectileSounds;
    public float Score;

    public GameObject[] Enemies;
    public Vector3[] spawnValues;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop;
    public int randEnemy;
    public int spawnPos;
    public int difficultylevel = 1;

    void Start() {
        StartCoroutine(waitSpawner());
    }


    void Update() {
        GameObject.FindGameObjectWithTag("Score").GetComponent<TextMeshProUGUI>().SetText(Mathf.Round(Score).ToString());
        GameObject.FindGameObjectWithTag("HP2").GetComponent<TextMeshProUGUI>().SetText(Mathf.Round(GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Health).ToString() + "%");
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
        if (Time.timeSinceLevelLoad >= 300 && Time.timeSinceLevelLoad <= 599)
        {
            difficultylevel = 2;
        } else if (Time.timeSinceLevelLoad >= 600 && Time.timeSinceLevelLoad <= 799)
        {
            difficultylevel = 3;
        } else if (Time.timeSinceLevelLoad >= 800 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WeaponLevel == 2)
        {
            difficultylevel = 4;
        } else if (Time.timeSinceLevelLoad >= 1000 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WeaponLevel == 3)
        {
            difficultylevel = 5;
        } else if (Time.timeSinceLevelLoad >= 1800 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WeaponLevel == 3)
        {
            difficultylevel = 6;
        } else if (Time.timeSinceLevelLoad >= 3600 && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().WeaponLevel == 3)
        {
            difficultylevel = 7;
        }
    }
    IEnumerator waitSpawner()
    {
        yield return new WaitForSeconds(startWait);

        while (!stop)
        {
            spawnPos = Random.Range(0, 16);

            if (difficultylevel == 1)
            {
                randEnemy = Random.Range(0, 10);
                spawnLeastWait = 4;
                spawnMostWait = 7;
            }
            else if (difficultylevel == 2)
            {
                randEnemy = Random.Range(11, 21);
                if (randEnemy >= 18 && randEnemy <= 20)
                {
                    spawnLeastWait = 6;
                    spawnMostWait = 9;
                }
                else
                {
                    spawnLeastWait = 4;
                    spawnMostWait = 7;
                }
            }else if (difficultylevel == 3)
            {
                GameObject.FindGameObjectWithTag("Spawner2").SetActive(true);
                randEnemy = Random.Range(13, 23);
                if (randEnemy >= 18 && randEnemy <= 22)
                {
                    spawnLeastWait = 5;
                    spawnMostWait = 8;
                }else
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 6;
                }
            }
            else if (difficultylevel == 4)
            {
                GameObject.FindGameObjectWithTag("Spawner2").SetActive(true);
                randEnemy = Random.Range(20, 28);
                if (randEnemy >= 20 && randEnemy <= 22)
                {
                    spawnLeastWait = 4;
                    spawnMostWait = 7;
                }
                else if (randEnemy >= 26 && randEnemy <= 27)
                {
                    spawnLeastWait = 4;
                    spawnMostWait = 7;
                }
                else
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
            }
            else if (difficultylevel == 5)
            {
                GameObject.FindGameObjectWithTag("Spawner2").SetActive(true);
                randEnemy = Random.Range(26, 35);
                if (randEnemy >= 32 && randEnemy <= 34)
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
                else if (randEnemy >= 26 && randEnemy <= 28)
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
                else
                {
                    spawnLeastWait = 2;
                    spawnMostWait = 4;
                }
            }
            else if (difficultylevel == 6)
            {
                GameObject.FindGameObjectWithTag("Spawner3").SetActive(true);
                randEnemy = Random.Range(26, 35);
                if (randEnemy >= 32 && randEnemy <= 34)
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
                else if (randEnemy >= 26 && randEnemy <= 28)
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
                else
                {
                    spawnLeastWait = 2;
                    spawnMostWait = 4;
                }
            }
            else if (difficultylevel == 7)
            {
                GameObject.FindGameObjectWithTag("Spawner4").SetActive(true);
                randEnemy = Random.Range(26, 35);
                if (randEnemy >= 32 && randEnemy <= 34)
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
                else if (randEnemy >= 26 && randEnemy <= 28)
                {
                    spawnLeastWait = 3;
                    spawnMostWait = 5;
                }
                else
                {
                    spawnLeastWait = 2;
                    spawnMostWait = 4;
                }
            }
            Vector3 spawnPosition = spawnValues[spawnPos];

            Instantiate(Enemies[randEnemy], spawnPosition + transform.TransformPoint(0, 0, 0), Quaternion.identity);

            yield return new WaitForSeconds(spawnWait);
        }
    }
}
