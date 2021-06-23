using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NebelGift : MonoBehaviour
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
        sprite.color = new Color(0.7f, 10f, 0.7f, 0.6f);  // Red-Green-blue- alpha) alpha ist die tranparenz!
    }
}
