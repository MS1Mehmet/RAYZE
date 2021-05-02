using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject portal;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player") 
        {
          
            
            StartCoroutine(Teleport());
            
        }
    }

    
    IEnumerator Teleport() 
    {
        
      yield return new WaitForSeconds(0.5f);
      player.transform.position = new Vector2(portal.transform.position.x, portal.transform.position.y);
    
    }

}
// if (Input.GetKeyDown(KeyCode.Q))