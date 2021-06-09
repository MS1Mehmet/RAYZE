using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField]
    float red;
    [SerializeField]
    float green;
    [SerializeField]
    float blue;
    [SerializeField]
    float transparenz;
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
        sprite.color = new Color(red, green, blue, transparenz);  // Red-Green-blue- alpha) alpha ist die tranparenz!
    }
}
