using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayManagement : MonoBehaviour
{
    // Start is called before the first frame update

    public Text ammoText;
    public Text goldText;
    public Text potsText;
    public int health = 20;
    public Slider slider;
    public int contador;


    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            setAmmo(contador);
            setGold(contador);
            setPots(contador);
            contador++;
        }
    }
    public void setMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(int health)
    {
        slider.value = health;
    }

    public void setAmmo(int ammo)
    {
        ammoText.text = ""+ammo;
    }
    public void setGold(int gold)
    {
        goldText.text = ""+gold;
    }
    public void setPots(int pots)
    {
        potsText.text = ""+pots;
    }
}
