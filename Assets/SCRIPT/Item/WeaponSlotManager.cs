using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotManager : MonoBehaviour
{
    public WeaponHolderSlot leftHandSlot;
    public WeaponHolderSlot rightHandSlot;

    private void Awake(){
        WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
        foreach(WeaponHolderSlot weaponSlot in weaponHolderSlots){
            if(weaponSlot.isLeftHandSlot){
                leftHandSlot=weaponSlot;
            }else if(weaponSlot.isRightHandSlot){
                rightHandSlot=weaponSlot;
            }
        }
    }

    public void LoadWeaponSlot(WeaponItem weaponItem , bool isLeft){
        if(isLeft){
            leftHandSlot.LoadWeaponModel(weaponItem);
        }else{
            rightHandSlot.LoadWeaponModel(weaponItem);
        }
    }

}
