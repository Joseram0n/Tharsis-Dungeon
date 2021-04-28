using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
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
  


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        AttackDir = Vector2.zero;
        spriterenderer = this.GetComponent<SpriteRenderer>();
        spriterenderer.sprite = sword;
        GameObject newGameObject = new GameObject();
        attackPoint = newGameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if(!CheckAttack())
        {
            TakeInput();
        }
        else
        {
            direction = Vector2.zero;
        }
    }
    
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);
        if (direction.x != 0 || direction.y != 0)
            SetAnimatorMovement(direction);
        else
            SetAnimatorMovement(AttackDir);
    }
    private void TakeInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        direction = new Vector2(moveX,moveY);
        
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
    
    private void SetAnimatorMovement(Vector2 direction)
    {

        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
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
        if(animator.GetBool("hasSword"))
        {
            attackPoint.position= new Vector3(this.transform.position.x + (AttackDir.x * multiplier), this.transform.position.y + (AttackDir.y * multiplier),0);
            Collider2D[] hitEnemies=Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            foreach(Collider2D enemy in hitEnemies)
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
    public bool CheckAttack()
    {
       if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Sword")&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Attack_Bow"))
            imAttacking = false;
        return imAttacking;
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
