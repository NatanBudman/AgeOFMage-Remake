using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void LoadScene(string SceneName) 
    {
        SceneManager.LoadScene($"Scenes/{SceneName}");
    }
    public void EnablePanel(GameObject panel) 
    {
        panel.SetActive(true);
    }
    public void DisablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
    public void QuitGame() 
    {
        Application.Quit();
    }
}
