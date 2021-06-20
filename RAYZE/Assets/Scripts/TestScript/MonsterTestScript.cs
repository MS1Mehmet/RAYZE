using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTestScript : MonoBehaviour , IDamageable
{
    int monsterHP = 30;
    public void Damage(int amount)
    {
        Debug.Log("Monster getroffen mit "+amount+" Schaden");
        monsterHP -= amount;
    }

    // Update is called once per frame
    void Update()
    {
        if (monsterHP <= 0)
        {
            Debug.Log("Monster ist Tot");
            monsterHP = 30;
        }
    }
}
