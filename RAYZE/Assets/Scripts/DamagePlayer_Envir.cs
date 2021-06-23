using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer_Envir : MonoBehaviour
{
    


    public Player playerScr;
    public int hitDamage;

   
    BoxCollider2D box2d;
    

    
    

    // Start is called before the first frame update
    void Start()
    {


        box2d = GetComponent<BoxCollider2D>();
        
       
    }

    // Update is called once per frame
    void Update()
    {

    }

  


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScr.DamageState.PlayerTakeDamage(hitDamage);
            playerScr.SetTakenHit(true);
            
        }
    }
}
