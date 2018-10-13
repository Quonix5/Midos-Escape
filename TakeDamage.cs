using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public float Health;

    public void Damage(float damage)
    {
        Health -= damage;
    }
    public void Update()
    {
        if (Health > 100)
        {
            Health = 100;
        }
    }
}
