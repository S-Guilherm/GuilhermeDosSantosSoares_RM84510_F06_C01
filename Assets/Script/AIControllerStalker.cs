using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class AIControllerStalker : MonoBehaviour
{

    //...

    // movimentaçãp ai
    private AICharacterControl aiCharacterControl;
    private Transform alvo;

    private void Awake()
    {
        aiCharacterControl = GetComponent<AICharacterControl>();
    }


    //...

    public enum States
    {
        Follow,
        Search,
        Attack
    }

    private States currentState;

 

    [Header("State: Follow")]
    private GameObject Player;
    private float fieldVision = 5f;
    private float distancePlayer;

    [Header("State: Search")]
    public float searchTime = 6f;
    private float noVisionTime;

    [Header("State: Attack")]
    public static float damageEnemy = 100f;
    private bool podeAtacar;


    //...


    void Start()
    {

        

        

        Player = GameObject.FindGameObjectWithTag("Player");

        podeAtacar = true;
    }


    void Update()
    {
        CheckStates();

        if (Vector3.Distance(transform.position, Player.transform.position) < 1.5f)
        {
            Attacar();
        }
    }


    

    private void CheckStates()
    {

        if (currentState != States.Follow && haveVisionPlayer())
        {
            Follow();

            return;
        }
       

        switch (currentState)
        {
            
            case States.Follow:

                if (!haveVisionPlayer())
                {
                    Follow();
                }
                else
                {
                    alvo = Player.transform;
                }

                break;
                              
        }

        aiCharacterControl.target = alvo;
    }


    private void OnCollisionEnter(Collision colision)
    {

        if(colision.collider.name == "Player")
        {
            SceneManager.LoadScene(0);
        }

    }


    // metodos perseguir
    #region Perseguir

    private void Follow()
    {
        currentState = States.Follow;
    }

    private bool haveVisionPlayer()
    {
        distancePlayer = Vector3.Distance(
            transform.position, Player.transform.position);

        return distancePlayer <= fieldVision;
    }

    #endregion Perseguir

    //...

    //metodos procurar
    #region Procurar

    private void Search()
    {
        currentState = States.Search;

        noVisionTime = Time.time;

        alvo = null;
    }

    private bool noVisionLongTime()
    {
        return noVisionTime + searchTime <= Time.time;
    }

    #endregion Procurar

    //...

    //metodos atacar
    #region Atacar

    void Attacar()
    {
        if (podeAtacar == true)
        {
            StartCoroutine("TempoDeAtaque");
            Player.GetComponent<LifePlayer>().currentLife -= damageEnemy;
        }
    }
    IEnumerator TempoDeAtaque()
    {
        podeAtacar = false;
        yield return new WaitForSeconds(1);
        podeAtacar = true;


    }


    #endregion atacar

    //...
}

