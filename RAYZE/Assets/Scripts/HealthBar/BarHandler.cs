using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BarHandler : MonoBehaviour
{
   [SerializeField] private HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        float health = 1f;
        bool changeColor = true;
       
        FunctionPeriodic.Create(() =>
        {
            if(health > .01f)
            {
                health -= .01f;
                healthBar.SetSize(health);

                if(health < .3f)
                {
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
