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
    public float projectileForce = 2f;
    private Rigidbody2D rb;

    private PlayerMovement pm;




    void Start()
    {
        //Vida, items
        currentHealth = maxHealth;
        canvas.GetComponent<DisplayManagement>().setMaxHealth(maxHealth);

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
        Destroy(newGameObject);


        
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
        Debug.Log("X: " + direction.x + "Y: "+direction.y);

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
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyRecieveDamage>().DealDamage(damage);
            }
        }
        else
        {
            float angulo = 0;
            if (AttackDir.x != 0)
                angulo = Mathf.Acos(AttackDir.x);
            else
                angulo = Mathf.Asin(AttackDir.y);
            angulo = angulo * 180 / Mathf.PI;
            GameObject arrow_thrown = Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, angulo));
            arrow_thrown.GetComponent<Rigidbody2D>().velocity = AttackDir * projectileForce;
        }

        imAttacking = true;
        animator.SetTrigger("Attack");

    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }



    public void takeDamage(int damage)
    {
        currentHealth-=damage; //Si llega a 0, muere
        canvas.GetComponent<DisplayManagement>().setHealth(currentHealth);
    }
    public void heal()
    {
        currentHealth = maxHealth;
        canvas.GetComponent<DisplayManagement>().setHealth(maxHealth);
    }

    public bool CheckAttack()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword") && !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Bow"))
            imAttacking = false;
        return imAttacking;
    }
















}
