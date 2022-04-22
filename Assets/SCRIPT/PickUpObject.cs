using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    public GameObject TextEventItem;
    public Inventory Invent;

    // au démarage on fait appel au script invent pour l'utiliser par la suite
    void Start()
    {
        Invent.initSlot();
        Invent = GameObject.Find("Inventory").GetComponent<Inventory> ();
    }

    // permet de vérifier la collision à l'intérieur d'un objet, on vérifier cette collision avec 
    // son tag et si ça match on applique le contenu des cas de notre switch.
    // Ici ça permet d'incrémenter le tableau de l'objet ramasser et de mettre à jour le numero
    // présent dans le canvas d'un objet
    void OnTriggerStay(Collider collider)
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            switch(gameObject.tag)
            {
                case "slot0":
                    Invent.slotI[0]++;
                    Invent.UpdateNumber(0, Invent.slotI[0].ToString());
                    Destroy(gameObject);
                break;
                case "slot1":
                    Invent.slotI[1]++;
                    Invent.UpdateNumber(1, Invent.slotI[1].ToString());
                    Destroy(gameObject);
                break;
                case "slot2":
                    Invent.slotI[2]++;
                    Invent.UpdateNumber(2, Invent.slotI[2].ToString());
                    Destroy(gameObject);
                break;
                case "slot3":
                    Invent.slotI[3]++;
                    Invent.UpdateNumber(3, Invent.slotI[3].ToString());
                    Destroy(gameObject);
                break;
                case "slot4":
                    Invent.slotI[4]++;
                    Invent.UpdateNumber(4, Invent.slotI[4].ToString());
                    Destroy(gameObject);
                break;
            }
        }
    }
}