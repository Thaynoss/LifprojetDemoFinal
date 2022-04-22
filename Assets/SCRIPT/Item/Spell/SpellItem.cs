using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellItem : MonoBehaviour
{
    public GameObject spellWarmUpFX;
    public GameObject spellCastFX;
    //public string spellAnimation;

    [Header("Spell Type")]
    public bool isMagicSpell;
    public bool isPyroSpell;

        public virtual void AttempToCastSpell(StatPlayer statPlayer,WeaponSlotManager weaponSlotManager){
        //base.AttempToCastSpell(statPlayer,weaponSlotManager);
        }

}
