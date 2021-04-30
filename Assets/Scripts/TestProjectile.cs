using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            if (collision.TryGetComponent<Enemy>(out Enemy componente)) //Enemy Layer
            {
                Debug.Log("I hit him!");
                componente.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}