using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage=20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name!="Player")
        {
            if (collision.gameObject.layer==6) //Enemy Layer
            {
                Debug.Log("I hit him!");
                collision.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
