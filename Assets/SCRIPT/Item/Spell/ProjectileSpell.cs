/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : SpellItem
{

    public int baseDamage;
    public float projectileVelocity;
    Rigidbody rigidBody;

    public override void AttempToCastSpell(StatPlayer statPlayer,WeaponSlotManager weaponSlotManager){
        base.AttempToCastSpell(statPlayer,weaponSlotManager);
        GameObject instatiateWarmUpSpellFX = Instantiate(spellWarmUpFX,weaponSlotManager.rightHandSlot.transform);
        instatiateWarmUpSpellFX.gameObject.transform.localScale=new Vector3(100,100,100);
    }

    public override void SucessfullyCastSpell(StatPlayer statPlayer,WeaponSlotManager weaponSlotManager){
        base.SucessfullyCastSpell(statPlayer,weaponSlotManager);
    }

}*/
