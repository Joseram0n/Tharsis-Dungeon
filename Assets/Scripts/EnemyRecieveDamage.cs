using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRecieveDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DealDamage(int damage)
    {
        health -= damage;
    }
}
