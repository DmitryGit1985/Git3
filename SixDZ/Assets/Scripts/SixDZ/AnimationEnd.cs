using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEnd : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator,
                                   AnimatorStateInfo stateInfo,
                                   int layerIndex)
    {
        if (stateInfo.IsName("Attack")) 
        { 
            return; 
        }
        Debug.Log("AttackEnd");


    }
}
