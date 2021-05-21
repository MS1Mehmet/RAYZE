using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nebeleffekt : MonoBehaviour
{
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transparentSprite();
    }

    private void transparentSprite() 
    {
        sprite.color = new Color(0.8f, 0.45f, 0.75f, 0.6f);  // Red-Green-blue- alpha) alpha ist die tranparenz!
    }

}
