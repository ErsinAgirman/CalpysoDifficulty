using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Difficulty;

public class DifficultyControl : MonoBehaviour
{
    Difficultys mydif;
    [SerializeField]private EasyAttack easyAttack; 
    [SerializeField]private NormalAttack normalAttack; 
    [SerializeField]private HardAttack hardAttack; 
   private void Start() {
    InvokeRepeating("SetDif",0f,1f);
   }
    void SetDif()
    {
       mydif=GameManager.Instance.GetDiffuclty;

        if (mydif==Difficultys.Easy)
        {
           easyAttack.enabled=true;
            normalAttack.enabled=false;
            hardAttack.enabled=false;
        }
        else if (mydif==Difficultys.Medium)
        {
           easyAttack.enabled=false;
            normalAttack.enabled=true;
            hardAttack.enabled=false;
        }
        else
        {
            easyAttack.enabled=false;
            normalAttack.enabled=false;
            hardAttack.enabled=true;
        }
    }
}
