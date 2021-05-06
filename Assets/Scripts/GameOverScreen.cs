using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScreen : MonoBehaviour
{

    public TextMeshProUGUI scoreTXT;
    public GameObject UIcoso;
    public TextMeshProUGUI entername;
    public TMP_InputField field;
    int coins;
    int kills;

    void Start()
    {
        gameObject.SetActive(true);
        UIcoso.SetActive(false);
        Debug.Log("MI PAPA: " + UIcoso.transform.parent.gameObject.activeInHierarchy);
    }
    public void Setup(int monedas, int kills)
    {
        coins = monedas;
        this.kills = kills;
        //gameObject.SetActive(true);
        UIcoso.SetActive(true);
        Time.timeScale = 0f;
        scoreTXT.text = "Coins: " + monedas.ToString() + "\nKills: " + kills.ToString();
    }


    public void ExitMenu()
    {
        string cadena = field.text;
        if (cadena!=null)
        {
            entername.text = "";
            string nombre = cadena;
            PlayerScore score = new PlayerScore(coins, kills, nombre);
            SaveSystem.SavePlayer(score);
            Time.timeScale = 1f;
            SceneManager.LoadScene("Menu Principal");
        }
        else
        {
            entername.text = "¡Debe ingresar un nombre primero!";
        }
        
    }



    // Start is called before the first frame update
    

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
