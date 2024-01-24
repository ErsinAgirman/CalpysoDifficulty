using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Difficulty;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;
   [SerializeField]private TextMeshProUGUI diftext;
   [SerializeField]private Difficultys difficulty;
   
   

   private void Awake() {
    if(Instance==null)
    Instance=this;
    }
    private void Start() {
         diftext.text="Player Rank: "+difficulty;
          diftext.color=Color.green;
    }
     public float GetClipLength(string triggerName,Animator anim)
    {
        RuntimeAnimatorController ac = anim.runtimeAnimatorController;

        foreach (AnimationClip clip in ac.animationClips)
        {
            if (clip.name.Equals(triggerName))
            {
                return clip.length;
            }
        }
        return 0f;
    }
    public void DifficultySetting(float deadtime)
    {
         if (deadtime<3)
         {
             difficulty=Difficultys.Hard;
               diftext.color=Color.red;
         }            
         else if(deadtime<7)
         {
           difficulty=Difficultys.Medium;
            diftext.color=Color.yellow;
         }   
         else
         {
           difficulty=Difficultys.Easy;
            diftext.color=Color.green;
         }
         
          diftext.text="Player Rank: "+difficulty;
          Debug.Log(deadtime);
     
    }
    public Difficultys GetDiffuclty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }
 
    


}
