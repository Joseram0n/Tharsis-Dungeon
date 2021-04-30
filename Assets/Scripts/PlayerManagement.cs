using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Canvas canvas;
    // Start is called before the first frame update


    private Vector2 direction;
    private Vector2 AttackDir;
    private Animator animator;

    private SpriteRenderer spriterenderer;
    public Sprite arco;
    public Sprite sword;
    private bool isSword = true;
    private bool imAttacking = false;

    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    private Transform attackPoint;
    public float multiplier;
    public int damage = 20;

    public GameObject arrow;
    public float projectileForce = 5f;
    private Rigidbody2D rb;
    private bool shot = false;
    public float fireTime = 0.8f;
    private float fireTimer;

    private PlayerMovement pm;

    public int flechas;
    public int monedas;
    public int pociones;

    DisplayManagement display;

    public int enemigosMatados = 0;

    private static PlayerManagement Instance;


    void Start()
    {

        /*
        //Instanciacion
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }

        */



        //Vida, items
        currentHealth = maxHealth;


        //Movimiento
        animator = GetComponent<Animator>();
        AttackDir = Vector2.zero;
        pm = GetComponent<PlayerMovement>();

        //Cambiar de Sprite
        spriterenderer = this.GetComponent<SpriteRenderer>();
        spriterenderer.sprite = sword;

        //Punto de ataque de la espada
        GameObject newGameObject = new GameObject();
        attackPoint = newGameObject.transform;

        display = canvas.GetComponent<DisplayManagement>();
        display.setMaxHealth(maxHealth);
        display.setAmmo(flechas);
        display.setGold(monedas);
        display.setPots(pociones);

    }

    // Update is called once per frame
    void Update()
    {
        if (!CheckAttack())
        {
            TakeInput();
        }
        else
        {
            direction = Vector2.zero;
        }

        if(shot)
        {
            if (fireTimer < Time.time - fireTime)
                Fire();
        }


    }
    private void FixedUpdate()
    {
        pm.Move(direction);
    }

    private void TakeInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveX, moveY);

        if (direction.x != 0 || direction.y != 0)
        {
            AttackDir = direction;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.E))
            changeSprite();
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
        if (Input.GetKeyDown(KeyCode.H))
            heal();
    }

    public void changeSprite()
    {
        if (isSword)
        {
            this.GetComponent<SpriteRenderer>().sprite = arco;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = sword;
        }
        animator.SetBool("hasSword", !animator.GetBool("hasSword"));
    }

    public void Attack()
    {
        if (animator.GetBool("hasSword"))
        {
            attackPoint.position = new Vector3(this.transform.position.x + (AttackDir.x * multiplier), this.transform.position.y + (AttackDir.y * multiplier), 0);
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.TryGetComponent<Enemy>(out Enemy componente))
                    componente.TakeDamage(damage);
            }
            imAttacking = true;
            animator.SetTrigger("Attack");
            SoundManager.PlaySound("slash");
        }
        else
        {
            if (flechas > 0)
            {
                flechas--;
                display.setAmmo(flechas);
                fireTimer = Time.time;
                shot = true;
                imAttacking = true;
                animator.SetTrigger("Attack");
                SoundManager.PlaySound("shoot");
            }

        }



    }

    public void Fire()
    {
        shot = false;
        float angulo = 0;
        if (AttackDir.x != 0)
            angulo = Mathf.Acos(AttackDir.x);
        else
            angulo = Mathf.Asin(AttackDir.y);
        angulo = angulo * 180 / Mathf.PI;
        GameObject arrow_thrown = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, angulo));
        arrow_thrown.GetComponent<Rigidbody2D>().velocity = AttackDir * projectileForce;
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



    public void takeDamage(int damage)
    {
        Debug.Log("IM HIT");
        currentHealth -= damage; //Si llega a 0, muere
        display.setHealth(currentHealth);
        SoundManager.PlaySound("playerHit");
    }
    public void heal()
    {
        if(pociones>0)
        {
            pociones--;
            display.setPots(pociones);
            currentHealth = maxHealth;
            display.setHealth(maxHealth);
        }
        
    }

    public bool CheckAttack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Bow"))
            imAttacking = false;
        return imAttacking;
    }


    public void GainLoot(string nombre)
    {
        int cantidad = 0;
        switch (nombre)
        {
            case "Moneda(Clone)":
                cantidad = Random.Range(1, 10);
                monedas += cantidad;
                display.setGold(monedas);

                break;
            case "Poción(Clone)":
                cantidad = 1;
                pociones += cantidad;
                display.setPots(pociones);

                break;
            case "Flecha(Clone)":
                cantidad = Random.Range(3, 8);
                flechas += cantidad;
                display.setAmmo(flechas);

                break;
        }
        SoundManager.PlaySound("pickUp");
        enemigosMatados++;
    }






}
