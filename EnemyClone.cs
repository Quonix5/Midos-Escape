using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class EnemyClone : MonoBehaviour {

    [Header("Stats")]
    public float Health;
    public float Speed;
    public float FireRate;
    public float Damage;
    public float BulletSpeed;
    public float TooClose;
    public float TooFar;
    public float CanShootDistance;
    public bool CanAttack;

    [Header("References")]
    public Rigidbody2D m_Rigidbody2D;
    public Projectile Bullet;
    public Transform Gun;
    public Transform Player;
    public ParticleSystem Blood;
    public ParticleSystem DeathBlood;
    public AudioSource Hit;
    public ParticleSystem Experience;
    public PauseMenu Volume;
    public ParticleSystem HP;

    [Header("Private")]
    [SerializeField] private TakeDamage TakeDamage;
    [SerializeField] private float fireRate;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private GameController Controller;

    void Start () {
        Volume = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PauseMenu>();
        TakeDamage = gameObject.AddComponent<TakeDamage>();
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        TakeDamage.Health = Health;
        DeathBlood.GetComponent<AudioSource>().playOnAwake = true;
        Controller = GameObject.FindGameObjectWithTag("Spawner").GetComponent<GameController>();
        if (tag == "Pyro" && Player.GetComponent<PlayerController>().WeaponLevel == 1)
        {
            Speed = 6;
        }else if (tag == "Pyro" && Player.GetComponent<PlayerController>().WeaponLevel == 2)
        {
            Speed = 9;
        }else if (tag == "Pyro" && Player.GetComponent<PlayerController>().WeaponLevel == 3)
        {
            Speed = 12;
        }
    }


    void Update()
    {
        if (Hit.volume != Volume.Hit)
        {
            Hit.volume = Volume.Hit;
        }
        if (DeathBlood.GetComponent<AudioSource>().volume != Volume.Death)
        {
            DeathBlood.GetComponent<AudioSource>().volume = Volume.Death;
        }
        if (TakeDamage.Health < Health)
        {
            Instantiate(Blood, transform.position, Quaternion.identity);
            Hit.Play();
        }
        Health = TakeDamage.Health;
        if (Health <= 0)
        {
            if (tag == "Clonw")
            {
                Controller.Score += 3;
            }else if (tag == "Assault")
            {
                Controller.Score += 5;
            }
            else if (tag == "Pyro")
            {
                Controller.Score += 7;
            }else if (tag == "Sniper")
            {
                Controller.Score += 10;
            }
            HP.playOnAwake = true;
            Instantiate(HP, transform.position, Quaternion.identity);
            DeathBlood.playOnAwake = true;
            Instantiate(DeathBlood, transform.position, Quaternion.identity);
            Experience.playOnAwake = true;
            Instantiate(Experience, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Vector2 direction = Player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion Rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Rotation;
        if (CanAttack == true || tag == "Pyro")
        {
            if (Vector2.Distance(transform.position, Player.position) < TooClose)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, -Speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, Player.position) > TooFar)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, Speed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, Player.position) > TooClose && Vector2.Distance(transform.position, Player.position) < TooFar)
            {
                transform.position = transform.position;
            }
            if (fireRate <= 0 && Vector2.Distance(transform.position, Player.position) < CanShootDistance)
            {
                if (gameObject.tag == "Pyro")
                {
                    ParticleSystem Flame = gameObject.GetComponentInChildren<ParticleSystem>();
                    ParticleSystem.EmissionModule Emission = Flame.emission;
                    Emission.rateOverTime = 200f;
                    Flame.startSize = 1f;
                    Flame.startLifetime = 0.7f;
                    Flame.startSpeed = 5f;
                    if (Bullet.Sound.isPlaying == false)
                    {
                        Bullet.Sound.Play(0);
                    }
                    else if (Bullet.Sound.isPlaying == true && Bullet.Sound.volume < Volume.Flame)
                    {
                        Bullet.Sound.volume += Mathf.Lerp(Bullet.Sound.volume, Volume.Flame, Time.deltaTime);
                    }
                    Bullet.ProjectileDamage = Damage;
                    Bullet.WhoCanIKill = 13;
                }
                else
                {
                    Quaternion WeaponRotation = Quaternion.AngleAxis(angle - Random.Range(85, 95), Vector3.forward);
                    gameObject.GetComponent<Animator>().SetTrigger("Shoot");
                    Projectile clone = Instantiate(Bullet, Gun.position, WeaponRotation);
                    clone.ProjectileSpeed = BulletSpeed;
                    clone.ProjectileDamage = Damage;
                    clone.WhoCanIKill = 12;
                    fireRate = FireRate;
                }
            }
            else
            {
                fireRate -= Time.deltaTime;
            }
            if (gameObject.tag == "Pyro" && Vector2.Distance(transform.position, Player.position) > CanShootDistance)
            {
                ParticleSystem Flame = gameObject.GetComponentInChildren<ParticleSystem>();
                ParticleSystem.EmissionModule Emission = Flame.emission;
                if (Flame.isPlaying == false)
                {
                    Flame.Play(true);
                }
                Emission.rateOverTime = 100f;
                Flame.startSize = 0.7f;
                Flame.startLifetime = 0.35f;
                Flame.startSpeed = -15f;
                if (Bullet.Sound.volume > 0)
                {
                    Bullet.Sound.volume -= Mathf.Lerp(Bullet.Sound.volume, 0, Time.deltaTime);
                }
                else if (Bullet.Sound.volume <= 0)
                {
                    Bullet.Sound.Stop();
                }
            }
        }else if (CanAttack == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0), Speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Arena")
        {
            CanAttack = true;
        }
    }
}
