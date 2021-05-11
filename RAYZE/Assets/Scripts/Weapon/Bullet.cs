using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 15f;

    public Rigidbody2D rb;

    public Bullet()
    {
    }

    public void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        Destroy(gameObject);
    }
    public void ShootRight()
    {
        rb.velocity = Vector2.right * speed;
    }
    public void ShootLeft()
    {
        rb.velocity = Vector2.left * speed;
    }
}
