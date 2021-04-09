using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void EmpezarJuego()
    {
        SceneManager.LoadScene(1);
    }
    public void SalirJuego()
    {
        Debug.Log("Quitting");
        Application.Quit();

    }
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
