using UnityEngine;

public class SwordCollider : MonoBehaviour{

    private GameObject sword;
    private StatPlayer sp;
    private Collider boxCollider;

    public void OnCollisionEnter(Collision collision){
        Debug.Log(collision.collider.tag);
            if(collision.collider.tag=="Enemy" && collision.collider.name=="SpiderBossGrantComplete"){
            sp.hitEnemy(0);
            boxCollider.enabled=false;
            }
            if(collision.collider.tag=="Enemy" && collision.collider.name=="spiderfinal_tex_rig"){
            sp.hitEnemy(1);
            Debug.Log("Ptite araignée toucher");
            boxCollider.enabled=false;
        }
    }

    private void setCollisionFalse(){
        boxCollider.enabled=false;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0)){
            boxCollider.enabled=true;
            Invoke("setCollisionFalse",2f);
            Debug.Log("update collider");
        }
    }

    public void Start(){
        GameObject player=GameObject.Find("Trista");
        sp=player.gameObject.transform.GetComponent<StatPlayer>();
        boxCollider = GetComponent<Collider>();
        boxCollider.enabled=false;
    }

}
