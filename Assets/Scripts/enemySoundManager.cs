using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySoundManager : MonoBehaviour
{
    public AudioSource enemySrc;
    public AudioClip attack, run, death;
    void Start()
    {
        enemySrc = GetComponent<AudioSource>();
    }

    public void enemyAttackSound()
    {
        enemySrc.clip = attack;
        enemySrc.Play();
    }

    public void enemyRunSound()
    {
        enemySrc.clip = run;
        enemySrc.Play();
    }

    public void enemyDeathSound()
    {
        enemySrc.clip = death;
        enemySrc.Play();
    }
}
