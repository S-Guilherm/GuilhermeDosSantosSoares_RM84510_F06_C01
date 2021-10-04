using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AIController : MonoBehaviour {

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
        Wait,
        Patrol,
        Follow,
        Search,
        Attack
    }

    private States currentState;

    [Header("State: Wait")]
    public float waitTime = 2f;
    private float waitingTime;

    [Header("State: Patrol")]
    public float minimumDistanceWaypoint = 1f;
    public float distanceWayPointAtual;

    //teste
    public Transform[] wayPoints;
    private int wayPointAtual;

    //teste

    [Header("State: Follow")]
    private GameObject Player;
    private float fieldVision = 5f;
    private float distancePlayer;

    [Header("State: Search")]
    public float searchTime = 6f;
    private float noVisionTime;

    [Header("State: Attacar")]
    public static float damageEnemy = 20f;
    private bool podeAtacar;
    

    //...


    void Start()
    {

        //teste

        wayPointAtual = Random.Range(0, wayPoints.Length);

        //teste

        Wait();

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

        if(currentState != States.Follow && haveVisionPlayer())
        {
            Follow();

            return;
        }
        /*teste 
        if(distancePlayer <= distanceAttack)
        {
            Attack();

            return;
        }
        */


        switch (currentState)
        {
            case States.Wait:

                if (WaitedTime())
                {
                    Patrol();
                }
                else
                {
                    alvo = transform;
                }


                break;
            case States.Patrol:

                if (PertoWayPointAtual())
                {
                    Wait();

                    AlterarWayPoint();
                }
                else
                {

                    alvo = wayPoints[wayPointAtual];
                }

                break;
            case States.Follow:

                if (!haveVisionPlayer())
                {
                    Search();
                }
                else
                {
                    alvo = Player.transform;
                }
                
                break;
            case States.Search:

                if (noVisionLongTime())
                {
                    Wait();
                }

                break;

                /* teste ataque
            case States.Search:

                /* if (cronometroAttack >= timeByAttack && distancePlayer <= distanceAttack)
                 {

                     atacandoAlgo = true;
                     cronometroAttack = 0;

                     LifePlayer.Life = LifePlayer.Life - damageEnemy;
                     Debug.Log("Recebeu ataque");
                 }
                 else if(cronometroAttack >= timeByAttack && distancePlayer > distanceAttack)
                 {
                     atacandoAlgo = false;
                     cronometroAttack = 0;

                     Debug.Log("Errou ataque");
                 }

                 
                break;
                */


        }

        aiCharacterControl.target = alvo;
    }

    //metodos esperar
    #region Esperar
    private void Wait()
    {
        currentState = States.Wait;
        waitingTime = Time.time;
    }


    // metodos do estado esperar
    private bool WaitedTime()
    {
        return waitingTime + waitTime <= Time.time;
    }

    #endregion Esperar

    //...


    //metodos patrulhar
    #region Patrulhar
    private void Patrol()
    {
        currentState = States.Patrol;
    }
    //...


    //metodos do estado patrulhar

    private bool PertoWayPointAtual()
    {

        //distanceWayPointAtual = Vector3.Distance(transform.position, waypointAtual.position);

        //teste

        distanceWayPointAtual = Vector3.Distance(wayPoints[wayPointAtual].transform.position, transform.position);

        //teste
        return distanceWayPointAtual <= minimumDistanceWaypoint;
    }

    private void AlterarWayPoint()
    {
    
        if(distanceWayPointAtual <= 2)
        {
            wayPointAtual = Random.Range(0, wayPoints.Length);
        }

        //waypointAtual = (waypointAtual == wayPont1)  ? wayPont2 : wayPont1;

                
    }

    #endregion Patrulhar

    //...

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

    //metodos atacar
    /*
        #region atacar
    
    private void Attack()
    {
        if(atacandoAlgo == true)
        {
            cronometroAttack += Time.deltaTime;
        }
    }

    */




    public void Attacar()
    {
        if(podeAtacar == true)
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
