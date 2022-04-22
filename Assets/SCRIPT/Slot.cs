using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Slot : MonoBehaviour
{
    public Inventory Invent;
    private int nbSlot;
    public StatPlayer sp;
    public HealthBar hb;
    public TakeWeaponFromQuickSlot Weapon;

    // au démarage on fait appel au script TakeWeaponQuickSlot et Inventory pour
    // pouvoir les appelés dans le rest du code
    public void Start()
    {
        Weapon = GameObject.Find("Player").GetComponent<TakeWeaponFromQuickSlot> ();
        Invent = GameObject.Find("Inventory").GetComponent<Inventory> ();
        nbSlot = transform.GetSiblingIndex(); 
    }

    // permet de consommer une potion de vie présente dans l'inventaire
    public void ConsumHealth()
    {
        if(Invent.slotI[nbSlot] > 0)
        {
            Invent.slotI[nbSlot]--;
            sp.currentHealth += 10;
            hb.SetCurrentHealth(sp.currentHealth);

            Invent.UpdateNumber(nbSlot, Invent.slotI[nbSlot].ToString());
        }
    }

    // permet de consommer une potion de mana présente dans l'inventaire
    // agit sur le canvas en décrémentant le nombre d'objet
    // pas de mana implémenter sur le joueur donc normal que rien ne se passe
    public void ConsumMana()
    {
        if(Invent.slotI[nbSlot] > 0)
        {
            Invent.slotI[nbSlot]--;
            Invent.UpdateNumber(nbSlot, Invent.slotI[nbSlot].ToString());
        }
    }

    // permet d'ajouter à l'inventaire d'arme l'épée si on clique sur l'épée de l'inventaire
    // agit sur le canvas en décrémentant le nombre d'objet
    public void addSwordToQuickSlot()
    {
        if(Invent.slotI[nbSlot] > 0)
        {
            Invent.slotI[nbSlot]--;
            Invent.UpdateNumber(nbSlot, Invent.slotI[nbSlot].ToString());
            Weapon.swordQuickSlot.SetActive(true);
        }
    }

    // permet d'ajouter à l'inventaire d'arme la hache si on clique sur la hache de l'inventaire
    // agit sur le canvas en décrémentant le nombre d'objet
    public void addAxeToQuickSlot()
    {
        if(Invent.slotI[nbSlot] > 0)
        {
            Invent.slotI[nbSlot]--;
            Invent.UpdateNumber(nbSlot, Invent.slotI[nbSlot].ToString());
            Weapon.axeQuickSlot.SetActive(true);
            
        }
    }

    // permet d'ajouter à l'inventaire d'arme le bouclier si on clique sur le bouclier de l'inventaire
    // agit sur le canvas en décrémentant le nombre d'objet
    public void addShieldToQuickSlot()
    {
        if(Invent.slotI[nbSlot] > 0)
        {
            Invent.slotI[nbSlot]--;
            Invent.UpdateNumber(nbSlot, Invent.slotI[nbSlot].ToString());
            Weapon.shieldQuickSlot.SetActive(true);
        }
    }
}