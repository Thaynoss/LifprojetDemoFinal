using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody projectileCurrent;

    // Instancie un projectile clone en direction du joueur qui lui infligera des dégats.  
    public void InstantiateProjectile(){
            
        projectileCurrent = Instantiate(projectile,transform.position,Quaternion.identity).GetComponent<Rigidbody>(); //On cherche l'objet a dupliqué .. si remplacer par variable "projectile"
                                                                                                                                        //, il se clone et se supprime
        projectileCurrent.AddForce(transform.forward*50f,ForceMode.Impulse);
        projectileCurrent.AddForce(transform.up*10f,ForceMode.Impulse);
    }

    public void Awake(){
        projectile= projectile.GetComponent<Rigidbody>();
    }

}
