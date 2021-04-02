using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code kopiert und modifiziert von Tony Morelli, Link verfügbar unter: https://www.youtube.com/channel/UCUDgC_B4-iNiYTGWlXcz5Zg
public class UpDownShoot : MonoBehaviour
{
    Animator animator;
    public GameObject bullet;
    [SerializeField] int bulletDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
           GameObject go = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);
            go.GetComponent<BulletUpDown>().ySpeed = +0.1f;
            go.GetComponent<BulletUpDown>().SetDamageValue(bulletDamage);
            //animator.Play("Ryze_ShootUp");
           
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {

        }
        
    }
}
