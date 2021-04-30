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
        Debug.Log("Gameobject name: " + nombre);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Player found!");
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerManagement>().GainLoot(nombre);
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        Debug.Log("me ded");
    }
}