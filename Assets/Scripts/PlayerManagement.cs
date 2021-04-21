using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public int maxHealth = 6;
    public int currentHealth;

    public HealthBar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
            takeDamage();
        if (Input.GetKeyDown(KeyCode.H))
            heal();
        
    }


    public void takeDamage()
    {
        currentHealth--; //Si llega a 0, muere
        healthbar.setHealth(currentHealth);
    }
    public void heal()
    {
        currentHealth = maxHealth;
        healthbar.setHealth(maxHealth);
    }
    
}
