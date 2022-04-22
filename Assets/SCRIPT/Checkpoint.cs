using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject checkPoint;
    private Collider boxCollider;
    public Vector3 pos;
    private Vector3 savePose; 

    public void OnCollisionEnter(Collision collision){
        
            if(collision.collider.tag=="Checkpoint"){
            pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            boxCollider.enabled=false;
            Debug.Log("Collision false");
        }
    }
    /* Récupére le BoxCollider contenue dans le gameObject Checkpoint*/
    public void Start(){
        boxCollider = checkPoint.GetComponent<BoxCollider>();
    }

    /* On sauvegarde la position */
    private void Update(){
        savePose=pos;   
    }

}
