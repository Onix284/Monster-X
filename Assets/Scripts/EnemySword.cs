using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySword : MonoBehaviour
{

    // Start is called before the first frame update

    public PlayerMovement player;
    private float maxHealth;

    [SerializeField] private Image healthBar;

    void Start()
    {

        maxHealth = player.playerHealth;

    }

    // Update is called once per frame
    void Update()
    {
          
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            player.playerHealth -= 5;
            healthBar.fillAmount = player.playerHealth / maxHealth;
        }
        else if(player.playerHealthCollaps == true)
        {
            healthBar.fillAmount = player.playerHealth / maxHealth;
        }
    }

    
}
