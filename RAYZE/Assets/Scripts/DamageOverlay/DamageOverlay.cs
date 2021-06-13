using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageOverlay : MonoBehaviour
{
    [SerializeField] Image image;
    bool isVisible = true;
    // Start is called before the first frame update
    void Start()
    {
        image.enabled = false;
        image = GetComponent<Image>();
        var tempColor = image.color;
        tempColor.a = 0.4f;
        image.color = tempColor;

    }

 public void setVisibility(bool isImageVisible) //bool isImageVisible
    {
        if (isImageVisible)
        {
            image.enabled = true;
        }
        else 
        {
            image.enabled = false;
        }
       
    }
  



}
