using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardAttack : MonoBehaviour
{
    [SerializeField]private float attackrange,attackspeed,maxheal,saferange,bulletrange,bulletspeed,bulletdestryotime;
    [SerializeField]private GameObject bullet;
    [SerializeField]private Transform safepoint,attackpoint;
    [SerializeField]private bool canescape=true;
   [SerializeField]private int damage,escapeheal;
   [SerializeField]private Animator anim;
   [SerializeField][Header("Düşman biziz")]private LayerMask enemylayer;
   private float currentatspeed;
   private void Start() {
    currentatspeed=attackspeed;
    
   }
    void Update()
    {
        if(GetComponent<Health>().DieProperty==false&&GetComponent<Health>().underattackProperty==false)
        {
           Attackarea();
          HealArea();
        }
       
    }
    void Attackarea()
    {


       if (CheckTheEnemy()!=null&&canescape)
       {
          if (attackspeed>0) 
            attackspeed-=Time.deltaTime;     
          else
            {
                CheckTheEnemy().gameObject.GetComponent<Health>().TakeDamage(damage,gameObject.GetComponent<Collider2D>());
                 anim.SetTrigger("attack");      
                attackspeed=currentatspeed;
               
            }
       }
       else if (CheckTheRange()!=null&&canescape)
       {
          if (attackspeed>0) 
            attackspeed-=Time.deltaTime;     
          else
            {         
                 anim.SetTrigger("rangeattack");      
                attackspeed=currentatspeed;             
            }
       }
    }
    private void SpawnBullet()
    {
        GameObject blt=Instantiate(bullet,attackpoint.position,Quaternion.identity);
        Destroy(blt,bulletdestryotime);
       if (GetComponent<EnemyAI>().isGoingRight)
       {
         blt.GetComponent<Rigidbody2D>().velocity=new Vector2(bulletspeed,0);
       }
       else
         blt.GetComponent<Rigidbody2D>().velocity=new Vector2(-bulletspeed,0);
    }

    void HealArea()
    {
        if (GetComponent<Health>().HealthProperty<escapeheal&&canescape)
        {
            CalculateSafePoint();
            GetComponent<EnemyAI>().targetProperty=safepoint;
            canescape=false;
            GetComponent<EnemyAI>().escaping=true;
        }
        if (canescape==false&&GetComponent<Health>().HealthProperty<escapeheal)
        {
             if (2f>=Vector2.Distance(transform.position,safepoint.position))
             {
                Debug.Log("can bastı");
                GetComponent<Health>().HealthProperty=maxheal;
                ResetEscape();
             }
        }
        if(GetComponent<Health>().HealthProperty>escapeheal)
         GetComponent<EnemyAI>().targetProperty=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void ResetEscape()
    {    
        canescape=true;
         GetComponent<EnemyAI>().escaping=false;
    }
    void CalculateSafePoint()
    {
          Transform player=GameObject.FindGameObjectWithTag("Player").transform;
          Vector2 delta=new Vector2(player.position.x+Random.Range(Random.Range(-saferange,-(saferange-10)),Random.Range(saferange-10,saferange)),safepoint.position.y);
          safepoint.transform.position=delta;
    }
    Collider2D CheckTheEnemy()
    {
        Collider2D[] enemys=Physics2D.OverlapCircleAll(transform.position,attackrange,enemylayer);
        foreach (Collider2D enmy in enemys)
        {
            return enmy;
        }
       
       return null;
    }
     Collider2D CheckTheRange()
    {
        Collider2D[] enemys=Physics2D.OverlapCircleAll(transform.position,bulletrange,enemylayer);
        foreach (Collider2D enmy in enemys)
        {
            return enmy;
        }
       
       return null;
    }
    private void OnDrawGizmos() {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position,attackrange);
          Gizmos.DrawWireSphere(transform.position,bulletrange);
    }
}
