using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    [SerializeField]
    private float direction;
    [SerializeField]
    private float speed;
    Rigidbody2D bulletRB;
  
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();

       


        Destroy(this.gameObject, 3);
    }

    private void FixedUpdate()
    {
      if(direction < 0) 
       {
            bulletSpeed();
       }
      
      else 
       {
            bulletSpeed();
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
       


    }

    private void bulletSpeed() 
    {
            bulletRB.velocity = new Vector2(speed * (direction), 0.0f);  // Gibst du in der Konsole einen negativen Wert ein geht es nacht rechts ;)
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
