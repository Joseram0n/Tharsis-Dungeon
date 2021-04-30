using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    // Start is called before the first frame update
    string nombre;

    private void Start()
    {
        nombre = this.gameObject.name;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerManagement>().GainLoot(nombre);
            Destroy(this.gameObject);
        }
    }
}