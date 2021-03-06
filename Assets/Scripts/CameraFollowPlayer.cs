using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform player;
    public float smoothing;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = GameVariables.getVolume();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(player!=null)
        {
            Vector3 newposition = Vector3.Lerp(transform.position,player.transform.position+offset,smoothing);
            transform.position = newposition;

        }
    }
}
