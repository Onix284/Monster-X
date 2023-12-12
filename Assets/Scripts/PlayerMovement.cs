using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    public Rigidbody rb;
    Animator animator;
    [SerializeField] private float turnSpeed = 5f;
    private bool playerIsOnGround = true;

    public float playerHealth = 100f;

    public bool playerIsDead;

    public bool playerHealthCollaps;
    [SerializeField] private Image PlayerHealthBar;
    private float maxHealth;

    public PlayerSounds playerSounds;
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        maxHealth = playerHealth;


    }

    // Update is called once per frame
    [System.Obsolete]
    private void Update()
    {
        if (playerHealth > 0)
        {
            playerIsDead = false;
            //Movement
            float verticalInput = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
            transform.Translate(0, 0, verticalInput);

            //Rotation
            if (movementSpeed != 0)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    transform.RotateAround(Vector3.up, -turnSpeed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.RotateAround(Vector3.up, turnSpeed * Time.deltaTime);
                }
            }


            //Jump Mechanism
            if (Input.GetButtonDown("Jump") && playerIsOnGround == true)
            {
                rb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
                playerIsOnGround = false;
            }


            //Animation 
          
                AnimationCharacter();
                
            

            //Scale
            if (Input.GetKeyUp(KeyCode.F))
            {
                transform.localScale += new Vector3(1f, 1f, 1f);
            }

            if (Input.GetKeyUp(KeyCode.G))
            {
                transform.localScale += new Vector3(-0.5f, -0.5f, -0.5f);
            }

        }
        else if(playerHealth <= 0 && playerIsDead == false) 
        {
            animator.SetInteger("state", 7); //Death Animation
            playerSounds.DeathSound();
            playerIsDead = true;
            rb.tag = "Untagged";
            RestartLevel();

        }
   
                 
    }


    private void AnimationCharacter()
    {
        //Animation
        
        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetInteger("state", 6); //Jump
        }
        else if (Input.GetMouseButton(0))
        {
            animator.SetInteger("state", 3); // Attack
            playerSounds.AttackSound();
        }
        else if (Input.GetKey(KeyCode.B))
        {
            animator.SetInteger("state", 1); // Dance
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
                animator.SetInteger("state", 2); //Run
        }
        else if (Input.GetKey(KeyCode.W))
        {
            animator.SetInteger("state", 4); //Walk
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetInteger("state", 5); //Backward Walk
        }
        else
        {
            animator.SetInteger("state", 0); //Idle
        }
}

    //Collision
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            playerIsOnGround = true;
        }

      
        if (collision.gameObject.tag == "Cube")
        {
            
            Destroy(collision.gameObject);
            playerSounds.HealthSound();
            if (playerHealth <= 90) 
            {
                playerHealth += 10;
                PlayerHealthBar.fillAmount = playerHealth / maxHealth;
            }
            else
            {
                playerHealth = 100f;
                PlayerHealthBar.fillAmount = playerHealth / maxHealth;
            }
           

        }


        if (collision.gameObject.tag == "Finish")
        {
            playerSounds.FinishSound();
            Invoke("LoadNextLevel", 3f);    
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        StartCoroutine(justWait());
        
    }

    private IEnumerator justWait()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
    


