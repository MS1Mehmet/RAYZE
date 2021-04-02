using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Code kopiert und modifiziert von Tony Bhimani, Link verfügbar unter: https://www.youtube.com/user/TonyBhimani
public class UIHealthBar : MonoBehaviour
{

    public Image mask;
    float originaSize;

    public static UIHealthBar instance { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        originaSize = mask.rectTransform.rect.height;
    }

    public void SetValue(float value)
    {
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, originaSize * value);
    }
}
