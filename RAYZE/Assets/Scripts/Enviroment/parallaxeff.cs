using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxeff : MonoBehaviour
{

    private float lenght, startPos;
    public GameObject cam;
    public float parallax;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1- parallax));
        float dist = (cam.transform.position.x * ( parallax));
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if(temp > startPos + lenght)
        {
            startPos += lenght;
        }
        else if (temp < startPos - lenght) 
        {
            startPos -= lenght;
        }
    }
}
