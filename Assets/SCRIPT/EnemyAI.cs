using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public StatPlayer sp;
    [Header("Enemy stat")]
    public int healthEnemy;
    private int currentHealth;
    public int manaEnemy;
    private int currentMana;
    public int damageEnemy;
    public int experiencePointDropByEnemy;

    [Header("A.I Settings")]
    public Projectile projectile;
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask WhatIsGround, WhatIsPlayer;
    
//Pattrouille
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

//Attaque
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public bool touchPlayer=false;

//Etat
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;


    // Initialise les données de Joueur et de l'agent/IA
    private void Awake(){
        Debug.Log("Awake");
        player = GameObject.Find("Player/Trista").transform;
        agent = GetComponent<NavMeshAgent>();
        
        currentHealth=healthEnemy;
        currentMana=manaEnemy;

    }

    /* Prend des coordonnées aléatoire et fait pattrouiller l'araignée */
    private void SearchWalkPoint(){
        float randomZ = Random.Range(-walkPointRange,walkPointRange);
        float randomX = Random.Range(-walkPointRange,walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint,-transform.up,2f,WhatIsGround))
            walkPointSet=true;
    }

    /* Fait avancer l'araignée jusqu'au coordonées randomX et randomZ */ 
    private void Patroling(){
        if(!walkPointSet) SearchWalkPoint();
        if(walkPointSet) agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if(distanceToWalkPoint.magnitude < 1)
            walkPointSet=false;
    }

    // Poursuit le joueur suivant sa position.
    private void ChasePlayer(){
        agent.SetDestination(player.position);
    }

    /*Attaque le joueur s'il est dans la range de l'ennemie et active le script Projectile ,
    cela coute de la mana a l'ennemie pour pas qu'il tire a l'infini
    On detruit le clone du projectile 
    */  
    private void AttackPlayer(){
        agent.SetDestination(transform.position);
        transform.LookAt(player);


        if(!alreadyAttacked && currentMana>0){

            projectile.InstantiateProjectile();
            alreadyAttacked=true;

            currentMana-=5;
            Destroy(projectile.projectileCurrent.gameObject,timeBetweenAttacks);
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
       }
    }

    //Permet de reinitialisé l'attaque quand la fonction est appelé.
    private void ResetAttack(){
        alreadyAttacked=false;
    }

    //Permet de voir le rayon d'attaque(rouge) et le rayon de son champ de vision(jaune)
    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position,sightRange);
    }

    // Update is called once per frame
    void Update(){

        playerInSightRange=Physics.CheckSphere(transform.position,sightRange,WhatIsPlayer);
        playerInAttackRange=Physics.CheckSphere(transform.position,attackRange,WhatIsPlayer);
        agent.SetDestination(transform.position);

        if(!playerInSightRange && !playerInAttackRange){
            Patroling();
        }        
        if(playerInSightRange && !playerInAttackRange ) {
            ChasePlayer();
        } 
        if(playerInSightRange && playerInAttackRange ) {
            AttackPlayer();
        }
    }

}
