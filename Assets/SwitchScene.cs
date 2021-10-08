using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("saiu do game");
    }
}
