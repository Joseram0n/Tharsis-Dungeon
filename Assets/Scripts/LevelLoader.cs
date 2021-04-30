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
    
    void LoadDungeonLevel()
    {
        StartCoroutine(LoadLevel(numeroEscenaDestino));
    }

     IEnumerator LoadLevel(int nivel)
     {
        crossfade.SetActive(true);
        crossfade.GetComponent<Animator>().SetTrigger("End");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nivel);
     }



    // Start is called before the first frame update
    void Start()
    {
        crossfade.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(gateDungeon.IsTouching(player_collider2D))
        {
            LoadDungeonLevel();
        }
    }

}
