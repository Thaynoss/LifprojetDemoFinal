using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePersonnalise : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject effect;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f,1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int effectDamage;
    public float effectRange;
    public float effectForce;

    //LifeTime
    public int maxCollisions;
    public float maxLifetime;
    public bool effectOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Setup(){
        physics_mat= new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        
        GetComponent<SphereCollider>().material = physics_mat;

        //Set gravity
        rb.useGravity = useGravity;
    }
    
    private void Effect(){
        if(effect!=null) Instantiate(effect,transform.position,Quaternion.identity);

        //Verifie si ennemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, effectRange,whatIsEnemies);
        for(int i=0;i<enemies.Length;i++){

            if(enemies[i].GetComponent<Rigidbody>())
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(effectForce,transform.position,effectRange);

        }

    }

    private void OnCollisionEnter(Collision collision){
        if(collision.collider.CompareTag("Bullet")) return;

        collisions++;

        if(collision.collider.CompareTag("Enemy") && effectOnTouch) Effect();
        Invoke("Delay",0.05f);
    }
    private void Delay(){
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, effectRange);

    }

    // Start is called before the first frame update
    void Start()
    {
        Setup();
    }


    // Update is called once per frame
    void Update(){
        if(collisions>maxCollisions) Effect();
        
        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0) Effect();

    }
}
