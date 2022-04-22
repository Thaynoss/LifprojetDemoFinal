using UnityEngine;

public class StatPlayer : MonoBehaviour {
    
    public GameObject Player;
    public Checkpoint cp;

    public EnemyAI[] AI;
    public LevelPlayer LP;
    public HealthBar healthBar;
    //public ManaBar manaBar;

    public int healthPlayer;
    public int currentHealth;

    /*public int manaPlayer;
    public int currentMana;*/

    public int damagePlayer;
    private bool alive=true;
    public Vector3 position;

    /*
        Initialise notre tableau d'ennemies avec les gameObjects fils du gameObject "Enemy".
        On initialise la barre de Vie avec la vie courrante du joueur.  (mana non implémenter encore)
    */
    void Start(){
       GameObject Enemy =GameObject.Find("Enemy");
        for (int i = 0; i < Enemy.transform.childCount;i++){
            AI[i]=Enemy.gameObject.transform.GetChild(i).GetChild(0).GetComponent<EnemyAI>();
        }

        //position=new Vector3(0,0,0);
        currentHealth=SetHealthPlayer();
        healthBar.SetHealth(currentHealth);
        
        //currentMana=SetManaPlayer();
        //manaBar.SetMana(currentMana);

    }

    /*
        Quand le joueur meurt , on met toute les variables a zero par précautions.
        On desactiver aussi les déplacements du joueur et on invoque la fonction revive 4 secondes aprés. 
    */
    public void die(){
        currentHealth=0;
        //currentMana=0;
        damagePlayer=0;
        alive=false;
        
        Player.SetActive(alive);
        transform.GetComponent<PlayerMovementLastChance>().enabled=false;

        Invoke("revive",4);

    }

    /*
    Fait revivre le joueur a l'emplacement du checkpoint
    On récupére les statistique lié a son niveau actuelle
    et on fait réapparaitre le joueur.
    */
    public void revive(){
        transform.position=new Vector3(-27.232206344604493f,1f,-101.10720825195313f);
        healthPlayer=LP.healthL;
        currentHealth=healthPlayer; 
        healthBar.SetHealth(healthPlayer);
        
        //manaPlayer=LP.manaL;
        //currentMana=manaPlayer;
        //manaBar.SetMana(manaPlayer);

        damagePlayer=LP.damageL;
        alive=true;
        Player.SetActive(alive);
        transform.GetComponent<PlayerMovementLastChance>().enabled=true;


    }

    /*
    Prend des dégats en fonctions de l'ennemie qui l'attaque.
    */
    public void takeDamage(int idAI){
        if(currentHealth >=0){
            currentHealth-=AI[idAI].damageEnemy;

            healthBar.SetCurrentHealth(currentHealth);
        }
    }

    /*
    Inflige des dégats a l'ennemie en fonctions de son tag, et si l'ennemie meurt, on récupére les points d'éxperience
    qui lui est associés
    */
    public void hitEnemy(int idAI){
        if(AI[idAI].healthEnemy>0){
        AI[idAI].healthEnemy-=damagePlayer;
        }
        if(AI[idAI].healthEnemy<=0){ 
            GameObject idEnemy =GameObject.Find(AI[idAI].name);
            Destroy(idEnemy.transform.parent.gameObject);

            LP.experiencePoint+=AI[idAI].experiencePointDropByEnemy;

        }
    }

    public void OnCollisionEnter(Collision collision){
        if(collision.collider.tag=="Projectile" && collision.collider.name=="spyteAcid1(Clone)"){
            takeDamage(0);     
            Destroy(GameObject.Find("spyteAcid1(Clone)"));
        }
        if(collision.collider.tag=="Projectile" && collision.collider.name=="spyteAcid2(Clone)"){
            takeDamage(1);     
            Destroy(GameObject.Find("spyteAcid2(Clone)") );
        }
    }

    /*
    Set la vie du joueur
    */
    private int SetHealthPlayer(){
        return healthPlayer;
    }

    // Update is called once per frame
    void Update(){
         if(currentHealth <= 0){
            die();
        }
    }

}