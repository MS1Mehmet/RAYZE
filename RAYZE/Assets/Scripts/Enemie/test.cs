using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public Transform distance1;
    public int attackX, attackY;
    public LayerMask player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] playerToDmg = Physics2D.OverlapBoxAll(distance1.position, new Vector2(attackX, attackY),0,player );
    }

    private void OnTriggerEnter2D(Collider2D collision)     // Kommt in den Bereich des Colliders; Dieser muss auf trigger sein!
    {
        if (collision.tag == "Player")
        {
            Debug.Log("WTF TRIGGER");
          



        }
    }
}
