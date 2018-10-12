using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{

    [Header("Stats")]
    public float ProjectileDamage;

    [Header("References")]
    public Rigidbody2D m_RigidBody2D;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<TakeDamage>().Damage(ProjectileDamage);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<TakeDamage>().Damage(ProjectileDamage);
    }
}