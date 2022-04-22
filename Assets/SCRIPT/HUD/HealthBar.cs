using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;

    /* La taille de la barre de vie s'adapte avec la vie max du joueur  */
    public void SetHealth(int health){
        slider.maxValue=health;
        slider.value = health;
    }

    /*La barre de vie s'adapte avec la vie courrante du joueur s'il prend un dégat  */
    public void SetCurrentHealth(int currentHealth){
        slider.value = currentHealth;
    }

}