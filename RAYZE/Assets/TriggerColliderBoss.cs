using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerColliderBoss : MonoBehaviour
{

    Rigidbody2D rb;

    Boss1_SrctySystem Boss;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)     // Kommt in den Bereich des Colliders; Dieser muss auf trigger sein!
    {
        if (collision.tag == "Player")
        {
            Debug.Log("WTF TRIGGER");
            Boss.Istrigger();
            Debug.Log("WTF TRIGGER");
        }

        
    }


}
