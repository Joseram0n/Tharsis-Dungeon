using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    Resolution[] resolutions;
    public Dropdown resolutionsDropdown;
    // Start is called before the first frame update
    private void Start()
    {
        resolutions=Screen.resolutions;
        resolutionsDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for(int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }
        resolutionsDropdown.AddOptions(options);
        resolutionsDropdown.value = currentResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
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



    public void setVariables()
    {
        GlobalVariables.Set("Volume", settingsPanel.GetComponent<Slider>().value);
    }
}
