using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSword : MonoBehaviour
{

    public AudioSource swordCollisionSound;

    private float maxHealth = 75f;
    void Start()
    {
        swordCollisionSound = GetComponent<AudioSource>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetMouseButton(0))
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<enemyMovement>().enemyHealth -= 20;
                Image[] images = other.gameObject.GetComponentsInChildren<Image>();

                images[1].fillAmount = other.gameObject.GetComponent<enemyMovement>().enemyHealth / maxHealth;
                swordCollisionSound.Play();
            }
            
        }

    }
}
