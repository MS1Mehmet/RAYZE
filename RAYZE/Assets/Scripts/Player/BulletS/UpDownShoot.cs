using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code kopiert und modifiziert von Tony Morelli, Link verfügbar unter: https://www.youtube.com/channel/UCUDgC_B4-iNiYTGWlXcz5Zg
public class UpDownShoot : MonoBehaviour
{
    /////////////////////////////////////////
    // Variablen

    Animator animator;
    public GameObject bullet;
    [SerializeField] int bulletDamage = 1;

    public float coolDown = 5.0f;
    float waitTime = 0.0f;
    bool coolDownActive = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (coolDownActive)
        {
            waitTime += Time.deltaTime;
        }

        if (waitTime > coolDown)
        {
            Debug.Log(waitTime);
            waitTime = 0.0f;
            Debug.Log(waitTime);
            Debug.Log(coolDown);
            coolDownActive = false;
        }

        if (Input.GetKeyDown(KeyCode.W) & coolDownActive == false)
            {
                GameObject go = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
                go.GetComponent<BulletUpDown>().ySpeed = +0.1f;
                go.GetComponent<BulletUpDown>().SetDamageValue(bulletDamage);
            coolDownActive = true;
                //animator.Play("Ryze_ShootUp");
            }

         if (Input.GetKeyDown(KeyCode.S))
            {
                GameObject go = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
                go.GetComponent<BulletUpDown>().ySpeed = -0.1f;
                go.GetComponent<BulletUpDown>().SetDamageValue(bulletDamage);
            }
    }
}
