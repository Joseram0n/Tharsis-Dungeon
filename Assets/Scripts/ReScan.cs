using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ReScan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        //Espera que la dugeon este creada para scanear
        yield return new WaitForSeconds(1);
        AstarPath.active.Scan();
    }
}


