using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
          



        }
    }
}
