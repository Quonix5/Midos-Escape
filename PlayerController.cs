using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Stats")]
    public float Health;
    public float Speed;
    public float fireRate;
    public float Damage;
    public float BulletSpeed;
    public float WeaponLevel = 1;
    public float Experience;
    public float MaxExp = 250;

    [Header ("References")]
    public Rigidbody2D m_Rigidbody2D;
    public Projectile Bullet;
    public Transform Gun;
    public ParticleSystem Blood;
    public ParticleSystem DeathBlood;
    public AudioSource Hit;
    public PauseMenu Volume;

    [Header("Private")]
    [SerializeField] private TakeDamage TakeDamage;
    [SerializeField] private float HorizontalMove;
    [SerializeField] private float VerticalMove;
    [SerializeField] private float FireRate;
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;
    [SerializeField] private Vector3 m_Velocity = Vector3.zero;
    [SerializeField] private Image WeaponLevel1;
    [SerializeField] private Image WeaponLevel2;
    [SerializeField] private Image WeaponLevel3;

    void Start () {
        Volume.Playerdead = false;
        Volume = GameObject.FindGameObjectWithTag("Canvas").GetComponent<PauseMenu>();
        TakeDamage = gameObject.AddComponent<TakeDamage>();
        TakeDamage.Health = Health;
        DeathBlood.GetComponent<AudioSource>().playOnAwake = true;
    }
    private void FixedUpdate()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal") * Speed;
        VerticalMove = Input.GetAxisRaw("Vertical") * Speed;
        Vector3 targetVelocity = new Vector2((HorizontalMove * Time.fixedDeltaTime) * 10f, (VerticalMove * Time.fixedDeltaTime) * 10);
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);
        m_Rigidbody2D.position = new Vector2(Mathf.Clamp(m_Rigidbody2D.position.x, -15.5f, 44.5f), Mathf.Clamp(m_Rigidbody2D.position.y, -6.5f, 36f));
    }
    void Update () {
        if (Hit.volume != Volume.Hit)
        {
            Hit.volume = Volume.Hit;
        }
        if (DeathBlood.GetComponent<AudioSource>().volume != Volume.Death)
        {
            DeathBlood.GetComponent<AudioSource>().volume = Volume.Death;
        }
        if (Health > 100)
        {
            Health = 100;
        }
        if (TakeDamage.Health < Health)
        {
            Instantiate(Blood, transform.position, Quaternion.identity);
            Hit.Play();
        }
        Health = TakeDamage.Health;
        if (Health <= 0)
        {
            DeathBlood.playOnAwake = true;
            Instantiate(DeathBlood, transform.position, Quaternion.identity);
            Volume.Playerdead = true;
            Destroy(gameObject);
        }
        if (Experience >= MaxExp && WeaponLevel != 3)
        {
            Experience -= MaxExp;
            WeaponLevel += 1;
            MaxExp = MaxExp * 2f;
            Damage = Damage * 2;
        }
        if (WeaponLevel == 2)
        {
            WeaponLevel1.fillAmount -= Time.deltaTime;
            WeaponLevel2.fillAmount += Time.deltaTime;
        }
        if (WeaponLevel == 3)
        {
            Experience = 1000;
            WeaponLevel2.fillAmount -= Time.deltaTime;
            WeaponLevel3.fillAmount += Time.deltaTime;
        }
        GameObject.FindGameObjectWithTag("XP").GetComponent<TextMeshProUGUI>().SetText((Mathf.Round((Experience / MaxExp) * 100)).ToString() + "%");
        GameObject.FindGameObjectWithTag("ExpBar").GetComponent<Image>().fillAmount = Experience / MaxExp;
        GameObject.FindGameObjectWithTag("Healthbar").GetComponent<Image>().fillAmount = Health / 100;
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion Rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, Rotation, Time.deltaTime * 50);
        if (Input.GetKey(KeyCode.Mouse0) && FireRate <= 0 && WeaponLevel == 1)
        {
            FireRate = fireRate;
            gameObject.GetComponent<Animator>().SetTrigger("Shoot");
            Projectile clone = (Projectile)Instantiate(Bullet, Gun.position, Rotation);
            clone.ProjectileDamage = Damage;
            clone.ProjectileSpeed = BulletSpeed;
            clone.WhoCanIKill = 8;
        }else if (Input.GetKey(KeyCode.Mouse0) && FireRate <= 0 && WeaponLevel == 2)
        {
            FireRate = fireRate;
            gameObject.GetComponent<Animator>().SetTrigger("Shoot");
            Quaternion WeaponRotation = Quaternion.AngleAxis(angle - 87.5f, Vector3.forward);
            Quaternion WeaponRotation2 = Quaternion.AngleAxis(angle - 92.5f, Vector3.forward);
            Projectile clone = (Projectile)Instantiate(Bullet, Gun.position, WeaponRotation);
            Projectile clone2 = (Projectile)Instantiate(Bullet, Gun.position, WeaponRotation2);
            clone.ProjectileDamage = Damage;
            clone.ProjectileSpeed = BulletSpeed;
            clone.WhoCanIKill = 8;
            clone2.ProjectileDamage = Damage;
            clone2.ProjectileSpeed = BulletSpeed;
            clone2.WhoCanIKill = 8;
        }else if (Input.GetKey(KeyCode.Mouse0) && FireRate <= 0 && WeaponLevel == 3)
        {
            FireRate = fireRate;
            gameObject.GetComponent<Animator>().SetTrigger("Shoot");
            Quaternion WeaponRotation = Quaternion.AngleAxis(angle - 85, Vector3.forward);
            Quaternion WeaponRotation2 = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            Quaternion WeaponRotation3 = Quaternion.AngleAxis(angle - 95, Vector3.forward);
            Projectile clone = (Projectile)Instantiate(Bullet, Gun.position, WeaponRotation);
            Projectile clone2 = (Projectile)Instantiate(Bullet, Gun.position, WeaponRotation2);
            Projectile clone3 = (Projectile)Instantiate(Bullet, Gun.position, WeaponRotation3);
            clone.ProjectileDamage = Damage;
            clone.ProjectileSpeed = BulletSpeed;
            clone.WhoCanIKill = 8;
            clone2.ProjectileDamage = Damage;
            clone2.ProjectileSpeed = BulletSpeed;
            clone2.WhoCanIKill = 8;
            clone3.ProjectileDamage = Damage;
            clone3.ProjectileSpeed = BulletSpeed;
            clone3.WhoCanIKill = 8;
        }
        else
        {
            FireRate -= Time.deltaTime;
        }
    }
}
