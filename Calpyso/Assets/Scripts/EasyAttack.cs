using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyAttack : MonoBehaviour
{

   [SerializeField]private float attackrange,attackspeed;
   [SerializeField]private int damage;
   [SerializeField]private Animator anim;
   [SerializeField][Header("Düşman biziz")]private LayerMask enemylayer;
   private float currentatspeed;
   private void Start() {
    currentatspeed=attackspeed;
   }
    void Update()
    {
        if(GetComponent<Health>().DieProperty==false&&GetComponent<Health>().underattackProperty==false)
        Attackarea();
    }
    void Attackarea()
    {
       if (CheckTheEnemy()!=null)
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
    private void OnDrawGizmos() {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position,attackrange);
    }
}
