using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// By Djordje

public class SoundManager : MonoBehaviour
{
    
    public AudioSource shoot;
    public AudioSource run;
    public AudioSource hit;


    void Awake()
    {
        
        
     

    }


   



   

    public void ShootSound()
    {
        shoot.Play();
    }

    public void RunSound()
    {
        run.Play();
    }

    public void HitSound()
    {
        hit.Play();
    }
}
