using UnityEngine;

public class LevelPlayer : MonoBehaviour{

    public StatPlayer sp;

    private int level=1;
    private const int LEVELMAX=20;
    public int experiencePoint=0;
    private int expRequierd=100;
    private bool takeLevel=false;
    public int healthL;
    //public int manaL;
    public int damageL;

    public void levelUp(){
        
        if(level<=LEVELMAX){
        takeLevel=true;
        level++;
        healthL+=healthL;
        //manaL+=manaL+level;
        damageL+=damageL+level;
        experiencePoint=0;
        expRequierd+=expRequierd;
        }

    }

    // Start is called before the first frame update
    public void Start()
    {
        healthL=sp.healthPlayer;
        //manaL=sp.manaPlayer;
        damageL=sp.damagePlayer;
    }

    // Update is called once per frame
    void Update(){
        if(experiencePoint>=expRequierd){
        levelUp();
        if(takeLevel){
            sp.healthPlayer=healthL;
            //sp.manaPlayer=manaL;
            sp.damagePlayer=damageL;
            takeLevel=false;
            }  
        }
    }

}
