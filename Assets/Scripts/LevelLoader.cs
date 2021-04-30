using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public BoxCollider2D gateDungeon;
    public GameObject crossfade;
    public BoxCollider2D player_collider2D;

    public int numeroEscenaDestino;
    public float tiempoEspera;
    
    void LoadDungeonLevel()
    {
        StartCoroutine(LoadLevel(numeroEscenaDestino));
    }

     IEnumerator LoadLevel(int nivel)
     {
        crossfade.SetActive(true);

        yield return new WaitForSeconds(tiempoEspera);

        SceneManager.LoadScene(nivel);

        crossfade.GetComponent<Animator>().SetTrigger("End");
    }


    void Start()
    {
        crossfade.SetActive(false);
        player_collider2D = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player_collider2D != null)
        {
            if (gateDungeon.IsTouching(player_collider2D))
            {
                LoadDungeonLevel();
            }
        }
        else
        {
            player_collider2D = GameObject.Find("Player").GetComponent<BoxCollider2D>();
        }

    }


}
