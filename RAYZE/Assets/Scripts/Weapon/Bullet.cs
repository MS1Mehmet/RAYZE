using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private PlayerData playerData;
    [SerializeField]
    private Animator Anim;

    public void Start()
    {
        Anim.SetBool("Start", true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(playerData.weaponDamage);
        }
        Destroy(gameObject);
    }

    private void BulletAnimationsFinish()
    {
        Anim.SetBool("Start", false);
        Anim.SetBool("Idle", true);
    }
}
