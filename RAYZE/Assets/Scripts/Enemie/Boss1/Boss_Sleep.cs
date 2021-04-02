using System.Collections;
using System.Collections.Generic;
using UnityEngine;
        //By Mehmet Budak
public class Boss_Sleep : StateMachineBehaviour
{
    //By Mehmet Budak

    [SerializeField] Boss1_SrctySystem Boss1;
    [SerializeField] TriggerColliderBoss Boss2;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss1 = GameObject.FindGameObjectWithTag("OldSrctySystem").GetComponent<Boss1_SrctySystem>();
      
    }

  
}
