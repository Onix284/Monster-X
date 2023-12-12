using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyMovement : MonoBehaviour
{
    public float fieldOfView = 120f;
    public Transform target;
    public bool playerIsInSight;
    public float AwakeDistance = 200f;
    public bool AwareOfPlayer;
    public float playerDistance;

    public PlayerMovement player;
    //Raycasting Variables
    public bool playerInVision;

    public NavMeshAgent enemyAgent;

    // Patrolling Variables
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    public float patrolSpeed = 3f;
    public bool patrolling = true;

    //Animation
    private Animator enemyAnim;

    //Death Animation
    private bool isDead = false;

    //Attack Variables
    [SerializeField] public float attackRange = 20f;

    //Enemy Health Variable
    public float enemyHealth = 70f; // Current Health

    //Sound Variables
    public AudioSource deathSound;

    private void Start()
    {
        if (patrolPoints.Length > 0)
        {
            enemyAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }

        enemyAnim = GetComponent<Animator>();

        deathSound = GetComponent<AudioSource>();

        if (deathSound.clip != null)
        {
            Debug.Log("Death sound clip found.");
        }
        else
        {
            Debug.LogWarning("No death sound clip found!");
        }

       

    }

    private void Update()
    {

        //Follow Player
        if (player.playerIsDead == false)
        {
            hitRay();


            playerDistance = Vector3.Distance(transform.position, target.position);

            Vector3 playerDirection = target.position - transform.position;

            float playerAngle = Vector3.Angle(target.position, playerDirection);

            if (playerAngle <= fieldOfView / 2f)
            {
                playerIsInSight = true;
            }
            else
            {
                playerIsInSight = false;
            }

            if (playerIsInSight == true && playerDistance <= AwakeDistance && playerInVision == true)
            {
                AwareOfPlayer = true;
                patrolling = false;
            }

            if (isDead == false)
            {
                if (AwareOfPlayer == true)
                {
                    enemyAgent.SetDestination(target.position);

                }
                else if (patrolling == true)
                {
                    if (patrolPoints.Length > 0)
                    {
                        float distanceToPatrolPoint = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);
                        if (distanceToPatrolPoint < 1.0f)
                        {
                            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
                            enemyAgent.SetDestination(patrolPoints[currentPatrolIndex].position);
                        }
                    }
                }
            }

           

            //Calling Animation
            if (enemyHealth > 0)
            {
                EnemyAnimation();

            }
            else if (enemyHealth <= 0 && !isDead)
            {
                Debug.Log("Attempting to play death sound...");

                enemyAnim.SetInteger("enemyState", 3);
                deathSound.Play();
                isDead = true;
                Debug.Log("Death sound played.");
            }


        }

    }

  

    //Raycast so enemy can not see player through obstacles
   private void hitRay()
    {
        Vector3 playerDirection = target.position - transform.position;
        RaycastHit hit;
        if(Physics.Raycast(transform.position, playerDirection, out hit))
        {
            if(hit.transform.tag == "Player")
            {
                playerInVision = true;
            }
        }
        else
        {
            playerInVision = false;
        }
    }


    //Enemy Animation
    void EnemyAnimation()
    {
        if (isDead)
        {
            enemyAnim.SetInteger("enemyState", 4);   // Set to death animation
        }
        else if (AwareOfPlayer) 
        {
            if (playerDistance <= attackRange && player.playerIsDead == false)
            {
                enemyAnim.SetInteger("enemyState", 2); // Attack animation
                deathSound.Play();
            }
            else
            {
                enemyAnim.SetInteger("enemyState", 1); // Follow player animation
            }
        }
        else
        {
            enemyAnim.SetInteger("enemyState", 0); // Idle animation or other animation when not aware of player
        }
    }

}