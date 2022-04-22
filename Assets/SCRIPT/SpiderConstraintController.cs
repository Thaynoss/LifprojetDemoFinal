using UnityEngine;


public class SpiderConstraintController : MonoBehaviour
{
    Vector3 originalPosition; // Position original de la jambe pour garde la jambe sur le sol
    public GameObject moveCube; 
    public float legMoveSpeed = 7f; // Vitesse de la jambe
    public float moveDistance = 0.7f; 
    public float moveStoppingDistance = 0.7f; 
    public SpiderConstraintController oppositeLeg; //Definit quel est la jambe opposé
    bool isMoving = false; //Permet de dire à la jambe opposer si elle à l'autorisation de faire le mouvement
    bool moving = false;

    private void Start()
    {
        originalPosition = transform.position; // fixe la pate au sol au demmarage de la partie
    }

    private void Update()
    {

        float distanceToMoveCube = Vector3.Distance(transform.position, moveCube.transform.position);
        if ((distanceToMoveCube > moveStoppingDistance && !oppositeLeg.isItMoving()) || moving) ///modif >= a >
        {
            moving = true;
            transform.position = Vector3.Lerp(transform.position, moveCube.transform.position + new Vector3(0.0f,-0.1f,0.0f) , Time.deltaTime * legMoveSpeed);
            originalPosition =transform.position;
            isMoving = true;
            if (distanceToMoveCube < moveStoppingDistance)
            {
                moving = false;
            }

        }
        else
        {
            transform.position = originalPosition;
            isMoving = false;

        }

    }
    
     public bool isItMoving() //to be called by the oppiste leg to check if the leg is moving or not
    {
        return isMoving;
    }
}