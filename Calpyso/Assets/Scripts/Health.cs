using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Animator anim;
    
    [SerializeField]private ObjectType objectType;
    [SerializeField]private GameObject bloodeffect;
    public float maxHealth = 100;
    [SerializeField]private float currentHealth;
    bool die,underattack;
    [SerializeField]private float firsthit,lasthit;
    bool oneshot;
    public bool block;
    [SerializeField]private Image healthbar;

    void Start()
    {   
        currentHealth = maxHealth;
        lasthit=Time.time;
        firsthit=lasthit;
    }
    public float HealthProperty{
        get{return currentHealth;}
        set{currentHealth=value;}
    }
   public bool DieProperty{
        get{return die;}
        set{die=value;}
    }
     public bool underattackProperty{
        get{return underattack;}
        set{underattack=value;}
    }
    private  void Block()
    {
        block=true;   
        Invoke(nameof(resetBlock), GameManager.Instance.GetClipLength("EjoBlock",anim));
    }
    private void resetBlock()
    {
        block=false;
    }
    
  
    public void TakeDamage(int damage,Collider2D hitcol)
    {
         
        if(block&&hitcol.GetComponent<Health>()!=null)
        hitcol.GetComponent<Health>().Changepoint(3);

         if(die||block)
         return;

         currentHealth -= damage;
         if(healthbar != null)
         {
           healthbar.fillAmount = Mathf.Clamp01(currentHealth / maxHealth);
         }
        
        GetHit();
      
        Debug.Log(objectType+" hedefinin Canı : " + currentHealth);
        if (currentHealth <= 0)
        {
            anim.SetTrigger("die");
             die=true;

             if (objectType == ObjectType.player)
            {
                CharacterDie();
            }
        }else
        {
           GameObject bld=Instantiate(bloodeffect,transform.position,Quaternion.identity);
           Destroy(bld,0.3f);
           Invoke("resetUnderattack",0.2f);
            underattack=true;
        }
    }
    private void GetHit()
    {
         
         lasthit=Time.time;

        if(oneshot==true)
        return;

        firsthit=Time.time;
        oneshot=true;
    }
    private void resetUnderattack()
    {
      underattack=false;
    }

    public void Die()
    {
        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GameManager.Instance.DifficultySetting(lasthit-firsthit);
       Destroy(gameObject.transform.parent.gameObject);
    }
    public void CharacterDie()
    {
        anim.SetTrigger("isDie");
         SceneManager.LoadScene(0);
    }
    public void Changepoint(float changetime)
    {
        //düşülen süre
      lasthit-=changetime;
    }
    [Serializable]
    public enum ObjectType
    {
        player,
        enemy
    }
}
