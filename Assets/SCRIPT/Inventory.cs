using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    // tableau d'entier correspondant au nombre d'objet de chaque sot
    public int[] slotI;
    // tableau de gameobject relatif à nos objet à ramasser
    public GameObject[] slotG;

    // permet d'initialiser nos tableaux. On remplit le tableau d'objet selon le nombre d'enfant dans la hiérarchie
    // donc selon le nombre d'objet à ramasser
    public void initSlot()
    {
        slotI = new int[transform.childCount];
        slotG = new GameObject[transform.childCount];
        for(int i = 0 ; i < transform.childCount ; i++)
        {
            slotG[i] = GameObject.Find("Slot" + i);
        }
        Debug.Log("init slot");
    }

    // met à jour le nombre présent sur le canvas de chaque objet de l'inventaire
    public void UpdateNumber(int nbSlot, string txt)
    {
        transform.GetChild (nbSlot).GetChild (1).GetComponent<Text> ().text = txt;
    }
}
