using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource jump;
    public AudioSource shoot;
    public AudioSource run;
    public AudioSource hit;
    public AudioSource death;
    // Start is called before the first frame update
    public void JumpSound()
    {
        jump.Play();
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
    public void DeathSound()
    {
        death.Play();
    }
}
