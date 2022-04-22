using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeWeaponFromQuickSlot : MonoBehaviour
{ 
    // tout les gameobject des objets qui sont présents dans la main
    public GameObject swordHand;
    public GameObject axeHand;
    public GameObject shieldHand;

    // tout les gameobject des objets qui sont dans le dos du personnage
    public GameObject swordBack;
    public GameObject axeBack;
    public GameObject shieldBack;

    // gameObject de l'inventaire des armes
    public GameObject swordQuickSlot;
    public GameObject axeQuickSlot;
    public GameObject shieldQuickSlot;
    
    // on initialise tout les gameobject a faux pour ne pas les voir sur la scène
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;

        swordHand.SetActive(false);
        axeHand.SetActive(false);
        shieldHand.SetActive(false);

        swordBack.SetActive(false);
        axeBack.SetActive(false);
        shieldBack.SetActive(false);

        swordQuickSlot.SetActive(false);
        axeQuickSlot.SetActive(false);
        shieldQuickSlot.SetActive(false);
    }

    // on ajoute selon la condition les armes derrière le personnage ou a la ceinture pour l'épée
    public void addBackWeapon() {
        if (swordQuickSlot.activeSelf) {
            swordBack.SetActive(true) ;
        }

        if (axeQuickSlot.activeSelf) {
            axeBack.SetActive(true);
        }

        if (shieldQuickSlot.activeSelf) {
            shieldBack.SetActive(true);
        }
    }

    // selon la touche pressé et selon le gameobject activé sur la scène on gère les cas des conditions
    public void inputAlpha() {
        // si on appuye sur 1 et que l'épée est dans l'inventaire des armes on active l'arme dans sa main
        // et si il y a déjà la hache on désactive l'épée et on active la hache et on active l'épée de la ceinture
        if (Input.GetKeyDown(KeyCode.Alpha1) && swordQuickSlot.activeSelf) {
            swordHand.SetActive(true);
            if (axeQuickSlot.activeSelf) {
                swordHand.SetActive(true);
                axeHand.SetActive(false);
            }
        }

        // même cas que le premier if du haut
        if (Input.GetKeyDown(KeyCode.Alpha2) && axeQuickSlot.activeSelf) {
            axeHand.SetActive(true);
            if (swordQuickSlot.activeSelf) {
                swordHand.SetActive(false);
                axeHand.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && shieldQuickSlot.activeSelf) {
            shieldHand.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            swordHand.SetActive(false);
            axeHand.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            swordHand.SetActive(false);
            axeHand.SetActive(false);
            shieldHand.SetActive(false);
        }
    }

    // permet de désactiver le gameobject de l'arrière du personnage si celui-ci est activé dans la main
    public void hideBackWeapon() {
        if (axeHand.activeSelf) {
            axeBack.SetActive(false);
        }

        if (swordHand.activeSelf) {
            swordBack.SetActive(false);
        }

        if (shieldHand.activeSelf) {
            shieldBack.SetActive(false);
        }
    }

    void Update()
    {
        addBackWeapon();
        inputAlpha();
        hideBackWeapon();
    }
}