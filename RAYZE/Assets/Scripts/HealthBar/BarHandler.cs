using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHandler : MonoBehaviour
{
   [SerializeField] private HealthBar healthBar;
   [SerializeField] private PlayerData playerData;
   bool changeColor = true;



    [SerializeField] DamageOverlay damageOverlay;
    bool imageVisibility = true;
    // Start is called before the first frame update


    public void HandleBar(float health)
    {
        
        if (health > .0001f)
        {
         
            healthBar.SetSize(health);

            if ((health) < 0.3f)
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
    }
    private void Update()
    {
        HandleBar(playerData.playerCurrentHealth/100);

  
    }
}
