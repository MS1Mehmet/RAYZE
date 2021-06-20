using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthpack : MonoBehaviour , IUpgradeables
{
    [SerializeField]
    private int HealthAmount;
    public PlayerData playerData;
    public void Upgrade()
    {
        playerData.playerCurrentHealth += HealthAmount;
        if (playerData.playerCurrentHealth > playerData.playerMaxHealth)
        {
            playerData.playerCurrentHealth = playerData.playerMaxHealth;
        }
        Destroy(gameObject);
    }

}
