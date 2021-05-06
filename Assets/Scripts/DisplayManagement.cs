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
    private DisplayManagement Instance;

    void Start()
    {
        /*
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }*/
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

        if (ammo == 0)
            ammoText.color = Color.red;
        else
            ammoText.color = Color.black;
    }
    public void setGold(int gold)
    {
        goldText.text = ""+gold;
    }
    public void setPots(int pots)
    {
        potsText.text = ""+pots;

        if (pots == 0)
            potsText.color = Color.red;
        else
            potsText.color = Color.black;
    }
}
