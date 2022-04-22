using UnityEngine;

public class LegAimGroundv2 : MonoBehaviour
{
    int layerMask;

    // Start is called before the first frame update
    void Start()
    {
        layerMask = LayerMask.GetMask("Ground");

    }                                         

    // Update is called once per frame
    // Attention ne pas ajouter de box collider car provoque des bugs de positionnement
    // 0.12 valeur pour eviter objet traverse objet environement
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position , -transform.up, out hit, layerMask))
        {
            transform.position = hit.point + new Vector3(0f,0.2f,0f) ; // Attenttion en dessous de 0.3 c'est en dessous de la map que l'objet passe !!
        }
    }
}