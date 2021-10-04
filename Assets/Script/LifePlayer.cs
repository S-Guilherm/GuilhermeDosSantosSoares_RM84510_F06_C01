using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LifePlayer : MonoBehaviour {

	public float Life = 100;
    public float currentLife;
    private GameObject playerLife;
    private LifePlayer lifePlayer;
   
    public HealthBar healthBar;
    
	
    void Start()
    {
       transform.tag = "Player";

        currentLife = Life;
        healthBar.SetMaxhealth(Life);

    }
    
    public void Damage()
    {

        currentLife =  currentLife - AIController.damageEnemy;
        //AIController.damageEnemy = AIController.damageEnemy - currentLife;
        healthBar.SetHealth(currentLife);
        //currentLife = Mathf.FloorToInt(Time.deltaTime);
        


    }

    
    

   

    void Update()
    {


        Damage();


        if (Life <= 0)
        {
            Life = 0;

            Death();
        }
               
    }

    

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
