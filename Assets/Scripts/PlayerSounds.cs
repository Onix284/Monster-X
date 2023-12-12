using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{

    public AudioSource audiosrc;
    public AudioClip attack, health, finish, death;
    void Start()
    {
        audiosrc = GetComponent<AudioSource>();
    }


    public void AttackSound()
    {
        audiosrc.clip = attack;
        audiosrc.Play();
    }

    public void HealthSound()
    {
        audiosrc.clip = health;
        audiosrc.Play();
    }

    public void DeathSound()
    {
        audiosrc.clip = death;
        audiosrc.Play();
    }
    public void FinishSound()
    {
        audiosrc.clip = finish;
        audiosrc.Play();
    }
}

