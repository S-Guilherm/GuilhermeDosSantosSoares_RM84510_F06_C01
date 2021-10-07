using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Optimize : MonoBehaviour
{
   

    // Qualidade de exibição
    void Update()
    {
        switch (Input.inputString)
        {
            case "1":
                //baixissima
                QualitySettings.SetQualityLevel(0, true);
                break;

            case "2":
                //baixa
                QualitySettings.SetQualityLevel(1, true);
                break;

            case "3":
                //normal
                QualitySettings.SetQualityLevel(2, true);
                break;

            case "4":
                //media
                QualitySettings.SetQualityLevel(3, true);
                break;

            case "5":
                //alta
                QualitySettings.SetQualityLevel(4, true);
                break;

            case "6":
                //epica
                QualitySettings.SetQualityLevel(5, true);
                break;

            default:
                Debug.Log("Nao ha qualidade nesta tecla");
                break;
        }

    }
}
