using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject DefaultBullet;
    public Transform GunPoint;

    public virtual void Enter()
    {
        Instantiate(DefaultBullet, GunPoint.position, Quaternion.identity);
    }

    public virtual void Exit()
    {
    }
}
