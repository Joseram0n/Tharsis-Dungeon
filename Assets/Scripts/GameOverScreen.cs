using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreTXT;
    public GameObject UIcoso;

    void Start()
    {
        gameObject.SetActive(true);
        UIcoso.SetActive(false);
        Debug.Log("MI PAPA: " + UIcoso.transform.parent.gameObject.activeInHierarchy);
    }
    public void Setup(int monedas, int kills)
    {
        //gameObject.SetActive(true);
        UIcoso.SetActive(true);
        Time.timeScale = 0f;
        scoreTXT.text = "Coins: " + monedas.ToString() + "\nKills: " + kills.ToString();
    }


    public void ExitMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Principal");
    }



    // Start is called before the first frame update
    

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
