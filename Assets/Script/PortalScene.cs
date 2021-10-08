using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalScene : MonoBehaviour
{

    public Text texto;
    GameObject player;

    private void Start()
    {
        texto.enabled = false;
      
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            texto.enabled = true;
            WinScene();
        }
        else
        {
            texto.enabled = false;
        }
       
    }

    private void WinScene()
    {
        StartCoroutine("TextoTela");
        
    }

    IEnumerator TextoTela()
    {
       
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2);
    }
}
