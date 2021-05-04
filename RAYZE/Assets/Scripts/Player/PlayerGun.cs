using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.SceneManagement;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    // Variablen
    float damage;
    float coolDown;
    int limit;
    public GameObject defaultBullet;
    GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        DefaultGun();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Einstellung der Standartmunition
    void DefaultGun()
    {
        damage = 1;
        coolDown = 0.5f;
        bullet = defaultBullet;
    }

    //Einstellung der Upgrademunition
    public void UpgradeGun(float dmg, float cd, int lim, GameObject bul)
    {
        damage = dmg;
        coolDown = cd;
        limit = lim;
        bullet = bul;
    }
}
