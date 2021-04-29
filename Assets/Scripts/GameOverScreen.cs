using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreTXT;


    void Setup(int kills,int coins)
    {
        gameObject.SetActive(true);
        scoreTXT.text = "Coins: " + coins.ToString() + "\nKills: " + kills.ToString();
    }


    public void ExitMenu()
    {
        SceneManager.LoadScene("Menu Principal");
    }



    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
