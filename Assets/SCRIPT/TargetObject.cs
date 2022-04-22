using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public Transform target;

    // permet de bloqué le regard de la caméra dans la direction de l'objet
    void Update()
    {
        transform.LookAt(target);
    }
}