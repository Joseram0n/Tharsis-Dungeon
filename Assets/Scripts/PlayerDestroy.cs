using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private static PlayerDestroy Instance;
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
    }
}
