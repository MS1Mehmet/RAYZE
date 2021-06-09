using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{

    [SerializeField]
    GameObject objectToSpawn;
    private GameObject spawnedGameObject;

    [SerializeField]
    float delayBeforeSpawn;
    [SerializeField]
    float time;
    float cyleTime = 3.5f;
    [SerializeField]
    float forceX = 0f;
    [SerializeField]
    float forceY = 0f;
    [SerializeField]
    float torque = 0f;  // drehung des GameObjects
    [SerializeField]
    float destroyAfter = 3.5f;

    // Start is called before the first frame update
    void Start()
    {
        
        
       
    }

    private void FixedUpdate()
    {
        spawner();
    }

    private void spawner() 
    {
        if (delayBeforeSpawn < Time.time) 
        {
            delayBeforeSpawn = Time.time + time;
            Rigidbody2D rb2d = null;
            spawnedGameObject = (GameObject)Instantiate(objectToSpawn);
            rb2d = spawnedGameObject.GetComponent<Rigidbody2D>();
            spawnedGameObject.transform.position = transform.position + Vector3.up * 0.1f;
            rb2d.AddForce(new Vector2(forceX, forceY));
            rb2d.AddTorque(torque);
           
           

            if (forceX < 0)
            {
                spawnedGameObject.transform.localScale = new Vector3(-1, -1, -1);
            }
           
        }
    }

   
}
