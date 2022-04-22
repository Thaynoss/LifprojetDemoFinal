using UnityEngine;

public class Trap : MonoBehaviour
{
    private GameObject trap;
    private StatPlayer sp;
    private Collider boxCollider;

    public void OnCollisionEnter(Collision collision){
        
        if(collision.collider.tag=="Player"){
            sp.healthPlayer-=5;
        }            
    }


    public void Start(){
        GameObject player=GameObject.Find("Trista");
        sp=player.gameObject.transform.GetComponent<StatPlayer>();
        boxCollider = GetComponent<Collider>();

    }
}
