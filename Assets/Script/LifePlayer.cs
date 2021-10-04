using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LifePlayer : MonoBehaviour {

	public float Life = 100f;
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
        
        currentLife = currentLife - AIController.damageEnemy;
        //AIController.damageEnemy = AIController.damageEnemy - currentLife;
        healthBar.SetHealth(currentLife);
        //currentLife = Mathf.FloorToInt(Time.deltaTime);
        


    }

    public void danoArmadilha()
    {
        currentLife = currentLife - DamageTrap.trapDamage;
        healthBar.SetHealth(currentLife);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
           
            Damage();
        }
        else
        {
            danoArmadilha();
        }
    }

    



    void Update()
    {



        if (currentLife <= 0)
        {
            currentLife = 0;

            Death();
        }
               
    }

    

    void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
