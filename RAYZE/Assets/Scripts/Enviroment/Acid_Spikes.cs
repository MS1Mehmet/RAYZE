using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Mehmet Budak
public class Acid_Spikes : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Defeat()
    {
        GameManager.Instance.PlayerDefeated(); // Spiel ist vorbei/Spiel wird neu geladen
        Invoke("StartDefeatAnimation", 0.0001f);
    }

    private void instaDeath()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Acid")
        {
            Defeat();
        }
    }
}
