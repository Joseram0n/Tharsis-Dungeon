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
    public Transform attackPoint;
    public float multiplier = 0.2f;
    public int damage = 20;

    public GameObject arrow;
    public float projectileForce = 2f;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AttackDir = Vector2.zero;
        spriterenderer = this.GetComponent<SpriteRenderer>();
        spriterenderer.sprite = sword;
    }

    // Update is called once per frame
    void Update()
    {
        if(!CheckAttack())
        {
            TakeInput();
            Move();
        } 
    }

    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (direction.x != 0 || direction.y != 0)
            SetAnimatorMovement(direction);
        else
        {
            if(isSword)
            {
                animator.SetLayerWeight(1, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);
            }
            else
            {
                animator.SetLayerWeight(1, 1);
                animator.SetLayerWeight(0, 0);
                animator.SetLayerWeight(2, 0);
                animator.SetLayerWeight(3, 0);

            }
        }
    }
    private void TakeInput()
    {
        direction = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector2.right;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector2.down;
        }
        if (direction.x != 0 || direction.y != 0)
            AttackDir = direction;

        if (Input.GetKeyDown(KeyCode.E))
            changeSprite();
        if (Input.GetKeyDown(KeyCode.Space))
            Attack();
    }
    
    private void SetAnimatorMovement(Vector2 direction)
    {
        if (isSword)
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 1);
            animator.SetLayerWeight(3, 0);
        }
        else
        {
            animator.SetLayerWeight(0, 0);
            animator.SetLayerWeight(1, 0);
            animator.SetLayerWeight(2, 0);
            animator.SetLayerWeight(3, 1);
        }
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
        isSword = !isSword;
    }

    public void Attack()
    {
        imAttacking = true;
        if(isSword)
        {
            attackPoint.position= new Vector2(this.transform.position.x+AttackDir.x*multiplier, this.transform.position.y+AttackDir.y*multiplier);
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
        animator.SetTrigger("Attack");
        
    }
    public bool CheckAttack()
    {
        for (int i = 0; i < 4; i++)
            if (animator.GetCurrentAnimatorStateInfo(i).IsName("Estado_Transicion"))
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
