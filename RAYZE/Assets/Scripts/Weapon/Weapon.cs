using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    public GameObject DefaultBullet;
    [SerializeField]
    public Transform GunPoint;
    public Player player;
    public GameObject bullet;

    public virtual void Enter()
    {
        if(player.FacingDirection == 1)
        {

            bullet = Instantiate(DefaultBullet, GunPoint.position, Quaternion.identity);
        }
    }

    public virtual void Exit()
    {
    }
}
