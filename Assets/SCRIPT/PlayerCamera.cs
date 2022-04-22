using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{

    [SerializeField] private float mouseSensitvity;
    // Start is called before the first frame update

    private Transform parent;

    private void Start()
    {
        parent = transform.parent;
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void Rotate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitvity * Time.deltaTime;
        parent.Rotate(Vector3.up,mouseX);

    }
}
