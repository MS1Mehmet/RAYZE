using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code kopiert und modifiziert von Tony Morelli, Link verfügbar unter: https://www.youtube.com/channel/UCUDgC_B4-iNiYTGWlXcz5Zg

public class BulletUpDown : MonoBehaviour
{
    public int damage = 1;
    public float ySpeed ;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyBullet());
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        position.y += ySpeed;
        transform.position = position;
        
    }

    public void SetDamageValue(int damage)
    {
        this.damage = damage;
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(this.damage);
            }
            Destroy(gameObject, 0.01f);   //Wenn unser Schuss "enemy" berührt, dann wird der Schuss zerstört

        }

    }
}
