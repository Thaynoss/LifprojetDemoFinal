using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IKFootSolver : MonoBehaviour
{   
    [SerializeField] LayerMask terrainLayer = default;
	[SerializeField] Transform body = default;
	[SerializeField] IKFootSolver otherFoot =default;
	[SerializeField] float speed = 1;
	[SerializeField] float stepDistance = 4;
	[SerializeField] float stepLength = 4;
	[SerializeField] float stepHeight = 1;
	[SerializeField] Vector3 footOffset = default;
	float footSpacing;
	Vector3 oldPosition, currentPosition, newPosition;
	Vector3 oldNormal, currentNormal, newNormal;
	float lerp;

	private void Start()
	{
		footSpacing = transform.localPosition.x ;// on vas calculer la position par rapport au corp mais seulement pour une jambe
		currentPosition = newPosition = oldPosition = transform.position ;// position de la cible du pied ici tous egaux ( début )
		currentNormal = newNormal = oldNormal = transform.up ;// toute les normal sont elles aussi égaux (début) 
		lerp = 1; 
	}

	// Update  is called once per frame

	void Update()
	{
		transform.position =currentPosition;//
		transform.up = currentNormal;// inialiser au début de la frame mais sera calculer tout du long il vas y avoir un loop et les position seront à chaque fois recalculer à le fin du prog
		
		Ray ray = new Ray(body.position + (body.right * footSpacing), Vector3.down); 
		//Pour calculer la position ou le pied sera placé à chaque fois on vas faire un ray cast c'est à dire fabriqué une ligne invisible
		//sur la scene pour détecter les intersection donc les éventuelles colision posibles ici entre la position du corp et la direction où l'on veut aller
		//body.right partie droite du corp fois la taille du pas, vector3.down est un racourcis pour l'écriture Vector3(0, -1, 0)--> en direction du sol quoi
		
			// condition pour lancer le rayon: si ce dernier et lancer et qu'il ne touche rien on à un probleme(pas de sol)
			// même principe qu'un personne aveugle si le baton ne touche pas le sol = probleme 
			if (Physics.Raycast (ray , out RaycastHit info, 10, terrainLayer.value)) // ici (ray , out RaycastHit info, 10 terrainLayer.value)on dit envoie le rayon garde l'information de ce qu'il à toucher ou pas 
			{
				// si l'info stocker sur le RaycastHit info dit que l'on à toucher un objet alors on regarde si la distance entre la nouvelle position  et la poosition que l'on vient de calculer avec le rayon
				// est plus grande que la distance du pas (c'est à dire suffisante pour bouger). On vas bien sur envoyer un rayon en direction du sol suffisament proche du corp mais pas trop
				// on vas donc vérifier que le corp se trouve assez loin du pied pour faire un pas en avant ou en arrière. Il faut aussi verifier que l'autre pied n'est pas en train de bouger
				if (Vector3.Distance(newPosition,info.point) > stepDistance && !otherFoot.isMoving() && lerp >= 1)// lerp 1 ici pour dire que la jambe est prete à faire le mouvement mais ne doit pas le commencer -> si toute les condition sont vrai alors (bonne distance, autre jambe posé et celle actuelle pretent) on peut débuter le movement
				{
					lerp = 0; // ici on dit le mouvement peut débuter on peut lancer l'animation donc calculer la nouvelle position 
					int direction = body.InverseTransformPoint(info.point).z > body.InverseTransformPoint(newPosition).z ? 1 : -1;// on regarde ici la direction où l'on veut aller et ce part rapport au corp [(inversTransformpoint -> transforme une position dans l'espace)( ici par rapport à info.point soit c'est à dire l'endroit ou le rayon à touché le sol)
																			// inversTransformpoint vas donc calculers si l'axe Z de info.point et newPosition sont en face (1) ou derrière le personnage (-1)
					newPosition = info.point  + (body.forward * stepLength * direction) + footOffset; // newposition -> position ou la cible de la cinématique inverse vas bouger = info.point (raycast) + (body.forward * stepLength * direction)<- négatif ou positif
					newNormal = info.normal; // angle par rappport au sol c'est à dire la normal du rayon pour dire quel doit etre l'orientation du pied par raport au sol
				}
			}

	

		if (lerp < 1)//inferieur a 1 donc toujour dans le cycle du mouvement
		{
			Vector3 tempPosition = Vector3.Lerp(oldPosition , newPosition,lerp); // nouveau vecteur 3 temporaire pourmodifier les corrdonné en y. vector3.lerp et une fonction qui permet d'interpoler linéairement entre deux point (oldPosition, newPosition) on à du coup une valeur appeler lerp
			//qui sera entre 0 et un nombre à virgule . On vas evaluer entre ces deux position et linéairement bouger la jambe en fonction du temps et ce via lerp qui vas etre modifier à chaque frame
			tempPosition.y +=Mathf.Sin(lerp * Mathf.PI) * stepHeight; // on combine la ligne d'avant avec celle ici presente pour avoir un mouvement en forme d'arc (trigonométrie)

			currentPosition = tempPosition; // on affecte la valeur temporaire à la nouvelle position
			currentNormal = Vector3.Lerp(oldNormal , newNormal ,lerp );// on affecte l'ancienne normal à la nouvelle interpolation linéaire pp courbe de bézier
			lerp += Time.deltaTime *  speed; // on incrémente la valeur lerp par Time.delta time et par speed pour avoir un mouvment continue à chaque frame
		}
		else // si sup à 0
		{
			oldPosition = newPosition; // relance le cycle vers transform.position =currentPosition; etc
			oldNormal = newNormal;

		} 
	}

	private void onDrawGizmos()
	{
		Gizmos.color = Color.red;//permet de mieux visualiser le cycle et les positions

		Gizmos.DrawSphere (newPosition , 0.5f);

	}


	public bool isMoving() // retourne vrai ou faux si la variable qui active le mouvement est à inférieur à 1 c'est à dire tant que la jambe n'est pas reposer sur le sol
	{

		return lerp < 1;
	}
}
