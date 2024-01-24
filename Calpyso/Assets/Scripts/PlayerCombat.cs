using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask enemylayers;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    
    [SerializeField]private Animator anim;
    [SerializeField]private bool canattack=true,canblock=true;
    

   private void Update() {

    if (GetComponent<Health>().DieProperty==false)
    {
        AttackArea();
        DefenseArea();
    }  
   }
    void AttackArea()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)&&canattack)
        {
            anim.SetTrigger("isAttack");
            canattack=false;
             Invoke(nameof(ResetAttack),GameManager.Instance.GetClipLength("EjoAxeAttack",anim));  

            if(CheckTheEnemy()!=null)
            CheckTheEnemy().GetComponent<Health>().TakeDamage(attackDamage,gameObject.GetComponent<Collider2D>());     
        }
    }
    void DefenseArea()
    {
         if (Input.GetKeyDown(KeyCode.Mouse1)&&canblock)
        {
            anim.SetTrigger("block");
            canblock=false;    
            Invoke(nameof(ResetBlock),GameManager.Instance.GetClipLength("EjoBlock",anim));    
        }
    }
   
    private void ResetAttack()
    {
       canattack=true;
    }
    private void ResetBlock()
    {
      canblock=true;
    }
    private void OnDrawGizmosSelected() 
    {
        if (attackPoint==null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    Collider2D CheckTheEnemy()
    {
        Collider2D[] enemys=Physics2D.OverlapCircleAll(transform.position,attackRange,enemylayers);
        foreach (Collider2D enmy in enemys)
        {
            return enmy;
        }
        return null;
    }
}
