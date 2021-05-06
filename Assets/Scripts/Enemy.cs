using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    [Header("General")]
    public float health;
    public string enemyName;
    public float baseAttack;
    public float moveSpeed;

    [Header("PathFindig")]
    public Transform target;
    public float activateDistance = 50f;
    public float nexWaypointDistance = 2f;
    public bool pathfindingEnabled = true;
    public float pathUpdateSeconds = 0.5f;

    [Header("Behaivour")]
    public bool patrol = true;
    public float patrolDistance = 4f;
    public float startWaitTime;
    public float maxWaitTime;
    private float waitTime;

    private float attackRange = 1f;

    Path path;
    int currentWaypoint = 0;
    bool playerTracking = false;
    Vector2 homePosition;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;
    Animator anim;


    public GameObject arrow;
    public GameObject coin;
    public GameObject potion;

    public bool imAttacking = false;
    Vector2 direction = Vector2.zero;
    Vector2 area = Vector2.zero;

    float attackTime=0;
    float cooldown =2.0f;

    // Start is called before the first frame update
    void Start()
    {
        homePosition = transform.position;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);

        waitTime = Random.Range(startWaitTime, maxWaitTime);
    }

    // Update is called once per frame

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.transform.position) <= attackRange * 1.5f && !imAttacking)
        {
            Debug.Log("IM ATTACKING!!");
            attack(target.transform.position - transform.position);
            
        }


        if (imAttacking)
        {
            checkAttackFinished();
        }
    }
    void FixedUpdate()
    {
        
            
            if (TargetInDistance() && pathfindingEnabled && !imAttacking)
            {
                if (!playerTracking)
                {
                    target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
                    playerTracking = true;
                    anim.SetBool("detectado", playerTracking);
                }

                PathFollow();

            }


            // Volver a la posicion inicial
            if (!TargetInDistance() && pathfindingEnabled && !imAttacking)
            {
                if (playerTracking)
                {
                    playerTracking = false;
                    seeker.StartPath(rb.position, homePosition, OnPathComplete);
                }

                PathFollow();

                if (reachedEndOfPath)
                {
                    rb.velocity = new Vector2();
                    anim.SetBool("detectado", playerTracking);
                    waitTime -= Time.deltaTime;
                }

                if (patrol && waitTime <= 0)
                {
                    seeker.StartPath(rb.position,
                        new Vector2(Random.Range(rb.position.x - patrolDistance, rb.position.x + patrolDistance),
                        Random.Range(rb.position.y - patrolDistance, rb.position.y + patrolDistance)), OnPathComplete);
                    waitTime = Random.Range(startWaitTime, maxWaitTime);
                    anim.SetBool("detectado", true);
                }

            }


        
    }

    void UpdatePath()
    {
        if(pathfindingEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void PathFollow()
    {
        if(path == null)
        {
            reachedEndOfPath = true;
            return;
        }

        //llega al final del camino
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * moveSpeed; // * Time.deltaTime

        // Movimiento

        //rb.AddForce(force);
        rb.velocity = force;


        //Siguiente Movimiento
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nexWaypointDistance)
        {
            currentWaypoint++;
        }

        //Direccion de los sprite
        anim.speed = 1;
        anim.SetFloat("moveX", direction.x);
        anim.SetFloat("moveY", direction.y);
    }

    bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <=0)
        {
            die();
        }
    }
    void die()
    {
        anim.SetTrigger("die");
        SoundManager.PlaySound("enemyDeath");
        int item = Random.Range(1, 3);
        switch (item)
        {
            case 1: //Monedas
                Instantiate(coin, this.transform.position, Quaternion.identity);

                break;
            case 2: //Flechas
                Instantiate(arrow, this.transform.position, Quaternion.identity);
                break;
            case 3: //Pociones
                Instantiate(potion, this.transform.position, Quaternion.identity);
                break;
        }
        Destroy(this.gameObject,0.3f);


    }


    void attack(Vector3 vect)
    {
        Debug.Log("TIMER: " + (Time.time - attackTime));
        if(!imAttacking && cooldown<= Time.deltaTime-attackTime){
            imAttacking = true;

            direction = new Vector2(vect.x, vect.y).normalized;

            anim.SetTrigger("Attack");

            anim.SetFloat("DirX", direction.x);
            anim.SetFloat("DirY", direction.y);
            int damage = 1;
            area = new Vector2(this.transform.position.x + (direction.x * attackRange), this.transform.position.y + (direction.y * attackRange));
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(area, 0.5f);

            foreach (Collider2D player in hitPlayer)
            {
                if (player.TryGetComponent<PlayerManagement>(out PlayerManagement componente))
                {
                    componente.takeDamage(damage);
                    SoundManager.PlaySound("enemyAttack");
                }
            }

            attackTime = Time.time;

        }
        
        


    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(new Vector3(area.x, area.y, 1), 0.5f);
    }




    void checkAttackFinished()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("goblin_attack"))
        {
            Debug.Log("Setting False");
            imAttacking = false;
        }
            
        else
        {
            Debug.Log("MUAHAHAHAHAHA");
        }
    }




}
