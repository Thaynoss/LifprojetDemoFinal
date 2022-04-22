using UnityEngine;
using UnityEngine.UI;


public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetMana(int mana){

        slider.maxValue=mana;
        slider.value = mana;
    }

    public void SetCurrentMana(int currentMana){
        Debug.Log(currentMana);
        slider.value = currentMana;
    }

}