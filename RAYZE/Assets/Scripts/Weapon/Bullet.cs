﻿using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PlayerData playerData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(playerData.weaponDamage);
        }
        Destroy(gameObject);
    }
}
