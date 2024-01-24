using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform position1Transform;
    public Transform position2Transform;
    public float horizontalSpeed;
    public float distance;
    public float stopDistance; //  durma mesafesi
    public float followspeed;

    private Transform target;
    private Rigidbody2D rb;
    private Vector2 position1;
    private Vector2 position2;
    private bool isFollowing = false;
    public bool isGoingRight = true;
    public Animator anim;
    public bool escaping;

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        anim = GetComponent<Animator>();

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();

        // Başlangıçta pos1 ve pos2'yi düşmanın başlangıç pozisyonuna ayarla
        position1 = position1Transform.position;
        position2 = position2Transform.position;
    }
    public Transform targetProperty{
        get{return target;}
        set{target=value;}
    }

    void Update()
    {
        if(GetComponent<Health>().DieProperty==false&&GetComponent<Health>().underattackProperty==false)
        EnemyAi();
    }

   void EnemyMove()
{
    float moveDirection = isGoingRight ? 1 : -1;
    rb.velocity = new Vector2(moveDirection * horizontalSpeed, rb.velocity.y);

    float currentPositionX = transform.position.x;
    float targetPositionX = isGoingRight ? position2.x : position1.x;

    if ((isGoingRight && currentPositionX >= targetPositionX) || (!isGoingRight && currentPositionX <= targetPositionX))
    {
        isGoingRight = !isGoingRight;
        Flip();
    }
    anim.SetBool("isRunning", rb.velocity.x != 0);
}

void EnemyAi()
{
    float distanceToPlayer = Vector2.Distance(transform.position, target.position);

    if (distanceToPlayer < distance||escaping)
    {
        isFollowing = true;

        if (distanceToPlayer > stopDistance)
        {
            anim.SetBool("isRunning",true);
            EnemyFollow();
        }
        else
        {
            anim.SetBool("isRunning",false);
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Yanınıza gelindiğinde hangi yöne gideceğini kontrol et
        float playerDirection = target.position.x - transform.position.x;

        if (playerDirection > 0 && !isGoingRight)
        {
            isGoingRight = true;
            Flip();
        }
        else if (playerDirection < 0 && isGoingRight)
        {
            isGoingRight = false;
            Flip();
        }
    }
    else
    {
        isFollowing = false;
        anim.SetBool("isRunning", false);
        EnemyMove();
    }
}

void EnemyFollow()
{
    Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);
    transform.position = Vector2.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
}

void Flip()
{
    Vector3 localScale = transform.localScale;
    localScale.x *= -1;
    transform.localScale = localScale;
}

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        Gizmos.DrawLine(position1, position2);
    }
}
