using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
   [SerializeField] private HealthBar healthBar;


    [SerializeField] DamageOverlay damageOverlay;
    bool imageVisibility = true;
    // Start is called before the first frame update
    void Start()
    {
        float health = 1f;
        bool changeColor = true;
       
        FunctionPeriodic.Create(() =>
        {
            if(health > .0001f)
            {
                health -= .01f;
                healthBar.SetSize(health);

                if(health < .3f)
                {
                    damageOverlay.setVisibility(imageVisibility);
                    if (changeColor)
                    {
                        healthBar.SetColor(Color.red);
                        changeColor = false;
                    }
                    else
                    {
                        healthBar.SetColor(Color.white);
                        changeColor = true;
                    }
                    
                }
            }
        },0.05f);
        
    }
   


}
