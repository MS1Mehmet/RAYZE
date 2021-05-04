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
        sprite.color = new Color(1f, 1f, 0.8f, 0.55f);  // Red-Green-blue- alpha) alpha ist die tranparenz!
    }

}
