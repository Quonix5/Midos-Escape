using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header ("Stats")]
    public float ProjectileDamage;
    public float ProjectileSpeed;
    public int WhoCanIKill;

    [Header ("References")]
    public Rigidbody2D m_RigidBody2D;
    Vector3 Limit = new Vector2(13, 14);
    public AudioSource Sound;
    public GameController Controller;
    public PauseMenu Volume;

    private void Start()
    {
        if (gameObject.tag != "Flame" && gameObject.tag != "Experience")
        {
            Controller = GameObject.FindGameObjectWithTag("Spawner").GetComponent<GameController>();
            Sound.Play(0);
            Controller.ProjectileSounds += 1;
        }
        gameObject.layer = WhoCanIKill;
        PauseMenu Volume = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PauseMenu>();
        if (gameObject.tag == "Laser")
        {
            Sound.volume = Volume.Laser;
        }
        else if (gameObject.tag == "SniperBullet")
        {
            Sound.volume = Volume.SniperBullet;
        }
    }
    void Update () {
        transform.position += transform.TransformDirection(0, ProjectileSpeed * Time.deltaTime, 0);
        if (((Mathf.Abs(transform.position.x) > Mathf.Abs(Camera.main.transform.position.x) + 35 || Mathf.Abs(transform.position.y) > Mathf.Abs(Camera.main.transform.position.y) + 35) && gameObject.tag != "Flame" && gameObject.tag != "Experience"))
        {
            if (Sound.isPlaying == false)
            {
                Controller.ProjectileSounds -= 1;
                Destroy(gameObject);
            }
            if (Sound.isPlaying == true && Controller.ProjectileSounds >= 30)
            {
                Controller.ProjectileSounds -= 1;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag != "Flame")
        {
            collision.GetComponent<TakeDamage>().Damage(ProjectileDamage);
            if (Sound.isPlaying == false)
            {
                Controller.ProjectileSounds -= 1;
                Destroy(gameObject);
            }
            else
            {
                transform.position = new Vector2(200, 200);
                ProjectileSpeed = 0;
            }
        }
        if (collision.tag == "Player" && gameObject.tag == "Laser")
        {
            collision.GetComponent<GameController>().Score -= 5;
        }else if (collision.tag == "Player" && gameObject.tag == "SniperBullet")
        {
            collision.GetComponent<GameController>().Score -= 2.5f;
        }
    }
    void OnParticleCollision (GameObject other)
    {
        if (other.tag == "Player" && gameObject.tag == "Flame")
        {
            other.GetComponent<TakeDamage>().Damage(ProjectileDamage);
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<GameController>().Score -= ProjectileDamage * 0.5f;
        }else if (gameObject.tag == "Experience" && other.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("Spawner").GetComponent<GameController>().Score += 1;
            other.GetComponent<PlayerController>().Experience += 1;
        }else if (other.tag == "Player" && gameObject.tag == "HP")
        {
            other.GetComponent<TakeDamage>().Health += ProjectileDamage;
        }
    }
}
