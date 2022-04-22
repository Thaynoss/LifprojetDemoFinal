using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProjectilWeapon : MonoBehaviour
{
    //Bullet
    public GameObject bullet;

    //bullet force
    public float shootForce,upwardForce;

    //Stat weapon
    public float timeBetweenShooting,spread,reloadTime,timeBetweenShots;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;

    int bulletsLeft,bulletsShot;

    //Bool
    bool shooting, readyToShoot, reloading;

    //Reference 
    public Camera tpsCam;
    public Transform attackPoint;

    //Graphisme
    public TextMeshProUGUI ammunitionDisplay; 

    public bool allowInvoke=true;


    // Start is called before the first frame update
    void Awake(){
        bulletsLeft=magazineSize;
        readyToShoot=true;
    }
    private void MyInput(){
        if(allowButtonHold) shooting=Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading
        if(Input.GetKeyDown(KeyCode.R) && bulletsLeft <magazineSize && !reloading) Reload();

        if(readyToShoot && shooting && ! reloading && bulletsLeft <= 0) Reload();
         
        //shooting
        if(readyToShoot && shooting && !reloading && bulletsLeft >0){
           bulletsShot =  0;

           Shoot(); 
        }
    }

    private void Shoot(){
        readyToShoot=false;

        //Position exacte du coup
        Ray ray = tpsCam.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit hit;

        //Verifie qu'un coup est porté
        Vector3 targetPoint;
        if(Physics.Raycast(ray,out hit))
            targetPoint=hit.point;
        else targetPoint=ray.GetPoint(75); //un point loin du joueur 
        
        //Calcul la direction de l'attaque jusqua la cible
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Propagation calculé
        float x = Random.Range(-spread, spread);
        float y= Random.Range(-spread, spread);

        //Calcul new direction avec la propagation   
        Vector3 directionWithSpread=directionWithoutSpread + new Vector3(x,y,0);

        //Instance des projectiles
        GameObject currentBullet = Instantiate(bullet,attackPoint.position,Quaternion.identity);
        //Rotationne le projectile
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Ajoutes des Forces au projectile
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized*shootForce,ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(tpsCam.transform.up*upwardForce,ForceMode.Impulse);

        //reset le tir 
        if(allowInvoke){
            Invoke("ResetShot",timeBetweenShooting);
            allowInvoke = false;
        }
        if (bulletsShot< bulletPerTap && bulletsLeft>0){
            Invoke("Shoot",timeBetweenShots);
        }
        bulletsLeft--;
        bulletsShot++;
    }

    private void ResetShot(){
        readyToShoot=true;
        allowInvoke=true;
    }

    private void Reload(){
        reloading = true;
        Invoke("ReloadFinished",reloadTime);
    }
    private void ReloadFinished(){
        bulletsLeft = magazineSize;
        reloading = false;
    }

    // Update is called once per frame
    void Update(){
        
        MyInput();
        if(ammunitionDisplay != null)
            ammunitionDisplay.SetText(bulletsLeft / bulletPerTap + " / " + magazineSize/bulletPerTap);
    }
}
