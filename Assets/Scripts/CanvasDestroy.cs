using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDestroy : MonoBehaviour
{
    private CanvasDestroy Instance;
    // Start is called before the first frame update
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
